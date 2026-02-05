using Newtonsoft.Json;
using static SteamKit.SteamEnum;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询令牌验证器状态响应
    /// </summary>
    public class QueryAuthenticatorStatusResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("state")]
        public int State { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        [JsonProperty("device_identifier")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("steamguard_scheme")]
        public SteamGuardScheme GuardScheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_gid")]
        public string TokenGID { get; set; } = string.Empty;

        /// <summary>
        /// Version
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("inactivation_reason")]
        public int InactivationReason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("authenticator_type")]
        public int AuthenticatorType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("authenticator_allowed")]
        public bool AuthenticatorAllowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("email_validated")]
        public bool EmailValidated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_created")]
        public long TimeCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_transferred")]
        public long TimeTransferred { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("revocation_attempts_remaining")]
        public int RevocationAttemptsRemaining { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("classified_agent")]
        public string ClassifiedAgent { get; set; } = string.Empty;
    }
}
