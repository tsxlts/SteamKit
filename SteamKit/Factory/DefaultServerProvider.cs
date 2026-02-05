using System.Net;
using System.Security.Cryptography;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;

namespace SteamKit.Factory
{
    internal class DefaultServerProvider : IServerProvider
    {
        private List<Server> Servers { get; set; } = new List<Server>();

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<SteamServer?> GetServerAsync(ProtocolTypes protocol, CancellationToken cancellationToken = default)
        {
            if (!Servers.Any(c => c.Available && c.ProtocolTypes.HasFlag(protocol)))
            {
                var websockets = await SteamApi.QuetyCMListForConnectAsync("websockets", 50, cancellationToken);
                var netfilter = await SteamApi.QuetyCMListForConnectAsync("netfilter", 50, cancellationToken);
                var websocketsResponse = websockets.Body;
                var netfilterResponse = netfilter.Body;

                List<Server> serverRecords = new List<Server>();

                foreach (var server in websocketsResponse?.Servers ?? new List<Model.CMServer>())
                {
                    serverRecords.Add(Server.CreateWebSocketServer(server.Endpoint));
                }

                foreach (var server in netfilterResponse?.Servers ?? new List<Model.CMServer>())
                {
                    serverRecords.Add(Server.CreateDnsSocketServer(server.Endpoint));
                }

                ResetServer(serverRecords);
            }

            var temp = Servers.Where(c => c.Available && c.ProtocolTypes.HasFlag(protocol));
            int count = temp.Count();
            if (count > 0)
            {
                int index = RandomNumberGenerator.GetInt32(0, count);
                return temp.ElementAt(index);
            }

            return await GetServerFroCMListAsync(protocol, cancellationToken);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<SteamServer?> GetServerFroCMListAsync(ProtocolTypes protocol, CancellationToken cancellationToken = default)
        {
            if (!Servers.Any(c => c.Available && c.ProtocolTypes.HasFlag(protocol)))
            {
                var servers = (await SteamApi.QuetyCMListAsync(100, cancellationToken)).Body;

                List<Server> serverRecords = new List<Server>();

                foreach (var server in servers?.Servers ?? new List<string>())
                {
                    var colonPosition = server.LastIndexOf(':');
                    if (colonPosition == -1)
                    {
                        continue;
                    }
                    if (!IPAddress.TryParse(server.Substring(0, colonPosition), out var address))
                    {
                        continue;
                    }
                    if (!ushort.TryParse(server.Substring(colonPosition + 1), out var port))
                    {
                        continue;
                    }

                    serverRecords.Add(new Server(new IPEndPoint(address, port), ProtocolTypes.Tcp | ProtocolTypes.Udp));
                }
                foreach (var server in servers?.Websockets ?? new List<string>())
                {
                    var indexOfColon = server.IndexOf(':');
                    if (indexOfColon >= 0)
                    {
                        var hostname = server.Substring(0, indexOfColon);
                        var portNumber = server.Substring(indexOfColon + 1);
                        if (!int.TryParse(portNumber, out var port))
                        {
                            continue;
                        }

                        serverRecords.Add(new Server(new DnsEndPoint(hostname, port), ProtocolTypes.WebSocket));
                    }
                    else
                    {
                        serverRecords.Add(new Server(new DnsEndPoint(server, 443), ProtocolTypes.WebSocket));
                    }
                }

                ResetServer(serverRecords);
            }

            var temp = Servers.Where(c => c.Available && c.ProtocolTypes.HasFlag(protocol));
            int count = temp.Count();
            if (count == 0)
            {
                return null;
            }

            int index = RandomNumberGenerator.GetInt32(0, count);
            return temp.ElementAt(index);
        }

        /// <summary>
        /// 重置服务
        /// </summary>
        /// <param name="servers"></param>
        public void ResetServer(IEnumerable<SteamServer> servers)
        {
            Servers = servers.Select(c => new Server(c.EndPoint, c.ProtocolTypes)).ToList();
        }

        /// <summary>
        /// 禁用服务
        /// </summary>
        /// <param name="endPoint"></param>
        public void DisableServer(EndPoint endPoint)
        {
            foreach (var item in Servers.Where(c => c.Equals(endPoint)))
            {
                item.Available = false;
            }
        }
    }
}
