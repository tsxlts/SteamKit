using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场商品列表响应
    /// </summary>
    public class MarketListingsResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public List<MarketListings> Listings { get; set; } = new List<MarketListings>();

        /// <summary>
        /// 资产
        /// </summary>
        public List<MarketListingsDescription> Assets { get; set; } = new List<MarketListingsDescription>();
    }

    /// <summary>
    /// 市场商品
    /// </summary>
    public class MarketListings
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("listingid")]
        public string ListingId { get; set; } = string.Empty;

        /// <summary>
        /// 卖家货币类型Id
        /// </summary>
        [JsonProperty("currencyid")]
        public int CurrencyId { get; set; }

        /// <summary>
        /// 卖家出售价格
        /// 卖家实际到账价格
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// 总手续费金额
        /// </summary>
        [JsonProperty("fee")]
        public int Fee { get; set; }

        /// <summary>
        /// 游戏平台收取手续费费用
        /// </summary>
        [JsonProperty("publisher_fee")]
        public int PublisherFee { get; set; }

        /// <summary>
        /// 平台收取手续费费用
        /// </summary>
        [JsonProperty("steam_fee")]
        public int SteamFee { get; set; }

        /// <summary>
        /// 转换后的货币类型Id
        /// </summary>
        [JsonProperty("converted_currencyid")]
        public int ConvertedCurrencyId { get; set; }

        /// <summary>
        /// 转换后的卖家出售价格
        /// 卖家实际到账价格
        /// </summary>
        [JsonProperty("converted_price")]
        public int ConvertedPrice { get; set; }

        /// <summary>
        /// 转换后的总手续费金额
        /// </summary>
        [JsonProperty("converted_fee")]
        public int ConvertedFee { get; set; }

        /// <summary>
        /// 转换后的游戏平台收取手续费费用
        /// </summary>
        [JsonProperty("converted_publisher_fee")]
        public int ConvertedPublisherFee { get; set; }

        /// <summary>
        /// 转换后的Steam平台收取手续费费用
        /// </summary>
        [JsonProperty("converted_steam_fee")]
        public int ConvertedSteamFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("asset")]
        public MarketGoodsAsset Asset { get; set; } = new MarketGoodsAsset();

        /// <summary>
        /// 资产
        /// </summary>
        public class MarketGoodsAsset
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("appid")]
            public string AppId { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("contextid")]
            public ulong ContextId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("amount")]
            public int Amount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("currency")]
            public int Currency { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("market_actions")]
            public List<AssetAction> MarketActions { get; set; } = new List<AssetAction>();
        }
    }
}
