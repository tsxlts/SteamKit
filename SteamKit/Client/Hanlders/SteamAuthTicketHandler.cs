using System.Diagnostics;
using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SteamAuthTicketHandler : MessageHandler
    {
        private static uint Sequence;

        private object ticketChangeLock = new object();
        private readonly Queue<byte[]> gameConnectTokens;
        private readonly IDictionary<uint, List<CMsgAuthTicket>> ticketsByGame;
        private readonly IDictionary<EMsg, Func<IServerMsg, Task>> msgHandler;

        /// <summary>
        /// 
        /// </summary>
        internal SteamAuthTicketHandler() : base()
        {
            gameConnectTokens = new Queue<byte[]>();
            ticketsByGame = new Dictionary<uint, List<CMsgAuthTicket>>();
            msgHandler = new Dictionary<EMsg, Func<IServerMsg, Task>>
            {
                { EMsg.ClientGameConnectTokens, HandleGameConnectTokens },
                { EMsg.ClientLogOff, HandleLogOffResponse }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<TicketInfo?> GetAuthSessionTicketAsync(uint appId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var ticketResponse = await Client.SendAsync<CMsgClientGetAppOwnershipTicket, CMsgClientGetAppOwnershipTicketResponse>(EMsg.ClientGetAppOwnershipTicket, new CMsgClientGetAppOwnershipTicket
            {
                app_id = appId,
            }, cancellationToken);

            if (ticketResponse.EResult != EResult.OK)
            {
                throw new Exception($"Failed to obtain app ownership ticket. Result: {ticketResponse.EResult}. The user may not own the game or there was an error.");
            }

            if (!gameConnectTokens.TryDequeue(out var token))
            {
                return null;
            }

            var authTicket = BuildAuthTicket(token);
            var ticket = await VerifyTicket(appId, authTicket, cancellationToken, out var crc);

            if (!ticket.Result!.ticket_crc.Any(x => x == crc))
            {
                throw new Exception("Ticket verification failed.");
            }

            var tok = CombineTickets(authTicket, ticketResponse.Result!.ticket);
            return new TicketInfo(appId, tok);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetMsg"></param>
        protected internal override Task HandleMsgAsync(IServerMsg packetMsg)
        {
            if (msgHandler.TryGetValue(packetMsg.MsgType, out var handlerFunc))
            {
                return handlerFunc(packetMsg);
            }

            return Task.CompletedTask;
        }

        private Task HandleLogOffResponse(IServerMsg packetMsg)
        {
            gameConnectTokens.Clear();
            return Task.CompletedTask;
        }

        private Task HandleGameConnectTokens(IServerMsg packetMsg)
        {
            var body = new ServerProtoBufMsg<CMsgClientGameConnectTokens>(packetMsg).Body;

            foreach (var tok in body.tokens)
            {
                gameConnectTokens.Enqueue(tok);
            }

            while (gameConnectTokens.Count > body.max_tokens_to_keep)
            {
                gameConnectTokens.TryDequeue(out _);
            }

            return Task.CompletedTask;
        }

        private static byte[] BuildAuthTicket(byte[] gameConnectToken)
        {
            const int sessionSize =
                4 + // unknown, always 1
                4 + // unknown, always 2
                4 + // public IP v4, optional
                4 + // private IP v4, optional
                4 + // timestamp & uint.MaxValue
                4;  // sequence

            using var stream = new MemoryStream(gameConnectToken.Length + 4 + sessionSize);
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(gameConnectToken.Length);
                writer.Write(gameConnectToken);

                writer.Write(sessionSize);
                writer.Write(1);
                writer.Write(2);

                Span<byte> randomBytes = stackalloc byte[8];
                RandomNumberGenerator.Fill(randomBytes);
                writer.Write(randomBytes);
                writer.Write((uint)Stopwatch.GetTimestamp());
                // Use Interlocked to safely increment the sequence number
                writer.Write(Interlocked.Increment(ref Sequence));
            }
            return stream.ToArray();
        }

        private static byte[] CombineTickets(byte[] authTicket, byte[] appTicket)
        {
            var len = appTicket.Length;
            var token = new byte[authTicket.Length + 4 + len];
            var mem = token.AsSpan();
            authTicket.CopyTo(mem);
            MemoryMarshal.Write(mem[authTicket.Length..], in len);
            appTicket.CopyTo(mem[(authTicket.Length + 4)..]);

            return token;
        }

        private Task<JobResult<CMsgClientAuthListAck>> VerifyTicket(uint appid, byte[] authToken, CancellationToken cancellationToken, out uint crc)
        {
            crc = BitConverter.ToUInt32(Crc32.Hash(authToken), 0);
            lock (ticketChangeLock)
            {
                if (!ticketsByGame.TryGetValue(appid, out var items))
                {
                    items = [];
                    ticketsByGame[appid] = items;
                }

                items.Add(new CMsgAuthTicket
                {
                    gameid = appid,
                    ticket = authToken,
                    ticket_crc = crc
                });
            }

            return SendTickets(cancellationToken);
        }

        private Task<JobResult<CMsgClientAuthListAck>> SendTickets(CancellationToken cancellationToken)
        {
            var message = new CMsgClientAuthList
            {
                tokens_left = (uint)gameConnectTokens.Count,
            };

            lock (ticketChangeLock)
            {
                message.app_ids.AddRange(ticketsByGame.Keys);
                message.tickets.AddRange(ticketsByGame.Values.SelectMany(x => x));
            }

            return Client!.SendAsync<CMsgClientAuthList, CMsgClientAuthListAck>(EMsg.ClientAuthList, message, cancellationToken);
        }
    }
}
