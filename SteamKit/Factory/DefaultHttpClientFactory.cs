
using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace SteamKit.Factory
{
    internal class DefaultHttpClientFactory : IHttpClientFactory
    {
        private ConcurrentDictionary<string, HttpClient> Clients;

        public DefaultHttpClientFactory()
        {
            Clients = new ConcurrentDictionary<string, HttpClient>();
        }

        /// <summary>
        /// 获取HttpClient
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="useCookies">使用Cookie</param>
        /// <param name="allowAutoRedirect">允许自动302</param>
        /// <param name="proxy">代理</param>
        /// <returns></returns>
        public HttpClient GetHttpClient(Uri uri, bool useCookies, bool allowAutoRedirect, IWebProxy? proxy = null)
        {
            var domain = uri.Host;
            var port = uri.Port;
            var clientKey = new StringBuilder();
            clientKey.AppendLine($"[Server]{domain}:{port}");
            if (proxy != null)
            {
                clientKey.AppendLine($"[Proxy]{proxy.GetProxy(uri)}");
            }
            clientKey.AppendLine($"[useCookies]{useCookies}");
            clientKey.AppendLine($"[allowAutoRedirect]{allowAutoRedirect}");

            HttpClient client = Clients.GetOrAdd(clientKey.ToString(), key =>
            {
                var handler = new HttpClientHandler()
                {
                    UseCookies = useCookies,
                    AutomaticDecompression = DecompressionMethods.All,/* DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,*/
                    AllowAutoRedirect = allowAutoRedirect,
                    ServerCertificateCustomValidationCallback = (request, certificate2, chain, errors) => true
                };
                if (proxy != null)
                {
                    handler.Proxy = proxy;
                    handler.UseProxy = true;
                }

                return new HttpClient(handler)
                {
                    DefaultRequestVersion = HttpVersion.Version20,
                    DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                };
            });

            return client;
        }

        /// <summary>
        /// 请求完成时调用
        /// </summary>
        /// <param name="client">HttpClient</param>
        public void Complete(HttpClient client)
        {

        }
    }
}
