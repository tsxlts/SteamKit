using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 获取App登录Token响应
    /// </summary>
    public class GenerateAppAccessTokenResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; } = string.Empty;
    }
}
