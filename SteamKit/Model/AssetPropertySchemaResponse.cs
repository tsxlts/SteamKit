using Newtonsoft.Json;
using static SteamKit.Enums;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产属性结构响应
    /// </summary>
    public class AssetPropertySchemaResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("property_schemas", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetPropertySchema> PropertySchemas { get; set; } = new List<AssetPropertySchema>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class AssetPropertySchema
    {
        /// <summary>
        /// 属性Id
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }

        /// <summary>
        /// 属性名称标识
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称
        /// </summary>
        [JsonProperty("localized_label", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalizedLabel { get; set; } = string.Empty;

        /// <summary>
        /// 属性值类型
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public AssetPropertyType Type { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [JsonProperty("int_min", NullValueHandling = NullValueHandling.Ignore)]
        public long? IntMin { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [JsonProperty("int_max", NullValueHandling = NullValueHandling.Ignore)]
        public long? IntMax { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [JsonProperty("float_min", NullValueHandling = NullValueHandling.Ignore)]
        public float? FloatMin { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [JsonProperty("float_max", NullValueHandling = NullValueHandling.Ignore)]
        public float? FloatMax { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [JsonProperty("hide_from_description", NullValueHandling = NullValueHandling.Ignore)]
        public bool HideFromDescription { get; set; }
    }
}
