
using System.Net;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Model
{
    internal class Server : SteamServer
    {
        internal Server(EndPoint endPoint, ProtocolTypes protocolTypes) : base(endPoint, protocolTypes)
        {
            Available = true;
        }

        public bool Available { get; set; }

        public static bool operator ==(Server? left, Server? right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return !ReferenceEquals(left, null) && left.Equals(right);
        }

        public static bool operator !=(Server? left, Server? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Server other &&
                 EndPoint.Equals(other.EndPoint) &&
                 ProtocolTypes == other.ProtocolTypes;
        }

        public override int GetHashCode()
        {
            return EndPoint.GetHashCode() ^ ProtocolTypes.GetHashCode();
        }

        /// <summary>
        /// 根据给定的IP端点创建一个套接字服务器
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Server CreateSocketServer(IPEndPoint endPoint)
            => new Server(endPoint, ProtocolTypes.Tcp | ProtocolTypes.Udp);

        /// <summary>
        /// 根据“主机名:端口”格式的地址创建一个套接字服务器
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Server CreateDnsSocketServer(string address)
            => CreateServerFromDns(address, ProtocolTypes.Tcp | ProtocolTypes.Udp);

        /// <summary>
        /// 根据“主机名:端口”格式的地址创建一个WebSocket服务器
        /// </summary>
        /// <param name="address"></param>
        public static Server CreateWebSocketServer(string address)
            => CreateServerFromDns(address, ProtocolTypes.WebSocket);

        /// <summary>
        /// 为“hostname:port”形式的地址创建WebSocket服务器
        /// </summary>
        /// <param name="address"></param>
        /// <param name="protocolTypes"></param>
        private static Server CreateServerFromDns(string address, ProtocolTypes protocolTypes)
        {
            ArgumentNullException.ThrowIfNull(address);

            if (IPEndPoint.TryParse(address, out var ipEndPoint))
            {
                return new Server(ipEndPoint, protocolTypes);
            }

            EndPoint endPoint;
            const int DefaultPort = 443;

            var indexOfColon = address.IndexOf(':', StringComparison.Ordinal);
            if (indexOfColon >= 0)
            {
                var hostname = address[..indexOfColon];
                var portNumber = address[(indexOfColon + 1)..];

                if (!int.TryParse(portNumber, out var port))
                {
                    throw new ArgumentException("Port number must be a valid integer value.", nameof(address));
                }

                endPoint = new DnsEndPoint(hostname, port);
            }
            else
            {
                endPoint = new DnsEndPoint(address, DefaultPort);
            }

            return new Server(endPoint, protocolTypes);
        }
    }
}
