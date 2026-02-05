
using System.Net;
using System.Net.Sockets;
using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal.Connection
{
    internal class UdpConnection : IConnection
    {
        private const uint RESEND_DELAY = 3;
        private const uint TIMEOUT_DELAY = 60;
        private const uint RESEND_COUNT = 3;
        private const uint AHEAD_COUNT = 5;

        private readonly Socket socket;
        private Thread? netThread;
        private List<UdpPacket> outPackets;
        private Dictionary<uint, UdpPacket> inPackets;

        private int udpState = (int)UdpState.Disconnected;

        private DateTime timeOut;
        private DateTime nextResend;
        private static uint sourceConnId = 512;
        private uint remoteConnId;
        private uint outSeq;
        private uint outSeqSent;
        private uint outSeqAcked;
        private uint inSeq;
        private uint inSeqAcked;
        private uint inSeqHandled;

        public UdpConnection(EndPoint endPoint, Socket innerSocket)
        {
            CurrentEndPoint = endPoint;

            outPackets = new List<UdpPacket>();
            inPackets = new Dictionary<uint, UdpPacket>();

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            socket = innerSocket ?? throw new ArgumentNullException("innerSocket");
            socket.Bind(localEndPoint);
        }

        /// <summary>
        /// CurrentEndPoint
        /// </summary>
        public EndPoint CurrentEndPoint { get; }

        /// <summary>
        /// Connected
        /// </summary>
        public event EventHandler<ConnectedEventArgs>? Connected;

        /// <summary>
        /// Disconnected
        /// </summary>
        public event EventHandler<DisconnectedEventArgs>? Disconnected;

        /// <summary>
        /// MsgReceived
        /// </summary>
        public event EventHandler<MsgEventArgs>? MsgReceived;

        /// <summary>
        /// ProtocolTypes
        /// </summary>
        public ProtocolTypes Protocol => ProtocolTypes.Udp;

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            outPackets.Clear();
            inPackets.Clear();

            remoteConnId = 0;

            outSeq = 1;
            outSeqSent = 0;
            outSeqAcked = 0;

            inSeq = 0;
            inSeqAcked = 0;
            inSeqHandled = 0;

            netThread = new Thread(Loop)
            {
                Name = "Steam-UdpConnection"
            };
            netThread.Start();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        /// <param name="disconnectType"></param>
        /// <param name="cancellationToken"></param>
        public async Task DisconnectAsync(DisconnectType disconnectType, CancellationToken cancellationToken = default)
        {
            await DisconnectAsync(disconnectType, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        public async Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default)
        {
            if (udpState != (int)UdpState.Connected)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                UdpPacket[] packets = new UdpPacket[(ms.Length / UdpPacket.MAX_PAYLOAD) + 1];

                for (int i = 0; i < packets.Length; i++)
                {
                    long index = i * UdpPacket.MAX_PAYLOAD;
                    long length = Math.Min(UdpPacket.MAX_PAYLOAD, ms.Length - index);

                    packets[i] = new UdpPacket(EUdpPacketType.Data, ms, length);
                    packets[i].Header.MsgSize = (uint)ms.Length;
                }

                SendSequenced(packets);

                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="data"></param>
        public void Send(Memory<byte> data)
        {
            if (udpState != (int)UdpState.Connected)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                UdpPacket[] packets = new UdpPacket[(ms.Length / UdpPacket.MAX_PAYLOAD) + 1];

                for (int i = 0; i < packets.Length; i++)
                {
                    long index = i * UdpPacket.MAX_PAYLOAD;
                    long length = Math.Min(UdpPacket.MAX_PAYLOAD, ms.Length - index);

                    packets[i] = new UdpPacket(EUdpPacketType.Data, ms, length);
                    packets[i].Header.MsgSize = (uint)ms.Length;
                }

                SendSequenced(packets);
            }
        }

        /// <summary>
        /// GetLocalIP
        /// </summary>
        /// <returns></returns>
        public IPAddress? GetLocalIP()
        {
            var ipEndPoint = socket.LocalEndPoint as IPEndPoint;
            if (ipEndPoint == null || ipEndPoint.Address == IPAddress.Any)
            {
                return null;
            }

            return ipEndPoint.Address;
        }

        /// <summary>
        /// IsConnected
        /// </summary>
        public bool IsConnected => udpState == (int)UdpState.Connected;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            socket?.Dispose();
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        /// <param name="disconnectType"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        private async Task DisconnectAsync(DisconnectType disconnectType, Exception? exception, CancellationToken cancellationToken = default)
        {
            if (socket == null || netThread == null)
            {
                return;
            }

            UdpState state = (UdpState)udpState;
            UdpState localState = (UdpState)Interlocked.Exchange(ref udpState, (int)UdpState.Disconnecting);
            if (state != UdpState.Disconnected && localState == (int)UdpState.Disconnected)
            {
                udpState = (int)UdpState.Disconnected;
            }
            if (state == UdpState.Disconnecting)
            {
                SendSequenced(new UdpPacket(EUdpPacketType.Disconnect));
            }

            sourceConnId += 256;

            await Task.CompletedTask;

            Disconnected?.Invoke(this, new DisconnectedEventArgs(disconnectType) { Exception = exception });
        }

        /// <summary>
        /// Sends the packet as a sequenced, reliable packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void SendSequenced(UdpPacket packet)
        {
            lock (outPackets)
            {
                packet.Header.SeqThis = outSeq;
                packet.Header.MsgStartSeq = outSeq;
                packet.Header.PacketsInMsg = 1;

                outPackets.Add(packet);

                outSeq++;
            }
        }

        /// <summary>
        /// Sends the packets as one sequenced, reliable net message.
        /// </summary>
        /// <param name="packets">The packets that make up the single net message</param>
        private void SendSequenced(UdpPacket[] packets)
        {
            lock (outPackets)
            {
                uint msgStart = outSeq;

                foreach (UdpPacket packet in packets)
                {
                    SendSequenced(packet);

                    // Correct for any assumptions made for the single-packet case.
                    packet.Header.PacketsInMsg = (uint)packets.Length;
                    packet.Header.MsgStartSeq = msgStart;
                }
            }
        }

        /// <summary>
        /// Sends a packet immediately.
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void SendPacket(UdpPacket packet)
        {
            packet.Header.SourceConnID = sourceConnId;
            packet.Header.DestConnID = remoteConnId;
            packet.Header.SeqAck = inSeqAcked = inSeq;

            byte[] data = packet.GetData();

            try
            {
                socket.SendTo(data, CurrentEndPoint);
            }
            catch (SocketException)
            {
                udpState = (int)UdpState.Disconnected;
                return;
            }

            // If we've been idle but completely acked for more than two seconds, the next sent
            // packet will trip the resend detection. This fixes that.
            if (outSeqSent == outSeqAcked)
            {
                nextResend = DateTime.Now.AddSeconds(RESEND_DELAY);
            }

            // Sending should generally carry on from the packet most recently sent, even if it was a
            // resend (who knows what else was lost).
            if (packet.Header.SeqThis > 0)
            {
                outSeqSent = Math.Max(outSeqSent, packet.Header.SeqThis);
            }
        }

        /// <summary>
        /// Sends a datagram Ack, used when an Ack needs to be sent but there is no data response to piggy-back on.
        /// </summary>
        private void SendAck()
        {
            SendPacket(new UdpPacket(EUdpPacketType.Datagram));
        }

        /// <summary>
        /// Sends or resends sequenced messages, if necessary. Also responsible for throttling
        /// the rate at which they are sent.
        /// </summary>
        private void SendPendingMessages()
        {
            lock (outPackets)
            {
                if (DateTime.Now > nextResend && outSeqSent > outSeqAcked)
                {
                    // If we can't clear the send queue during a Disconnect, clear out the pending messages
                    if (udpState == (int)UdpState.Disconnecting)
                    {
                        outPackets.Clear();
                    }

                    // Don't send more than 3 (Steam behavior?)
                    for (int i = 0; i < RESEND_COUNT && i < outPackets.Count; i++)
                    {
                        SendPacket(outPackets[i]);
                    }

                    nextResend = DateTime.Now.AddSeconds(RESEND_DELAY);
                }
                else if (outSeqSent < outSeqAcked + AHEAD_COUNT)
                {
                    // I've never seen Steam send more than 4 packets before it gets an Ack, so this limits the
                    // number of sequenced packets that can be sent out at one time.
                    for (int i = (int)(outSeqSent - outSeqAcked); i < AHEAD_COUNT && i < outPackets.Count; i++)
                    {
                        SendPacket(outPackets[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the number of message parts in the next message.
        /// </summary>
        /// <returns>Non-zero number of message parts if a message is ready to be handled, 0 otherwise</returns>
        private uint ReadyMessageParts()
        {
            // Make sure that the first packet of the next message to handle is present
            if (!inPackets.TryGetValue(inSeqHandled + 1, out var packet))
            {
                return 0;
            }

            // ...and if relevant, all subparts of the message also
            for (uint i = 1; i < packet.Header.PacketsInMsg; i++)
            {
                if (!inPackets.ContainsKey(inSeqHandled + 1 + i))
                {
                    return 0;
                }
            }

            return packet.Header.PacketsInMsg;
        }

        /// <summary>
        /// Dispatches up to one message to the rest of SteamKit
        /// </summary>
        /// <returns>True if a message was dispatched, false otherwise</returns>
        private bool DispatchMessage()
        {
            uint numPackets = ReadyMessageParts();

            if (numPackets == 0)
                return false;

            MemoryStream payload = new MemoryStream();
            for (uint i = 0; i < numPackets; i++)
            {
                var handled = inPackets.TryGetValue(++inSeqHandled, out var packet);
                inPackets.Remove(inSeqHandled);

                packet!.Payload.WriteTo(payload);
            }

            byte[] data = payload.ToArray();

            MsgReceived?.Invoke(this, new MsgEventArgs(data, CurrentEndPoint!));

            return true;
        }

        /// <summary>
        /// Receives the packet, performs all sanity checks and then passes it along as necessary.
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ReceivePacket(UdpPacket packet)
        {
            // Check for a malformed packet
            if (!packet.IsValid)
            {
                return;
            }
            else if (remoteConnId > 0 && packet.Header.SourceConnID != remoteConnId)
            {
                return;
            }

            // Throw away any duplicate messages we've already received, making sure to
            // re-ack it in case it got lost.
            if (packet.Header.PacketType == EUdpPacketType.Data && packet.Header.SeqThis < inSeq)
            {
                SendAck();
                return;
            }

            // When we get a SeqAck, all packets with sequence numbers below that have been safely received by
            // the server; we are now free to remove our copies
            if (outSeqAcked < packet.Header.SeqAck)
            {
                outSeqAcked = packet.Header.SeqAck;

                // outSeqSent can be less than this in a very rare case involving resent packets.
                if (outSeqSent < outSeqAcked)
                {
                    outSeqSent = outSeqAcked;
                }

                outPackets.RemoveAll(x => x.Header.SeqThis <= outSeqAcked);
                nextResend = DateTime.Now.AddSeconds(RESEND_DELAY);
            }

            // inSeq should always be the latest value that we can ack, so advance it as far as is possible.
            if (packet.Header.SeqThis == inSeq + 1)
            {
                do
                {
                    inSeq++;
                }
                while (inPackets.ContainsKey(inSeq + 1));
            }

            switch (packet.Header.PacketType)
            {
                case EUdpPacketType.Challenge:
                    ReceiveChallenge(packet);
                    break;

                case EUdpPacketType.Accept:
                    ReceiveAccept(packet);
                    break;

                case EUdpPacketType.Data:
                    ReceiveData(packet);
                    break;

                case EUdpPacketType.Disconnect:
                    DisconnectAsync(DisconnectType.ConnectionClosed).GetAwaiter().GetResult();
                    return;

                case EUdpPacketType.Datagram:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Receives the challenge and responds with a Connect request
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ReceiveChallenge(UdpPacket packet)
        {
            if (Interlocked.CompareExchange(ref udpState, (int)UdpState.ConnectSent, (int)UdpState.ChallengeReqSent) != (int)UdpState.ChallengeReqSent)
            {
                return;
            }

            ChallengeData cr = new ChallengeData();
            cr.Deserialize(packet.Payload);

            ConnectData cd = new ConnectData();
            cd.ChallengeValue = cr.ChallengeValue ^ ConnectData.CHALLENGE_MASK;

            MemoryStream ms = new MemoryStream();
            cd.Serialize(ms);
            ms.Seek(0, SeekOrigin.Begin);

            SendSequenced(new UdpPacket(EUdpPacketType.Connect, ms));

            inSeqHandled = packet.Header.SeqThis;
        }

        /// <summary>
        /// Receives the notification of an accepted connection and sets the connection id that will be used for the
        /// connection's duration.
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ReceiveAccept(UdpPacket packet)
        {
            if (Interlocked.CompareExchange(ref udpState, (int)UdpState.Connected, (int)UdpState.ConnectSent) != (int)UdpState.ConnectSent)
            {
                return;
            }

            remoteConnId = packet.Header.SourceConnID;
            inSeqHandled = packet.Header.SeqThis;

            Connected?.Invoke(this, new ConnectedEventArgs());
        }

        /// <summary>
        /// Receives typical data packets before dispatching them for consumption by the rest of SteamKit
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ReceiveData(UdpPacket packet)
        {
            // Data packets are unexpected if a valid connection has not been established
            if (udpState != (int)UdpState.Connected && udpState != (int)UdpState.Disconnecting)
            {
                return;
            }

            // If we receive a packet that we've already processed (e.g. it got resent due to a lost ack)
            // or that is already waiting to be processed, do nothing.
            if (packet.Header.SeqThis <= inSeqHandled || inPackets.ContainsKey(packet.Header.SeqThis))
            {
                return;
            }

            inPackets.Add(packet.Header.SeqThis, packet);

            while (DispatchMessage()) ;
        }

        private void Loop()
        {
            EndPoint packetSender = new IPEndPoint(IPAddress.Any, 0);
            byte[] buf = new byte[2048];

            if (CurrentEndPoint != null)
            {
                timeOut = DateTime.Now.AddSeconds(TIMEOUT_DELAY);
                nextResend = DateTime.Now.AddSeconds(RESEND_DELAY);

                if (Interlocked.CompareExchange(ref udpState, (int)UdpState.ChallengeReqSent, (int)UdpState.Disconnected) != (int)UdpState.Disconnected)
                {
                    DisconnectAsync(DisconnectType.UserInitiated).GetAwaiter().GetResult();
                }
                else
                {
                    // Begin by sending off the challenge request
                    SendPacket(new UdpPacket(EUdpPacketType.ChallengeReq));
                }
            }

            while (udpState != (int)UdpState.Disconnected)
            {
                try
                {
                    // Wait up to 150ms for data, if none is found and the timeout is exceeded, we're done here.
                    if (!socket.Poll(150000, SelectMode.SelectRead) && DateTime.Now > timeOut)
                    {
                        DisconnectAsync(DisconnectType.ConnectionError).GetAwaiter().GetResult();
                        break;
                    }

                    // By using a 10ms wait, we allow for multiple packets sent at the time to all be processed before moving on
                    // to processing output and therefore Acks (the more we process at the same time, the fewer acks we have to send)
                    while (socket.Poll(10000, SelectMode.SelectRead))
                    {
                        int length = socket.ReceiveFrom(buf, ref packetSender);

                        // Ignore packets that aren't sent by the server we're connected to.
                        if (!packetSender.Equals(CurrentEndPoint))
                        {
                            continue;
                        }

                        // Data from the desired server was received; delay timeout
                        timeOut = DateTime.Now.AddSeconds(TIMEOUT_DELAY);

                        MemoryStream ms = new MemoryStream(buf, 0, length);
                        UdpPacket packet = new UdpPacket(ms);

                        ReceivePacket(packet);
                    }
                }
                catch (IOException ex)
                {
                    DisconnectAsync(DisconnectType.Exception, ex).GetAwaiter().GetResult();
                    break;
                }
                catch (SocketException ex)
                {
                    DisconnectAsync(DisconnectType.Exception, ex).GetAwaiter().GetResult();
                    break;
                }

                // Send or resend any sequenced packets; a call to ReceivePacket can set our state to disconnected
                // so don't send anything we have queued in that case
                if (udpState != (int)UdpState.Disconnected)
                {
                    SendPendingMessages();
                }

                // If we received data but had no data to send back, we need to manually Ack (usually tags along with
                // outgoing data); also acks disconnections
                if (inSeq != inSeqAcked)
                {
                    SendAck();
                }

                // If a graceful shutdown has been requested, nothing in the outgoing queue is discarded.
                // Once it's empty, we exit, since the last packet was our disconnect notification.
                if (udpState == (int)UdpState.Disconnecting && outPackets.Count == 0)
                {
                    DisconnectAsync(DisconnectType.UserInitiated).GetAwaiter().GetResult();
                    break;
                }
            }

            socket?.Dispose();
        }

        private enum UdpState
        {
            Disconnected,
            ChallengeReqSent,
            ConnectSent,
            Connected,
            Disconnecting
        }
    }
}
