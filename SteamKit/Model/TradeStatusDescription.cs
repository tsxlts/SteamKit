using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 交易状态资产描述信息
    /// </summary>
    public class TradeStatusDescription : BaseDescription
    {
        /// <summary>   
        /// 
        /// </summary>
        [JsonProperty("market_bucket_group_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketBucketGroupId { get; set; } = string.Empty;

        /// <summary>   
        /// 
        /// </summary>
        [JsonProperty("market_bucket_group_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketBucketGroupName { get; set; } = string.Empty;

        /// <summary>
        /// actions
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AssetAction> Actions { get; set; } = new List<AssetAction>();
    }
}
