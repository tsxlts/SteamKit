using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 库存描述
    /// </summary>
    public class SelfInventoryDescription : BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_drag_url")]
        public string IconDragUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_tradable_restriction")]
        public string MarketTradableRestriction { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("owner_descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetDescription> OwnerDescriptions { get; set; } = new List<AssetDescription>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> Actions { get; set; } = new List<AssetAction>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> MarketActions { get; set; } = new List<AssetAction>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetClassInfo.Tag> Tags { get; set; } = new List<AssetClassInfo.Tag>();
    }
}
