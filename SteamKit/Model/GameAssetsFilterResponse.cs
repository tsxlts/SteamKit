using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产筛选器响应
    /// </summary>
    public class GameAssetsFilterResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public List<GameAssetsFilter> Filters { get; set; } = new List<GameAssetsFilter>();
    }

    /// <summary>
    /// 筛选条件
    /// </summary>
    public class GameAssetsFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 筛选条件Key
        /// </summary>
        public string CategoryKey { get; set; } = string.Empty;

        /// <summary>
        /// 属性名标识
        /// 属性分类标识
        /// <para>例如：Weapon</para>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称
        /// 属性分类名称
        /// <para>例如：武器</para>
        /// </summary>
        public string LocalizedName { get; set; } = string.Empty;

        /// <summary>
        /// 属性值
        /// </summary>
        public List<Tag> Tags { get; set; } = new List<Tag>();

        /// <summary>
        /// 
        /// </summary>
        public class Tag
        {
            /// <summary>
            /// 筛选条件Key
            /// </summary>
            public string TagKey { get; set; } = string.Empty;

            /// <summary>
            /// 属性值标识
            /// <para>例如：weapon_taser</para>
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 属性值名称
            /// <para>例如：宙斯 X27 电击枪</para>
            /// </summary>
            [JsonProperty("localized_name")]
            public string LocalizedName { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("matches")]
            public string Matches { get; set; } = string.Empty;
        }
    }
}
