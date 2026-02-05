
using System.Net;

namespace SteamKit
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiException : HttpRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatus"></param>
        /// <param name="message"></param>
        /// <param name="response"></param>
        public ApiException(HttpStatusCode httpStatus, string? message, string? response) : base(message, null, httpStatus)
        {
            Response = response;
        }

        /// <summary>
        /// 响应数据
        /// </summary>
        public string? Response { get; }
    }
}
