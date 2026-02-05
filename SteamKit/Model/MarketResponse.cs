using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场查询响应
    /// </summary>
    public class MarketResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("results")]
        public List<MarketAsset>? Results { get; set; } = new List<MarketAsset>();
    }

    /// <summary>
    /// 市场资产
    /// </summary>
    public class MarketAsset
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hash_name")]
        public string HashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sell_listings")]
        public int SellListings { get; set; }

        /// <summary>
        /// 分
        /// </summary>
        [JsonProperty("sell_price")]
        public int SellPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sell_price_text")]
        public string SellPriceText { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_icon")]
        public string AppIcon { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_name")]
        public string AppName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("asset_description")]
        public Asset AssetDescription { get; set; } = new Asset();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sale_price_text")]
        public string SalePriceText { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public class Asset
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("appid")]
            public string AppId { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("classid")]
            public ulong ClassId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("instanceid")]
            public ulong InstanceId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("background_color")]
            public string BackgroundColor { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("icon_url")]
            public string IconUrl { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("tradable")]
            public bool Tradable { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name_color")]
            public string NameColor { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("market_name")]
            public string MarketName { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("market_hash_name")]
            public string MarketHashName { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("commodity")]
            public bool Commodity { get; set; }
        }
    }

    /// <summary>
    /// 市场查询筛选条件
    /// </summary>
    public class AssetFilter
    {
        /// <summary>
        /// 分类Key
        /// <para>
        /// category_<see cref="GameAssetsFilter.CategoryKey"/>
        /// </para>
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 分类标签
        /// <para>
        /// tag_<see cref="GameAssetsFilter.Tag.TagKey"/>
        /// </para>
        /// </summary>
        public IEnumerable<string>? TagValues { get; set; }
    }
}
