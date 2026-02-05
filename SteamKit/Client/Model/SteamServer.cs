using System.Net;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// SteamServer
    /// </summary>
    public class SteamServer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="protocolTypes"></param>
        public SteamServer(EndPoint endPoint, ProtocolTypes protocolTypes)
        {
            EndPoint = endPoint;
            ProtocolTypes = protocolTypes;
        }

        /// <summary>
        ///
        /// </summary>
        public EndPoint EndPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProtocolTypes ProtocolTypes { get; set; }
    }
}
