
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产属性
    /// </summary>
    public class AssetProperty
    {
        /// <summary>
        /// AppId
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// ContextId
        /// </summary>
        [JsonProperty("contextid")]
        public ulong ContextId { get; set; }

        /// <summary>
        /// AssetId
        /// </summary>
        [JsonProperty("assetid")]
        public ulong AssetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("asset_properties")]
        public List<Property> AssetProperties { get; set; } = new List<Property>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("asset_accessories")]
        public IEnumerable<AssetAccessory>? AssetAccessories { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public class Property
        {
            /// <summary>
            /// PropertyId
            /// </summary>
            [JsonProperty("propertyid")]
            public int PropertyId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name")]
            public string? Name { get; set; }

            /// <summary>
            /// FloatValue
            /// </summary>
            [JsonProperty("float_value")]
            public float? FloatValue { get; set; }

            /// <summary>
            /// IntValue
            /// </summary>
            [JsonProperty("int_value")]
            public long? IntValue { get; set; }

            /// <summary>
            /// StringValue
            /// </summary>
            [JsonProperty("string_value")]
            public string? StringValue { get; set; }
        }
    }
}
