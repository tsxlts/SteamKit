using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 拒绝报价响应
    /// </summary>
    public class DeclineOfferResponse
    {
        /// <summary>
        /// 报价Id
        /// </summary>
        [JsonProperty("tradeofferid")]
        public string? TradeOfferId { get; set; }
    }
}
