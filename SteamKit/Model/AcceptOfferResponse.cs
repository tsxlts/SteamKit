using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 接受报价响应
    /// </summary>
    public class AcceptOfferResponse
    {
        /// <summary>
        /// 交易Id
        /// </summary>
        [JsonProperty("tradeid")]
        public string? TradeId { get; set; }

        /// <summary>
        /// 是否需要手机确认
        /// </summary>
        [JsonProperty("needs_mobile_confirmation")]
        public bool NeedMobileConfirmation { get; set; }

        /// <summary>
        /// 是否需要邮箱确认
        /// </summary>
        [JsonProperty("needs_email_confirmation")]
        public bool NeedEmailConfirmation { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("strError")]
        public string? Error { get; set; }
    }
}
