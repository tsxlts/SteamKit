using System.Net;
using SteamKit.Factory;

namespace SteamKit
{
    /// <summary>
    /// HttpBulider
    /// </summary>
    public class HttpBulider
    {
        private static IHttpClientFactory httpClientFactory = new DefaultHttpClientFactory();

        /// <summary>
        /// 设置HttpClientFactory
        /// </summary>
        /// <param name="factory"></param>
        public static void WithHttpClientFactory(IHttpClientFactory factory)
        {
            httpClientFactory = factory;
        }

        /// <summary>
        /// 获取HttpClient
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="useCookies">使用Cookie</param>
        /// <param name="allowAutoRedirect">允许自动302</param>
        /// <param name="proxy">代理</param>
        /// <returns></returns>
        internal static HttpClient GetHttpClient(Uri uri, bool useCookies, bool allowAutoRedirect, IWebProxy? proxy = null)
        {
            return httpClientFactory.GetHttpClient(uri: uri, useCookies: useCookies, allowAutoRedirect: allowAutoRedirect, proxy: proxy);
        }

        /// <summary>
        /// 请求完成时调用
        /// </summary>
        /// <param name="client"></param>
        internal static void Complete(HttpClient client)
        {
            httpClientFactory.Complete(client);
        }
    }
}
