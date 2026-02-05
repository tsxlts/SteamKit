using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场商品资产
    /// </summary>
    public class MarketListingsDescription : BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

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
        /// actions
        /// </summary>
        [JsonProperty("market_actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> MarketActions { get; set; } = new List<AssetAction>();

        /// <summary>
        /// properties
        /// </summary>
        [JsonProperty("asset_properties", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetProperty.Property> AssetProperties { get; set; } = new List<AssetProperty.Property>();
    }
}
