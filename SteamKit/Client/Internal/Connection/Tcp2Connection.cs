using System.Net;
using System.Net.Sockets;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal.Connection
{
    internal class Tcp2Connection : IConnection
    {
        const uint MAGIC = 0x31305456; // "VT01"

        private readonly object netLock;
        private readonly Socket socket;
        private CancellationTokenSource? thisCancellationToken;
        private Thread? netThread;
        private NetworkStream? netStream;
        private BinaryReader? netReader;
        private BinaryWriter? netWriter;

        public Tcp2Connection(EndPoint endPoint, Socket innerSocket)
        {
            netLock = new object();
            CurrentEndPoint = endPoint;
            socket = innerSocket ?? throw new ArgumentNullException("innerSocket");
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
        public ProtocolTypes Protocol => ProtocolTypes.Tcp;

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (socket.Connected)
            {
                return;
            }

            await DisconnectAsync(DisconnectType.UserInitiated).ConfigureAwait(false);

            thisCancellationToken = new CancellationTokenSource();
            using (var connectCancellation = CancellationTokenSource.CreateLinkedTokenSource(thisCancellationToken.Token, cancellationToken))
            {
                using (connectCancellation.Token.Register(s => ((Socket)s!).Dispose(), socket))
                {
                    await socket.ConnectAsync(CurrentEndPoint, cancellationToken).ConfigureAwait(false);
                    if (!socket.Connected)
                    {
                        throw new ConnectionException(CurrentEndPoint, Protocol, "Connect Error");
                    }

                    netStream = new NetworkStream(socket, false);
                    netReader = new BinaryReader(netStream);
                    netWriter = new BinaryWriter(netStream);

                    netThread = new Thread(async () => await LoopAsync())
                    {
                        Name = "Steam-TcpConnection"
                    };
                    netThread.Start();

                    Connected?.Invoke(this, new ConnectedEventArgs());
                }
            }
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
        public Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default)
        {
            lock (netLock)
            {
                if (socket == null || !socket.Connected)
                {
                    throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
                }

                try
                {
                    netWriter!.Write((uint)data.Length);
                    netWriter.Write(MAGIC);
                    netWriter.Write(data.Span);
                }
                catch (Exception)
                {
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="data"></param>
        public void Send(Memory<byte> data)
        {
            lock (netLock)
            {
                if (socket == null || !socket.Connected)
                {
                    throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
                }

                try
                {
                    netWriter!.Write((uint)data.Length);
                    netWriter.Write(MAGIC);
                    netWriter.Write(data.Span);
                }
                catch (Exception)
                {
                }
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

        public bool IsConnected => socket.Connected;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            netWriter?.Dispose();
            netReader?.Dispose();
            netStream?.Dispose();
            socket?.Dispose();
        }

        private async Task DisconnectAsync(DisconnectType disconnectType, Exception? exception, CancellationToken cancellationToken = default)
        {
            if (!thisCancellationToken?.IsCancellationRequested ?? false)
            {
                thisCancellationToken?.Cancel();
            }

            if (socket != null && socket.Connected)
            {
                try
                {
                    using (socket)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        await socket.DisconnectAsync(false, cancellationToken).ConfigureAwait(false);
                        socket.Close();

                        netWriter?.Dispose();
                        netReader?.Dispose();
                        netStream?.Dispose();
                    }
                }
                finally
                {
                    Disconnected?.Invoke(this, new DisconnectedEventArgs(disconnectType) { Exception = exception });
                }
            }
        }

        private async Task LoopAsync()
        {
            while (!thisCancellationToken!.IsCancellationRequested)
            {
                try
                {
                    if (!socket.Connected)
                    {
                        break;
                    }

                    bool canRead = socket.Poll(60 * 1000, SelectMode.SelectRead);
                    if (!canRead)
                    {
                        continue;
                    }

                    byte[] packData;
                    try
                    {
                        packData = await ReadPacketAsync();
                        if (packData.Length == 0)
                        {
                            await DisconnectAsync(DisconnectType.ConnectionClosed).ConfigureAwait(false);
                            break;
                        }
                    }
                    catch (IOException ex)
                    {
                        await DisconnectAsync(DisconnectType.Exception, ex).ConfigureAwait(false);
                        break;
                    }

                    MsgReceived?.Invoke(this, new MsgEventArgs(packData, CurrentEndPoint!));
                }
                catch (Exception)
                {
                }
            }
            if (!thisCancellationToken!.IsCancellationRequested)
            {
                await DisconnectAsync(DisconnectType.ConnectionError);
            }
        }

        private Task<byte[]> ReadPacketAsync()
        {
            uint packetLen;
            uint packetMagic;

            try
            {
                packetLen = netReader!.ReadUInt32();
                packetMagic = netReader.ReadUInt32();
            }
            catch (EndOfStreamException)
            {
                return Task.FromResult(new byte[0]);
            }
            catch (IOException ex)
            {
                throw new IOException("Connection lost while reading packet header.", ex);
            }

            if (packetMagic != MAGIC)
            {
                throw new IOException("Got a packet with invalid magic!");
            }

            byte[] packData = netReader.ReadBytes((int)packetLen);
            if (packData.Length != packetLen)
            {
                throw new IOException("Connection lost while reading packet payload");
            }

            return Task.FromResult(packData);
        }
    }
}
