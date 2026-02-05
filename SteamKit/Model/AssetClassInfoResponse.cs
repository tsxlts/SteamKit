using Newtonsoft.Json;
using SteamKit.Converter;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产Class响应数据
    /// </summary>
    public class AssetClassInfoResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// 资产Class信息
        /// </summary>
        public List<AssetClassInfo> Assets { get; set; } = new List<AssetClassInfo>();
    }

    /// <summary>
    /// 资产Class
    /// </summary>
    public class AssetClassInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_drag_url")]
        public string IconDragUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_name")]
        public string MarketName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name_color")]
        public string NameColor { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tradable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Tradable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("marketable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Marketable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("commodity")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Commodity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_tradable_restriction")]
        public string MarketTradableRestriction { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_buy_country_restriction")]
        public string MarketBuyCountryRestriction { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cache_expiration", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CacheExpiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("fraudwarnings")]
        public IDictionary<int, string>? FrauDwarnings { get; set; }

        /// <summary>
        /// actions
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<int, AssetAction> Actions { get; set; } = new Dictionary<int, AssetAction>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<int, AssetDescription> Descriptions { get; set; } = new Dictionary<int, AssetDescription>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("owner_descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<int, AssetDescription> OwnerDescriptions { get; set; } = new Dictionary<int, AssetDescription>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<int, Tag> Tags { get; set; } = new Dictionary<int, Tag>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("classid")]
        public ulong ClassId { get; set; }

        /// <summary>
        /// 请求时有传入响应时才返回
        /// </summary>
        [JsonProperty("instanceid")]
        public ulong InstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class Tag
        {
            /// <summary>
            /// 属性名标识
            /// 属性分类标识
            /// <para>例如：Weapon</para>
            /// </summary>
            [JsonProperty("category")]
            public string Category { get; set; } = string.Empty;

            /// <summary>
            /// 属性名称
            /// 属性分类名称
            /// <para>例如：武器</para>
            /// </summary>
            [JsonProperty("category_name")]
            public string CategoryName { get; set; } = string.Empty;

            /// <summary>
            /// 属性值标识
            /// <para>例如：weapon_taser</para>
            /// </summary>
            [JsonProperty("internal_name")]
            public string InternalName { get; set; } = string.Empty;

            /// <summary>
            /// 属性值名称
            /// <para>例如：宙斯 X27 电击枪</para>
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 属性颜色
            /// </summary>
            [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
            public string Color { get; set; } = string.Empty;
        }
    }
}
