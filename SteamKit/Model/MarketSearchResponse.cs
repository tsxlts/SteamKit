using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场搜索响应
    /// </summary>
    public class MarketSearchResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<MarketGoods> Results { get; set; } = new List<MarketGoods>();
    }

    /// <summary>
    /// 市场商品
    /// </summary>
    public class MarketGoods
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_hash_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketHashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_name", NullValueHandling = NullValueHandling.Ignore)]
        public string AppName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url", NullValueHandling = NullValueHandling.Ignore)]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_type", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketType { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("min_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("listing_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ListingCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("search_score", NullValueHandling = NullValueHandling.Ignore)]
        public int SearchScore { get; set; }
    }
}
