using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 交易暂挂检测响应
    /// </summary>
    public class QueryTradeHoldDurationsResponse
    {
        /// <summary>
        /// 我提供物品被暂挂的时间
        /// </summary>
        [JsonProperty("my_escrow")]
        public EscrowResponse? MyEscrow { get; set; }

        /// <summary>
        /// 对方提供物品被暂挂的时间
        /// </summary>
        [JsonProperty("their_escrow")]
        public EscrowResponse? TheirEscrow { get; set; }

        /// <summary>
        /// 双方提供物品被暂挂的时间
        /// </summary>
        [JsonProperty("both_escrow")]
        public EscrowResponse? BothEscrow { get; set; }
    }

    /// <summary>
    /// 暂挂时间响应
    /// </summary>
    public class EscrowResponse
    {
        /// <summary>
        /// 暂挂时间
        /// 秒
        /// </summary>
        [JsonProperty("escrow_end_duration_seconds")]
        public int EscrowEndDurationSeconds { get; set; }

        /// <summary>
        /// 暂挂到期时间
        /// 秒时间戳
        /// </summary>
        [JsonProperty("escrow_end_date")]
        public long? EscrowEndDate { get; set; }

        /// <summary>
        /// 暂挂到期时间
        /// </summary>
        [JsonProperty("escrow_end_date_rfc3339")]
        public DateTime? EscrowEndDateRfc3339 { get; set; }
    }
}
