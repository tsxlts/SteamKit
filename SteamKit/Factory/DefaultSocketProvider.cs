using System.Net.Sockets;
using System.Net.WebSockets;

namespace SteamKit.Factory
{
    internal class DefaultSocketProvider : ISocketProvider
    {
        public Socket GetSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, int timeout)
        {
            var socket = new Socket(addressFamily, socketType, protocolType)
            {
                ReceiveTimeout = timeout,
                SendTimeout = timeout,
            };
            return socket;
        }

        public ClientWebSocket GetWebSocket()
        {
            var webSocket = new ClientWebSocket();
            return webSocket;
        }
    }
}
