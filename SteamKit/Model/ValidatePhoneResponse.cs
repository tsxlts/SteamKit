using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 验证手机号响应
    /// </summary>
    public class ValidatePhoneResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// 是否验证成功
        /// </summary>
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("is_voip")]
        public bool IsVoip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("is_fixed")]
        public bool IsFixed { get; set; }
    }
}
