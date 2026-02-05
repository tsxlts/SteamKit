using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 验证令牌响应
    /// </summary>
    public class ValidateTokenResponse
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        [JsonProperty("valid")]
        public bool Valid { get; set; }
    }
}
