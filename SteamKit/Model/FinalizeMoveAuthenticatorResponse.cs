
using Newtonsoft.Json;
using static SteamKit.SteamEnum;

namespace SteamKit.Model
{
    /// <summary>
    /// 确认移动令牌验证器响应
    /// </summary>
    public class FinalizeMoveAuthenticatorResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 新的令牌
        /// </summary>
        [JsonProperty("replacement_token")]
        public AuthenticatorToken? ReplacementToken { get; set; }
    }

    /// <summary>
    /// 令牌验证器信息
    /// </summary>
    public class AuthenticatorToken
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("steamid")]
        public string SteamId { get; set; } = string.Empty;

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
        [JsonProperty("steamguard_scheme")]
        public SteamGuardScheme GuardScheme { get; set; }
    }
}
