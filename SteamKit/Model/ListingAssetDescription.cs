using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 出售资产描述
    /// </summary>
    public class ListingAssetDescription : BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public ulong AssetId { get; set; }

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
    }
}
