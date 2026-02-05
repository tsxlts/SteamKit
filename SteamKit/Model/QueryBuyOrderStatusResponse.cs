using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询订购单状态响应
    /// </summary>
    public class QueryBuyOrderStatusResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 是否在订购中
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// 是否已完成订购
        /// </summary>
        [JsonProperty("purchased")]
        public bool Purchased { get; set; }

        /// <summary>
        /// 订购总数量
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 剩余订购数量
        /// </summary>
        [JsonProperty("quantity_remaining")]
        public int QuantityRemaining { get; set; }

        /// <summary>
        /// 订购单订单信息
        /// </summary>
        [JsonProperty("purchases")]
        public List<PurchaseOrder> Purchases { get; set; } = new List<PurchaseOrder>();
    }

    /// <summary>
    /// 订购单订单信息
    /// </summary>
    public class PurchaseOrder
    {
        /// <summary>
        /// 市场商品Id
        /// </summary>
        [JsonProperty("listingid")]
        public string ListingId { get; set; } = string.Empty;

        /// <summary>
        /// AppId
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// ContextId
        /// </summary>
        [JsonProperty("contextid")]
        public ulong ContextId { get; set; }

        /// <summary>
        /// 资产Id
        /// </summary>
        [JsonProperty("assetid")]
        public ulong AssetId { get; set; }

        /// <summary>
        /// 卖家账户Id
        /// </summary>
        [JsonProperty("accountid_seller")]
        public string SellerAccountId { get; set; } = string.Empty;

        /// <summary>
        /// 货币类型
        /// </summary>
        [JsonProperty("currency")]
        public SteamEnum.Currency currency { get; set; }

        /// <summary>
        /// 订单金额
        /// 单位：分
        /// </summary>
        [JsonProperty("price_total")]
        public int PriceTotal { get; set; }

        /// <summary>
        /// 卖家收款金额
        /// 单位：分
        /// </summary>
        [JsonProperty("price_subtotal")]
        public int PriceSubtotal { get; set; }

        /// <summary>
        /// 手续费
        /// 单位：分
        /// </summary>
        [JsonProperty("price_fee")]
        public int PriceFee { get; set; }
    }
}
