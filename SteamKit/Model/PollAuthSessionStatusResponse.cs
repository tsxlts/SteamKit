using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 登录授权响应数据
    /// </summary>
    public class PollAuthSessionStatusResponse
    {
        /// <summary>
        /// AccountName
        /// </summary>
        [JsonProperty("account_name")]
        public string AccountName { get; set; } = string.Empty;

        /// <summary>
        /// RefreshToken
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// AccessToken
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// HadRemoteInteraction
        /// </summary>
        [JsonProperty("had_remote_interaction")]
        public bool HadRemoteInteraction { get; set; }

        /// <summary>
        /// 新的令牌信息
        /// </summary>
        [JsonProperty("new_guard_data")]
        public string NewGuardData { get; set; } = string.Empty;

        /// <summary>
        /// 协议地址
        /// </summary>
        [JsonProperty("agreement_session_url")]
        public string? AgreementSessionUrl { get; set; }

        /// <summary>
        /// 新的ClientId
        /// </summary>
        [JsonProperty("new_client_id")]
        public string? NewClientId { get; set; }

        /// <summary>
        /// 二维码登录新的二维码
        /// </summary>
        [JsonProperty("new_challenge_url")]
        public string? NewChallengeUrl { get; set; }
    }
}
