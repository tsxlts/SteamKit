
namespace SteamKit.Client.Model
{
    /// <summary>
    /// 协议
    /// </summary>
    public enum ProtocolTypes
    {
        /// <summary>
        /// TCP
        /// </summary>
        Tcp = 1 << 0,

        /// <summary>
        /// UDP
        /// </summary>
        Udp = 1 << 1,

        /// <summary>
        /// WebSockets (HTTP / TLS)
        /// </summary>
        WebSocket = 1 << 2,

        /// <summary>
        /// All available protocol types
        /// </summary>
        All = Tcp | Udp | WebSocket
    }
}
