
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询交易报价摘要响应
    /// </summary>
    public class QueryTradeOffersSummaryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_received_count")]
        public int PendingReceivedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("new_received_count")]
        public int NewReceivedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("updated_received_count")]
        public int UpdatedReceivedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("historical_received_count")]
        public int HistoricalReceivedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_sent_count")]
        public int PendingSentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("newly_accepted_sent_count")]
        public int NewlyAcceptedSentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("updated_sent_count")]
        public int UpdatedSentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("historical_sent_count")]
        public int HistoricalSentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("escrow_received_count")]
        public int EscrowReceivedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("escrow_sent_count")]
        public int EscrowSentCount { get; set; }
    }
}
