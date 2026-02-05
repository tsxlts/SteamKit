using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场价格响应
    /// </summary>
    public class MarketPriceResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("lowest_price")]
        public string? LowestPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("volume")]
        public string? Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("median_price")]
        public string? MedianPrice { get; set; }
    }
}
