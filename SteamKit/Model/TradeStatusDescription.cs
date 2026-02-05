using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 交易状态资产描述信息
    /// </summary>
    public class TradeStatusDescription : BaseDescription
    {
        /// <summary>
        /// actions
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> Actions { get; set; } = new List<AssetAction>();
    }
}
