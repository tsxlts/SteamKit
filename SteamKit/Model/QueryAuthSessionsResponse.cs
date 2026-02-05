using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询待确认的登录授权信息响应
    /// </summary>
    public class QueryAuthSessionsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("client_ids")]
        public List<ulong> ClientIds { get; set; } = new List<ulong>();
    }
}
