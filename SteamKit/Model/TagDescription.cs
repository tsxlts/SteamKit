using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产描述信息
    /// </summary>
    public class TagDescription : BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("owner_descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetDescription> OwnerDescriptions { get; set; } = new List<AssetDescription>();

        /// <summary>
        /// actions
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> Actions { get; set; } = new List<AssetAction>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tag> Tags { get; set; } = new List<Tag>();

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
            [JsonProperty("localized_category_name")]
            public string LocalizedCategoryName { get; set; } = string.Empty;

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
            [JsonProperty("localized_tag_name")]
            public string LocalizedTagName { get; set; } = string.Empty;

            /// <summary>
            /// 属性颜色
            /// </summary>
            [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
            public string Color { get; set; } = string.Empty;
        }
    }
}
