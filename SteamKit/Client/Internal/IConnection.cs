using System.Net;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal
{
    internal interface IConnection : IDisposable
    {
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
        public ProtocolTypes Protocol { get; }

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public Task ConnectAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Disconnect
        /// </summary>
        /// <param name="disconnectType"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        public Task DisconnectAsync(DisconnectedEventArgs.DisconnectType disconnectType, CancellationToken cancellationToken = default);

        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        public Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="data"></param>
        public void Send(Memory<byte> data);

        /// <summary>
        /// GetLocalIP
        /// </summary>
        /// <returns></returns>
        public IPAddress? GetLocalIP();

        /// <summary>
        /// IsConnected
        /// </summary>
        public bool IsConnected { get; }
    }
}
