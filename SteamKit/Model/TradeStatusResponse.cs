using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 交易状态响应
    /// </summary>
    public class TradeStatusResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("trades")]
        public IEnumerable<Trade>? Trades { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions")]
        public IEnumerable<TradeStatusDescription>? Descriptions { get; set; }
    }

    /// <summary>
    /// 交易信息
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tradeid")]
        public string TradeId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("steamid_other")]
        public string SteamIdOther { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public TradeStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_init")]
        public long TimeInit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_mod")]
        public long TimeMod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_settlement")]
        public long? TimeSettlement { get; set; }

        /// <summary>
        /// 交易暂挂到期时间
        /// </summary>
        [JsonProperty("time_escrow_end")]
        public long? TimeEscrowEnd { get; set; }

        /// <summary>
        /// 被回滚的交易Id
        /// 回滚之前的交易Id
        /// </summary>
        [JsonProperty("rollback_trade")]
        public string? RollbackTrade { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assets_received")]
        public IEnumerable<NewAsset>? AssetsReceived { get; set; } = new List<NewAsset>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assets_given")]
        public IEnumerable<NewAsset>? AssetsGiven { get; set; } = new List<NewAsset>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class NewAsset : Asset
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("new_assetid")]
        public ulong NewAssetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("new_contextid")]
        public string NewContextId { get; set; } = string.Empty;

        /// <summary>
        /// 交易取消报价回滚后的资产Id
        /// </summary>
        [JsonProperty("rollback_new_assetid")]
        public ulong? RollbackNewAssetId { get; set; }

        /// <summary>
        /// 交易取消报价回滚后的ContextId
        /// </summary>
        [JsonProperty("rollback_new_contextid")]
        public string? RollbackNewContextId { get; set; }
    }

    /// <summary>
    /// 报价交易状态
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// 报价刚刚被接受/确认
        /// 但尚未完成交易
        /// </summary>
        Init = 0,

        /// <summary>
        /// Steam开始交易
        /// </summary>
        PreCommitted = 1,

        /// <summary>
        /// 物品已经交换了
        /// </summary>
        Committed = 2,

        /// <summary>
        /// 交易完成
        /// </summary>
        Complete = 3,

        /// <summary>
        /// 在Init之后但在Committed之前出现了问题，交易被回滚
        /// </summary>
        Failed = 4,

        /// <summary>
        /// 一位支持人员撤销了对一方的交易
        /// </summary>
        PartialSupportRollback = 5,

        /// <summary>
        /// 一位支持人士撤销了双方的交易
        /// </summary>
        FullSupportRollback = 6,

        /// <summary>
        /// 一位支持人员撤销了某些商品的交易
        /// </summary>
        SupportRollbackSelective = 7,

        /// <summary>
        /// 当交易失败时，我们试图回滚交易，但还没有对所有项目都做到这一点
        /// </summary>
        RollbackFailed = 8,

        /// <summary>
        /// 我们试图撤销交易，但一些失败并没有消失，我们放弃了
        /// </summary>
        RollbackAbandoned = 9,

        /// <summary>
        /// 交易暂挂
        /// </summary>
        InEscrow = 10,

        /// <summary>
        /// 交易暂估期取消交易
        /// </summary>
        EscrowRollback = 11,

        /// <summary>
        /// 用户撤销交易
        /// </summary>
        Reversed = 12
    }
}
