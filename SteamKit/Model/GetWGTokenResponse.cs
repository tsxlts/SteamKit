using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 获取WGToken响应
    /// </summary>
    public class GetWGTokenResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_secure")]
        public string TokenSecure { get; set; } = string.Empty;
    }
}
