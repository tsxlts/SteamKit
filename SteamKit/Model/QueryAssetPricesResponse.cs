using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询游戏内物品价格响应
    /// </summary>
    public class QueryAssetPricesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public AssetPricesResponse Result { get; set; } = new AssetPricesResponse();
    }

    /// <summary>
    /// 
    /// </summary>
    public class AssetPricesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetPrices> Assets { get; set; } = new List<AssetPrices>();

        /// <summary>
        /// 
        /// </summary>
        public class AssetPrices
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("prices", NullValueHandling = NullValueHandling.Ignore)]
            public IDictionary<string, ulong> Prices { get; set; } = new Dictionary<string, ulong>();

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime Date { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("class", NullValueHandling = NullValueHandling.Ignore)]
            public List<AssetCalss> Class { get; set; } = new List<AssetCalss>();

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("classid", NullValueHandling = NullValueHandling.Ignore)]
            public ulong Classid { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class AssetCalss
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            public string Value { get; set; } = string.Empty;
        }
    }
}
