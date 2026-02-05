using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// Ras秘钥响应
    /// </summary>
    public class RsaResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("publickey_exp")]
        public string Exponent { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("publickey_mod")]
        public string Modulus { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
