using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 取消报价响应
    /// </summary>
    public class CancelOfferResponse
    {
        /// <summary>
        /// 报价Id
        /// </summary>
        [JsonProperty("tradeofferid")]
        public string? TradeOfferId { get; set; }
    }
}
