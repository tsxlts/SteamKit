using System.Net;
using System.Text;

namespace SteamKit.Internal
{
    /// <summary>
    /// HttpClient帮助类
    /// </summary>
    internal static class InternalHttpClient
    {
        const int MaxAutomaticRedirections = 10;

        static InternalHttpClient()
        {
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="content">HttpContent</param>
        /// <param name="resultType">ResultType</param>
        /// <param name="headers">Headers</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="proxy">WebProxy</param>
        /// <param name="completionOption">CompletionOption</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri uri, HttpMethod method, HttpContent? content, ResultType resultType, IDictionary<string, string>? headers = null, CookieCollection? cookies = null, IWebProxy? proxy = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            string domain = uri.Host;
            string path = uri.AbsolutePath;

            var client = HttpBulider.GetHttpClient(uri: uri, useCookies: false, allowAutoRedirect: false, proxy: proxy);

            try
            {
                using (var request = new HttpRequestMessage(method, uri) { Content = content, Version = client.DefaultRequestVersion, VersionPolicy = client.DefaultVersionPolicy })
                {
                    request.SetHeader("Accept", GetAccept(resultType));

                    if (headers?.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            request.SetHeader(header.Key, header.Value);
                        }
                    }

                    if (cookies?.Count > 0)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        var domainCookies = cookies.GetCookies(domain, path);
                        foreach (Cookie cookie in domainCookies)
                        {
                            if (cookie.Expired)
                            {
                                continue;
                            }

                            stringBuilder.Append($"{cookie.Name}={Uri.EscapeDataString(cookie.Value ?? "")}; ");
                        }

                        request.SetHeader("Cookie", stringBuilder.ToString());
                    }

                    var response = await client.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);

                    return response;
                }
            }
            finally
            {
                HttpBulider.Complete(client);
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="uri">Uri</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="content">HttpContent</param>
        /// <param name="resultType">ResultType</param>
        /// <param name="headers">Headers</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="proxy">WebProxy</param>
        /// <param name="completionOption">CompletionOption</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, Uri uri, HttpMethod method, HttpContent? content, ResultType resultType, IDictionary<string, string>? headers = null, CookieCollection? cookies = null, IWebProxy? proxy = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            string domain = uri.Host;
            string path = uri.AbsolutePath;
            using (var request = new HttpRequestMessage(method, uri) { Content = content, Version = httpClient.DefaultRequestVersion, VersionPolicy = httpClient.DefaultVersionPolicy })
            {
                request.SetHeader("Accept", GetAccept(resultType));

                if (headers?.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        request.SetHeader(header.Key, header.Value);
                    }
                }

                if (cookies?.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    var domainCookies = cookies.GetCookies(domain, path);
                    foreach (Cookie cookie in domainCookies)
                    {
                        if (cookie.Expired)
                        {
                            continue;
                        }

                        stringBuilder.Append($"{cookie.Name}={Uri.EscapeDataString(cookie.Value ?? "")}; ");
                    }

                    request.SetHeader("Cookie", stringBuilder.ToString());
                }

                var response = await httpClient.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);

                return response;
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="content">HttpContent</param>
        /// <param name="resultType">ResultType</param>
        /// <param name="autoRedirect">AllowAutoRedirect</param>
        /// <param name="headers">Headers</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="proxy">WebProxy</param>
        /// <param name="completionOption">CompletionOption</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri uri, HttpMethod method, HttpContent? content, ResultType resultType, bool autoRedirect, IDictionary<string, string>? headers = null, CookieCollection? cookies = null, IWebProxy? proxy = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            Uri targetUri = new Uri(uri.ToString());
            CookieCollection requestCookies = new CookieCollection
            {
                cookies ?? new CookieCollection()
            };

            HttpResponseMessage response;
            CookieCollection responseCookies = new CookieCollection();
            CookieCollection itemResponseCookies = new CookieCollection();
            int times = 0;
            do
            {
                response = await SendAsync(targetUri, method, content, resultType, headers, requestCookies, proxy, completionOption, cancellationToken);
                if (response.StatusCode != HttpStatusCode.Found || response.Headers.Location == null || !autoRedirect)
                {
                    response.Headers.Add("Set-Cookie", responseCookies.Select(c => $"{c.Name}={Uri.EscapeDataString(c.Value ?? "")}"));
                    return response;
                }

                if ((times++) > MaxAutomaticRedirections)
                {
                    throw new NotSupportedException($"超过最大重定向次数:{MaxAutomaticRedirections}");
                }

                targetUri = response.Headers.Location switch
                {
                    var location when location.IsAbsoluteUri => location,
                    var location when location.ToString().StartsWith('/') => new Uri($"{targetUri.Scheme}://{targetUri.Host}:{targetUri.Port}{location}"),
                    var location => new Uri($"{targetUri}/{location}")
                };

                itemResponseCookies = HttpUtils.GetCookies(response);

                responseCookies.Add(itemResponseCookies);
                requestCookies.Add(itemResponseCookies);
            } while (true);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="uri">Uri</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="content">HttpContent</param>
        /// <param name="resultType">ResultType</param>
        /// <param name="autoRedirect">AllowAutoRedirect</param>
        /// <param name="headers">Headers</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="proxy">WebProxy</param>
        /// <param name="completionOption">CompletionOption</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, Uri uri, HttpMethod method, HttpContent? content, ResultType resultType, bool autoRedirect, IDictionary<string, string>? headers = null, CookieCollection? cookies = null, IWebProxy? proxy = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            Uri targetUri = new Uri(uri.ToString());
            CookieCollection requestCookies = new CookieCollection
            {
                cookies ?? new CookieCollection()
            };

            HttpResponseMessage response;
            CookieCollection responseCookies = new CookieCollection();
            CookieCollection itemResponseCookies = new CookieCollection();
            int times = 0;
            do
            {
                response = await SendAsync(httpClient, targetUri, method, content, resultType, headers, requestCookies, proxy, completionOption, cancellationToken);
                if (response.StatusCode != HttpStatusCode.Found || response.Headers.Location == null || !autoRedirect)
                {
                    response.Headers.Add("Set-Cookie", responseCookies.Select(c => $"{c.Name}={Uri.EscapeDataString(c.Value ?? "")}"));
                    return response;
                }

                if ((times++) > MaxAutomaticRedirections)
                {
                    throw new NotSupportedException($"超过最大重定向次数:{MaxAutomaticRedirections}");
                }

                targetUri = response.Headers.Location;
                itemResponseCookies = HttpUtils.GetCookies(response);

                responseCookies.Add(itemResponseCookies);
                requestCookies.Add(itemResponseCookies);
            } while (true);
        }

        private static string GetAccept(ResultType resultType)
        {
            string accept = "*/*";
            switch (resultType)
            {
                case ResultType.JsonFormate:
                    accept = $"application/json;q=1," +
                        $"text/html;q=0.9," +
                        $"application/xhtml+xml;q=0.9," +
                        $"application/xml;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
                case ResultType.XmlFormate:
                    accept = $"application/xml;q=1," +
                        $"application/json;q=0.9," +
                        $"text/html;q=0.9," +
                        $"application/xhtml+xml;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
                case ResultType.HtmlFormate:
                    accept = $"text/html;q=1," +
                        $"application/xhtml+xml;q=0.9," +
                        $"application/xml;q=0.9," +
                        $"application/json;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
                case ResultType.Stream:
                    accept = $"application/octet-stream;q=1," +
                        $"application/json;q=0.9," +
                        $"text/html;q=0.9," +
                        $"application/xhtml+xml;q=0.9," +
                        $"application/xml;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
                case ResultType.TextPlain:
                    accept = $"text/html;q=1," +
                        $"application/json;q=0.9," +
                        $"application/xml;q=0.9," +
                        $"application/xhtml+xml;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
                case ResultType.Default:
                default:
                    accept = $"text/html;q=1," +
                        $"application/json;q=0.9," +
                        $"application/xml;q=0.9," +
                        $"application/xhtml+xml;q=0.9," +
                        $"image/webp;q=0.8," +
                        $"*/*;q=0.8";
                    break;
            }

            return accept;
        }

        private static void SetHeader(this HttpRequestMessage msg, string name, string value)
        {
            switch (name.ToLower())
            {
                case "allow":
                case "content-disposition":
                case "content-encoding":
                case "content-language":
                case "content-length":
                case "content-location":
                case "content-md5":
                case "content-range":
                case "content-type":
                case "expires":
                case "last-modified":
                    if (msg.Content != null)
                    {
                        msg.Content.Headers.Remove(name);
                        msg.Content.Headers.TryAddWithoutValidation(name, value);
                    }
                    break;
                default:
                    msg.Headers.Remove(name);
                    msg.Headers.TryAddWithoutValidation(name, value);
                    break;
            }
        }

        /// <summary>
        /// 请求结果类型
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// 默认
            /// </summary>
            Default = 0,

            /// <summary>
            /// Json格式
            /// </summary>
            JsonFormate = 1,

            /// <summary>
            /// Xml格式
            /// </summary>
            XmlFormate = 2,

            /// <summary>
            /// 文本
            /// </summary>
            TextPlain = 3,

            /// <summary>
            /// HTML
            /// </summary>
            HtmlFormate = 4,

            /// <summary>
            /// 二进制数据流
            /// </summary>
            Stream = 5,
        }
    }
}
