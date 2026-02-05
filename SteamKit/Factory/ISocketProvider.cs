using System.Net.Sockets;
using System.Net.WebSockets;
using SteamKit.Client.Model;

namespace SteamKit.Factory
{
    /// <summary>
    /// ConnectionProvider
    /// </summary>
    public interface ISocketProvider
    {
        /// <summary>
        /// 获取
        /// <see cref="ProtocolTypes.Tcp"/>
        /// <see cref="ProtocolTypes.Udp"/>
        /// Socket
        /// </summary>
        /// <returns></returns>
        public Socket GetSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, int timeout);

        /// <summary>
        /// 获取
        /// <see cref="ProtocolTypes.WebSocket"/>
        /// ClientWebSocket
        /// </summary>
        /// <returns></returns>
        public ClientWebSocket GetWebSocket();
    }
}
