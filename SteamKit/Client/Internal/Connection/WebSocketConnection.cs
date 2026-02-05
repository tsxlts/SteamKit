using System.Buffers;
using System.ComponentModel;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal.Connection
{
    internal class WebSocketConnection : IConnection
    {
        private readonly ClientWebSocket socket;
        private CancellationTokenSource? thisCancellationToken;
        private Task? netThread;

        public WebSocketConnection(EndPoint endPoint, ClientWebSocket innerSocket)
        {
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
        public ProtocolTypes Protocol => ProtocolTypes.WebSocket;

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (socket.State == WebSocketState.Open)
            {
                return;
            }

            await DisconnectAsync(DisconnectType.UserInitiated).ConfigureAwait(false);

            thisCancellationToken = new CancellationTokenSource();
            using (var connectCancellation = CancellationTokenSource.CreateLinkedTokenSource(thisCancellationToken.Token, cancellationToken))
            {
                using (connectCancellation.Token.Register(s => ((ClientWebSocket)s!).Dispose(), socket))
                {
                    await socket.ConnectAsync(ConstructUri(CurrentEndPoint), cancellationToken).ConfigureAwait(false);
                    if (socket.State != WebSocketState.Open)
                    {
                        throw new ConnectionException(CurrentEndPoint, Protocol, "Connect Error");
                    }

                    netThread = LoopAsync();

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
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default)
        {
            if (socket == null || socket.State != WebSocketState.Open)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            try
            {
                using (CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(thisCancellationToken!.Token, cancellationToken))
                {
                    await socket.SendAsync(data, WebSocketMessageType.Binary, true, tokenSource.Token).ConfigureAwait(false);
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
            if (socket == null || socket.State != WebSocketState.Open)
            {
                throw new ConnectionException(CurrentEndPoint, Protocol, "没有创建连接,或者连接已断开");
            }

            try
            {
                socket.SendAsync(data, WebSocketMessageType.Binary, true, thisCancellationToken!.Token).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// GetLocalIP
        /// </summary>
        /// <returns></returns>
        public IPAddress? GetLocalIP() => IPAddress.None;

        /// <summary>
        /// IsConnected
        /// </summary>
        public bool IsConnected => socket?.State == WebSocketState.Open;

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
            if (socket != null && !new[] { WebSocketState.None, WebSocketState.Closed }.Contains(socket.State))
            {
                try
                {
                    using (socket)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken).ConfigureAwait(false);
                    }
                }
                finally
                {
                    if (!thisCancellationToken?.IsCancellationRequested ?? false)
                    {
                        thisCancellationToken?.Cancel();
                    }

                    Disconnected?.Invoke(this, new DisconnectedEventArgs(disconnectType) { Exception = exception });
                }
            }
        }

        internal static Uri ConstructUri(EndPoint endPoint)
        {
            var uri = new UriBuilder();
            uri.Scheme = "wss";
            uri.Path = "/cmsocket/";

            switch (endPoint)
            {
                case IPEndPoint ipep:
                    uri.Port = ipep.Port;
                    uri.Host = ipep.Address.ToString();
                    break;

                case DnsEndPoint dns:
                    uri.Host = dns.Host;
                    uri.Port = dns.Port;
                    break;

                default:
                    throw new InvalidOperationException("Unsupported endpoint type.");
            }

            return uri.Uri;
        }

        private async Task LoopAsync()
        {
            while (!thisCancellationToken!.IsCancellationRequested)
            {
                var outputBuffer = ArrayPool<byte>.Shared.Rent(1024);
                var readBuffer = ArrayPool<byte>.Shared.Rent(1024);
                var readMemory = readBuffer.AsMemory();

                try
                {
                    if (socket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    ValueWebSocketReceiveResult result;
                    var outputLength = 0;
                    do
                    {
                        try
                        {
                            result = await socket.ReceiveAsync(readMemory, thisCancellationToken!.Token).ConfigureAwait(false);
                        }
                        catch (ObjectDisposedException ex)
                        {
                            await DisconnectAsync(DisconnectType.Exception, ex).ConfigureAwait(false);
                            return;
                        }
                        catch (WebSocketException ex)
                        {
                            await DisconnectAsync(DisconnectType.Exception, ex).ConfigureAwait(false);
                            return;
                        }
                        catch (Win32Exception ex)
                        {
                            await DisconnectAsync(DisconnectType.Exception, ex).ConfigureAwait(false);
                            return;
                        }

                        switch (result.MessageType)
                        {
                            case WebSocketMessageType.Binary:
                                if (outputLength + result.Count > outputBuffer.Length)
                                {
                                    var newBuffer = ArrayPool<byte>.Shared.Rent(outputBuffer.Length * 2);
                                    Buffer.BlockCopy(outputBuffer, 0, newBuffer, 0, outputLength);
                                    ArrayPool<byte>.Shared.Return(outputBuffer);
                                    outputBuffer = newBuffer;
                                }

                                Buffer.BlockCopy(readBuffer, 0, outputBuffer, outputLength, result.Count);
                                outputLength += result.Count;
                                break;

                            case WebSocketMessageType.Text:
                                try
                                {
                                    var message = Encoding.UTF8.GetString(readBuffer, 0, result.Count);
                                }
                                catch
                                {
                                    var frameBytes = new byte[result.Count];
                                    Array.Copy(readBuffer, 0, frameBytes, 0, result.Count);
                                }
                                break;

                            case WebSocketMessageType.Close:
                                await DisconnectAsync(DisconnectType.ConnectionClosed);
                                return;

                            default:
                                await DisconnectAsync(DisconnectType.ConnectionError);
                                return;
                        }
                    }
                    while (!result.EndOfMessage);

                    var output = new byte[outputLength];
                    Buffer.BlockCopy(outputBuffer, 0, output, 0, output.Length);
                    MsgReceived?.Invoke(this, new MsgEventArgs(output, CurrentEndPoint!));
                }
                catch (Exception)
                {

                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(readBuffer);
                    ArrayPool<byte>.Shared.Return(outputBuffer);
                }
            }

            if (!thisCancellationToken!.IsCancellationRequested)
            {
                await DisconnectAsync(DisconnectType.ConnectionError);
            }
        }
    }
}
