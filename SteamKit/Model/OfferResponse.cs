using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 报价查询响应
    /// </summary>
    public class OffersResponse
    {
        /// <summary>
        /// 发送的报价
        /// </summary>
        [JsonProperty("trade_offers_sent", NullValueHandling = NullValueHandling.Ignore)]
        public List<Offer> TradeOffersSent { get; set; } = new List<Offer>();

        /// <summary>
        /// 收到的报价
        /// </summary>
        [JsonProperty("trade_offers_received", NullValueHandling = NullValueHandling.Ignore)]
        public List<Offer> TradeOffersReceived { get; set; } = new List<Offer>();

        /// <summary>
        /// 资产描述
        /// 只包含交易中的报价资产信息
        /// </summary>
        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<TagDescription> Descriptions { get; set; } = new List<TagDescription>();

        /// <summary>
        /// 下一页请求索引
        /// </summary>
        [JsonProperty("next_cursor")]
        public ulong NextCursor { get; set; }
    }

    /// <summary>
    /// 报价查询响应
    /// </summary>
    public class OfferResponse
    {
        /// <summary>
        /// 报价
        /// </summary>
        [JsonProperty("offer")]
        public Offer? TradeOffer { get; set; }

        /// <summary>
        /// 资产描述
        /// 只包含交易中的报价资产信息
        /// </summary>
        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<TagDescription> Descriptions { get; set; } = new List<TagDescription>();
    }

    /// <summary>
    /// 报价
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tradeofferid")]
        public string TradeOfferId { get; set; } = string.Empty;

        /// <summary>
        /// SteamId等于该值加76561197960265728
        /// </summary>
        [JsonProperty("accountid_other")]
        public ulong AccountIdOther { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("expiration_time")]
        public long ExpirationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("trade_offer_state")]
        public TradeOfferState TradeOfferState { get; set; }

        /// <summary>
        /// 送出的物品
        /// </summary>
        [JsonProperty("items_to_give", NullValueHandling = NullValueHandling.Ignore)]
        public List<OfferAsset>? ItemsToGive { get; set; } = new List<OfferAsset>();

        /// <summary>
        /// 收到的物品
        /// </summary>
        [JsonProperty("items_to_receive", NullValueHandling = NullValueHandling.Ignore)]
        public List<OfferAsset>? ItemsToReceive { get; set; } = new List<OfferAsset>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("is_our_offer")]
        public bool IsOurOffer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_created")]
        public long TimeCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_updated")]
        public long TimeUpdated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("from_real_time_trade")]
        public bool FromRealTimeTrade { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("escrow_end_date")]
        public int EscrowEndDate { get; set; }

        /// <summary>
        /// 报价对方接受后 accepted的报价会有这个字段
        /// </summary>
        [JsonProperty("tradeid")]
        public string? TradeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("confirmation_method")]
        public TradeOfferConfirmationMethod ConfirmationMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("delay_settlement")]
        public bool DelaySettlement { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OfferAsset
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("contextid")]
        public ulong ContextId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assetid")]
        public ulong AssetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("classid")]
        public ulong ClassId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("instanceid")]
        public ulong InstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("missing")]
        public bool IsMissing { get; set; }
    }

    /// <summary>
    /// 报价确认方式
    /// </summary>
    public enum TradeOfferConfirmationMethod
    {
        /// <summary>
        /// 
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// 
        /// </summary>
        Email = 1,

        /// <summary>
        /// 
        /// </summary>
        MobileApp = 2
    }

    /// <summary>
    /// 报价状态
    /// </summary>
    public enum TradeOfferState
    {
        ///<summary>
        ///订单失效
        ///交易作废
        ///</summary>
        Invalid = 1,

        ///<summary>
        /// 交易有效
        /// 向对方发送交易后,交易发起的时间截止到现在没有超过14天
        ///</summary>
        Active = 2,

        ///<summary>
        /// 交易成功
        ///</summary>
        Accepted = 3,

        ///<summary>
        /// 对方进行了还价
        /// 交易作废
        ///</summary>
        Countered = 4,

        ///<summary>
        /// 交易过期
        /// 超过交易有限期
        /// 交易作废
        ///</summary>
        Expired = 5,

        ///<summary>
        /// 交易取消
        /// 发起方主动取消了交易
        ///</summary>
        Canceled = 6,

        ///<summary>
        /// 对方直接拒绝交易
        ///</summary>
        Declined = 7,

        /// <summary>
        /// 无效的条目
        /// 交易的物品已经失效
        /// 交易作废
        /// </summary>
        InvalidItems = 8,

        /// <summary>
        /// 等待二次确认
        /// </summary>
        NeedsConfirmation = 9,

        /// <summary>
        /// 二次验证时取消交易
        /// 交易作废
        /// </summary>

        CanceledBySecondFactor = 10,

        /// <summary>
        /// 交易暂挂
        /// 如果有一方未开通手机令牌或者开通手机令牌未满7天双方即便确认的交易那么物品也不会及时到达
        /// </summary>
        InEscrow = 11,

        /// <summary>
        /// 原因未知
        /// </summary>
        Unknown = 12
    }

    /// <summary>
    /// 报价状态
    /// </summary>
    public enum OfferStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,

        /// <summary>
        /// 等待对方接受
        /// </summary>
        等待接受 = 101,

        /// <summary>
        /// 等待令牌确认
        /// </summary>
        等待令牌确认 = 102,

        /// <summary>
        /// 交易暂挂
        /// </summary>
        交易暂挂 = 201,

        /// <summary>
        /// 交易成功
        /// </summary>
        交易成功 = 301,

        /// <summary>
        /// 报价发送方取消报价
        /// </summary>
        主动取消 = 401,

        /// <summary>
        /// 报价接收方拒绝报价
        /// </summary>
        对方拒绝 = 402,

        /// <summary>
        /// 报价接收方修改报价
        /// 报价接收方进行还价
        /// </summary>
        对方修改报价 = 403,

        /// <summary>
        /// 报价已失效
        /// </summary>
        交易失效 = 404,

        /// <summary>
        /// 报价长时间未处理已过期
        /// </summary>
        交易过期 = 405
    }
}
