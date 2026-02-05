using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 资产
    /// </summary>
    public class Asset
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
        /// ClassId
        /// </summary>
        [JsonProperty("classid")]
        public ulong ClassId { get; set; }

        /// <summary>
        /// InstanceId
        /// </summary>
        [JsonProperty("instanceid")]
        public ulong InstanceId { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; } = 1;
    }
}
