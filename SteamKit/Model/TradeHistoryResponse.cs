using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 交易历史响应
    /// </summary>
    public class TradeHistoryResponse : TradeStatusResponse
    {
        /// <summary>
        /// 是否还有更多
        /// </summary>
        [JsonProperty("more")]

        public bool More { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty("total_trades")]
        public int TotalTrades { get; set; }
    }
}
