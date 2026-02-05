using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 发送报价响应
    /// </summary>
    public class SendOfferResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tradeofferid")]
        public string? TradeOfferId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("needs_mobile_confirmation")]
        public bool NeedsMobileConfirmation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("needs_email_confirmation")]
        public bool NeedsEmailConfirmation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("strError")]
        public string? Error { get; set; }
    }
}
