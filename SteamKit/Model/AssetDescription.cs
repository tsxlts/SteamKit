using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产描述信息
    /// </summary>
    public class AssetDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_data")]
        public AppDataInfo? AppData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class AppDataInfo
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("def_index")]
            public string DefIndex { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("is_itemset_name")]
            public string IsItemsetName { get; set; } = string.Empty;
        }
    }
}
