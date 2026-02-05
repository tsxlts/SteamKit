using Newtonsoft.Json;
using static SteamKit.Model.AssetClassInfo;

namespace SteamKit.Model
{
    /// <summary>
    /// 库存历史资产描述
    /// </summary>
    public class InventoryHistoryDescription : BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
