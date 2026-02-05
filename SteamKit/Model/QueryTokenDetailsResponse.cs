using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询登录Token详情响应
    /// </summary>
    public class QueryTokenDetailsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("steamid")]
        public string SteamId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("expiration")]
        public long Expiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; } = string.Empty;
    }
}
