using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 发送报价资产信息
    /// </summary>
    public class SendOfferAsset
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("contextid")]
        public string ContextId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assetid")]
        public string AssetId { get; set; } = string.Empty;
    }
}
