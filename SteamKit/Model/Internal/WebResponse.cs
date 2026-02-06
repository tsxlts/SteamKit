using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SteamKit.Internal;

namespace SteamKit.Model.Internal
{
    internal class WebResponse<T> : WebResponse, IWebResponse<T>
    {
        private JsonConverter[] _converters;
        private T? dataVaule = default;

        public WebResponse(HttpResponseMessage response, params JsonConverter[] converters) : base(response)
        {
            _converters = converters;

            using (Stream stream = new MemoryStream())
            {
                Content.CopyTo(stream);
                Content.Seek(0, SeekOrigin.Begin);
                stream.Seek(0, SeekOrigin.Begin);

                using (StreamReader reader = new StreamReader(stream))
                {
                    string? responseBody = reader.ReadToEnd();

                    Response = responseBody;
                    if (string.IsNullOrWhiteSpace(responseBody))
                    {
                        dataVaule = default;
                        return;
                    }

                    if (typeof(string).Equals(typeof(T)))
                    {
                        dataVaule = (T)(object)responseBody;
                        return;
                    }

                    if (MediaTypeHeaderValue.Parse("text/html").MediaType!.Equals(response.Content.Headers.ContentType?.MediaType, StringComparison.CurrentCultureIgnoreCase))
                    {
                        dataVaule = default;
                        return;
                    }

                    try
                    {
                        dataVaule = JsonConvert.DeserializeObject<T>(responseBody, _converters);
                    }
                    catch (Exception ex)
                    {
                        var logger = LoggerBuilder.GetLogger();
                        logger?.LogException(ex, "WebResponse Deserialize Body Failed");
                        logger?.LogDebug("WebResponse Deserialize Body({0}) Failed, Response Body:\n{1}", typeof(T), Response);

                        throw;
                    }
                }
            }
        }

        protected WebResponse(string? response, T? data)
        {
            _converters = new JsonConverter[0];
            Response = response;
            Body = data;
        }

        /// <summary>
        /// Response
        /// </summary>
        public string? Response { get; set; }

        /// <summary>
        /// Response Model
        /// </summary>
        public T? Body
        {
            get
            {
                return dataVaule;
            }
            private set
            {
                dataVaule = value;
            }
        }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <typeparam name="Out"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public WebResponse<Out> Convert<Out>(Func<T?, Out?> factory)
        {
            return new WebResponse<Out>(Response, factory(Body))
            {
                RequestUri = RequestUri,
                HttpStatusCode = HttpStatusCode,
                Headers = Headers,
                Cookies = Cookies,
                Content = Content,
                Message = Message
            };
        }
    }

    internal class WebResponse : IWebResponse
    {
        private static HttpResponseMessage NoResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
        {
            RequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("https://localhost")),
        };

        public WebResponse() : this(NoResponseMessage)
        {
        }

        public WebResponse(HttpResponseMessage response)
        {
            RequestUri = response.RequestMessage!.RequestUri!;
            HttpStatusCode = response.StatusCode;
            Headers = response.Headers;

            Cookies = HttpUtils.GetCookies(response);

            Content = new MemoryStream();
            using (var stream = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
            {
                stream.CopyTo(Content);
            }
            Content.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Http状态码
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// Cookie
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public Stream Content { get; set; }
    }
}
