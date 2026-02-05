using System.Net;
using System.Net.Sockets;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal.Connection
{
    internal class TcpConnection : IConnection
    {
        const uint MAGIC = 0x31305456; // "VT01"

        private readonly Socket socket;
        private CancellationTokenSource? thisCancellationToken;
        private Thread? netThread;

        public TcpConnection(EndPoint endPoint, int timeout)
        {
            CurrentEndPoint = endPoint;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = timeout,
                SendTimeout = timeout,
            };
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

                    netThread = new Thread(async () => await LoopAsync())
                    {
                        Name = "Steam-TcpConnection"
                    };
                    netThread.Start();

                    Connected?.Invoke(this, new ConnectedEventArgs());

                    /*
                    task = socket.ConnectAsync(CurrentEndPoint, cancellationToken).AsTask().ContinueWith(result =>
                    {
                        if (!result.IsCompletedSuccessfully)
                        {
                            switch (result.Status)
                            {
                                case TaskStatus.Canceled:
                                    throw new OperationCanceledException();
                                default:
                                    if (result.Exception != null)
                                    {
                                        throw result.Exception!;
                                    }
                                    throw new ConnectionException(CurrentEndPoint, Protocol, "Connect Exception");
                            }
                        }

                        netThread = new Thread(async () => await LoopAsync())
                        {
                            Name = "Steam-TcpConnection"
                        };
                        netThread.Start();

                        Connected?.Invoke(this, EventArgs.Empty);
                    });
                    */
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
        public async Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default)
        {
            if (socket == null || !socket.Connected)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            try
            {
                using (CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(thisCancellationToken!.Token, cancellationToken))
                {
                    List<byte> bytes = new List<byte>();
                    bytes.AddRange(BitConverter.GetBytes((uint)data.Length));
                    bytes.AddRange(BitConverter.GetBytes(MAGIC));
                    bytes.AddRange(data.Span);

                    await socket.SendAsync(bytes.ToArray(), tokenSource.Token).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="data"></param>
        public void Send(Memory<byte> data)
        {
            if (socket == null || !socket.Connected)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            try
            {
                List<byte> bytes = new List<byte>();
                bytes.AddRange(BitConverter.GetBytes((uint)data.Length));
                bytes.AddRange(BitConverter.GetBytes(MAGIC));
                bytes.AddRange(data.Span);

                socket.Send(bytes.ToArray());
            }
            catch (Exception)
            {
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

        private async Task<byte[]> ReadPacketAsync()
        {
            byte[] buffer;

            int size;
            buffer = new byte[4];
            size = await socket.ReceiveAsync(buffer);
            if (size != 4)
            {
                throw new IOException("Connection lost while reading packet header.");
            }
            uint packetLen = BitConverter.ToUInt32(buffer);

            buffer = new byte[4];
            size = await socket.ReceiveAsync(buffer);
            if (size != 4)
            {
                throw new IOException("Connection lost while reading packet header.");
            }
            uint packetMagic = BitConverter.ToUInt32(buffer);
            if (packetMagic != MAGIC)
            {
                throw new IOException("Got a packet with invalid magic!");
            }

            buffer = new byte[packetLen];
            size = 0;
            do
            {
                int readCount = await socket.ReceiveAsync(new ArraySegment<byte>(buffer, size, (int)packetLen - size));
                if (readCount == 0)
                {
                    break;
                }

                size = size + readCount;
            } while (size < packetLen);
            if (size != packetLen)
            {
                throw new IOException("Connection lost while reading packet payload");
            }

            return buffer;
        }
    }
}
