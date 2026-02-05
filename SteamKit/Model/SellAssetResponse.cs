using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 出售资产响应
    /// </summary>
    public class SellAssetResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 是否需要确认
        /// </summary>
        [JsonProperty("requires_confirmation")]
        public bool RequiresConfirmation { get; set; }

        /// <summary>
        /// 是否需要手机确认
        /// </summary>
        [JsonProperty("needs_mobile_confirmation")]
        public bool NeedsMobileConfirmation { get; set; }

        /// <summary>
        /// 是否需要邮箱确认
        /// </summary>
        [JsonProperty("needs_email_confirmation")]
        public bool NeedsEmailConfirmation { get; set; }

        /// <summary>
        /// 邮箱域名
        /// </summary>
        [JsonProperty("email_domain")]
        public string? EmailDomain { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
