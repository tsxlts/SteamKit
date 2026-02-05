
using System.Net;

namespace SteamKit.Factory
{
    /// <summary>
    /// HttpClientFactory
    /// </summary>
    public interface IHttpClientFactory
    {
        /// <summary>
        /// 获取HttpClient
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="useCookies">使用Cookie</param>
        /// <param name="allowAutoRedirect">允许自动302</param>
        /// <param name="proxy">代理</param>
        /// <returns></returns>
        public HttpClient GetHttpClient(Uri uri, bool useCookies, bool allowAutoRedirect, IWebProxy? proxy = null);

        /// <summary>
        /// 请求完成时调用
        /// </summary>
        /// <param name="client">HttpClient</param>
        public void Complete(HttpClient client);
    }
}
