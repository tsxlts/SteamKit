
using System.Net;
using SteamKit.Client.Model;

namespace SteamKit.Factory
{
    /// <summary>
    /// ServerFactory
    /// </summary>
    public interface IServerProvider
    {
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public Task<SteamServer?> GetServerAsync(ProtocolTypes protocol, CancellationToken cancellationToken = default);

        /// <summary>
        /// 重置服务
        /// </summary>
        /// <param name="servers"></param>
        public void ResetServer(IEnumerable<SteamServer> servers);

        /// <summary>
        /// 禁用服务
        /// </summary>
        /// <param name="endPoint"></param>
        public void DisableServer(EndPoint endPoint);
    }
}
