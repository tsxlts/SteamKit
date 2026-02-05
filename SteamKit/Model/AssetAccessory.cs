using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产附属物
    /// </summary>
    public class AssetAccessory
    {
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
        [JsonProperty("standalone_properties")]
        public IEnumerable<AssetProperty.Property>? StandaloneProperties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("parent_relationship_properties")]
        public IEnumerable<AssetProperty.Property>? ParentRelationshipProperties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nested_accessories")]
        public IEnumerable<AssetAccessory>? NestedAccessories { get; set; }
    }
}
