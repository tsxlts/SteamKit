
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 令牌码确认登录响应
    /// </summary>
    public class UpdateAuthSessionWithSteamGuardCodeResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("agreement_session_url")]
        public string? AgreementSessionUrl { get; set; }
    }
}
