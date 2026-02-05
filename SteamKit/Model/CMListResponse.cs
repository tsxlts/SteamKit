
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 服务器地址响应
    /// </summary>
    public class CMListResponse
    {
        /// <summary>
        /// Servers
        /// </summary>
        [JsonProperty("serverlist")]
        public IEnumerable<string>? Servers { get; set; }

        /// <summary>
        /// Websockets
        /// </summary>
        [JsonProperty("serverlist_websockets")]
        public IEnumerable<string>? Websockets { get; set; }

        /// <summary>
        /// Result
        /// </summary>
        [JsonProperty("result")]
        public int Result { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
