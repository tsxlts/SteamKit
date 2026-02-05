using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 移除令牌验证器响应
    /// </summary>
    public class RemoveAuthenticatorResponse
    {
        /// <summary>
        /// 是否撤销成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 撤销失败后剩余尝试撤销次数
        /// </summary>
        [JsonProperty("revocation_attempts_remaining")]
        public int RevocationAttemptsRemaining { get; set; }
    }
}
