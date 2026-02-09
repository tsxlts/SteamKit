using Newtonsoft.Json;
using static SteamKit.Enums;

namespace SteamKit.Model
{
    /// <summary>
    /// 添加令牌验证器响应
    /// </summary>
    public class AddAuthenticatorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("shared_secret")]
        public string SharedSecret { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("identity_secret")]
        public string IdentitySecret { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("revocation_code")]
        public string RevocationCode { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("uri")]
        public string URI { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("server_time")]
        public long ServerTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("account_name")]
        public string AccountName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_gid")]
        public string TokenGID { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("secret_1")]
        public string Secret1 { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public AddAuthenticatorStatus Status { get; set; }

        /// <summary>
        /// 验证方式
        /// </summary>
        [JsonProperty("confirm_type")]
        public AddAuthenticatorConfirmType ConfirmType { get; set; }
    }
}
