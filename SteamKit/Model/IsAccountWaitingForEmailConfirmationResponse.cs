using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 是否等待邮箱确认响应
    /// </summary>
    public class IsAccountWaitingForEmailConfirmationResponse
    {
        /// <summary>
        /// 等待邮箱确认
        /// </summary>
        [JsonProperty("awaiting_email_confirmation")]
        public bool AwaitingEmailConfirmation { get; set; }

        /// <summary>
        /// 等待时间
        /// 秒
        /// </summary>
        [JsonProperty("seconds_to_wait")]
        public int SecondsToWait { get; set; }
    }
}
