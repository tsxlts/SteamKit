using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产操作链接
    /// </summary>
    public class AssetAction
    {
        /// <summary>
        /// 链接
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
