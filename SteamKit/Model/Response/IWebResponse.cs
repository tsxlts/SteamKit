
using System.Net;
using System.Net.Http.Headers;

namespace SteamKit.Model
{
    /// <summary>
    /// Web响应数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWebResponse<T> : IWebResponse
    {
        /// <summary>
        /// Response
        /// </summary>
        public string? Response { get; }

        /// <summary>
        /// Response Model
        /// </summary>
        public T? Body { get; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string? Message { get; }
    }

    /// <summary>
    /// Web响应数据
    /// </summary>
    public interface IWebResponse : IResponse
    {
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
    }
}
