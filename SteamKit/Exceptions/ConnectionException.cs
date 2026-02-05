
using System.Net;
using SteamKit.Client.Model;

namespace SteamKit
{
    /// <summary>
    /// 连接错误
    /// </summary>
    public class ConnectionException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="protocol"></param>
        public ConnectionException(EndPoint point, ProtocolTypes protocol) : this(point, protocol, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        public ConnectionException(EndPoint point, ProtocolTypes protocol, string? message) : this(point, protocol, message, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConnectionException(EndPoint point, ProtocolTypes protocol, string? message, Exception? innerException) : base(message, innerException)
        {
            EndPoint = point;
            ProtocolType = protocol;
        }

        /// <summary>
        /// 
        /// </summary>
        public EndPoint EndPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProtocolTypes ProtocolType { get; set; }
    }
}
