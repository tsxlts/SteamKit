using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询市场订单历史记录响应
    /// </summary>
    public class QuetyMarketHistoryResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 市场操作类型
        /// </summary>
        [JsonProperty("events")]
        public List<Event> Events { get; set; } = new List<Event>();

        /// <summary>
        /// 订单
        /// </summary>
        [JsonProperty("purchases")]
        public List<Purchase> Purchases { get; set; } = new List<Purchase>();

        /// <summary>
        /// 商品
        /// </summary>
        [JsonProperty("listings")]
        public List<Listing> Listings { get; set; } = new List<Listing>();

        /// <summary>
        /// 购买订单
        /// </summary>
        public class Purchase
        {
            /// <summary>
            /// 商品编号
            /// </summary>
            [JsonProperty("listingid")]
            public string ListingId { get; set; } = string.Empty;

            /// <summary>
            /// 订单编号
            /// </summary>
            [JsonProperty("purchaseid")]
            public string PurchaseId { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("time_sold")]
            public int TimeSold { get; set; }

            /// <summary>
            /// 买方SteamId
            /// </summary>
            [JsonProperty("steamid_purchaser")]
            public string SteamidPurchaser { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("needs_rollback")]
            public int NeedsRollback { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("failed")]
            public int Failed { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("asset")]
            public PurchaseAsset Asset { get; set; } = new PurchaseAsset();

            /// <summary>
            /// 支付订单金额
            /// 不包含手续费
            /// </summary>
            [JsonProperty("paid_amount")]
            public int PaidAmount { get; set; }

            /// <summary>
            /// 支付手续费总金额
            /// </summary>
            [JsonProperty("paid_fee")]
            public int PaidFee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("currencyid")]
            public string CurrencyId { get; set; } = string.Empty;

            /// <summary>
            /// Steam手续费
            /// </summary>
            [JsonProperty("steam_fee")]
            public int SteamFee { get; set; }

            /// <summary>
            /// 游戏手续费
            /// </summary>
            [JsonProperty("publisher_fee")]
            public int PublisherFee { get; set; }

            /// <summary>
            /// 收取手续费游戏Id
            /// </summary>
            [JsonProperty("publisher_fee_app")]
            public string PublisherFeeApp { get; set; } = string.Empty;

            /// <summary>
            /// 游戏手续费费率
            /// </summary>
            [JsonProperty("publisher_fee_percent")]
            public decimal PublisherFeePercent { get; set; }

            /// <summary>
            /// 转换当前货币后实际收款金额
            /// </summary>
            [JsonProperty("received_amount")]
            public int ReceivedAmount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("received_currencyid")]
            public string ReceivedCurrencyId { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("funds_held")]
            public int FundsHeld { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("time_funds_held_until")]
            public long TimeFundsHeldUntil { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("funds_revoked")]
            public int FundsRevoked { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("funds_returned")]
            public int FundsReturned { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("added_tax")]
            public int AddedTax { get; set; }

            /// <summary>
            /// 购买订单资产信息
            /// </summary>
            public class PurchaseAsset
            {
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
                [JsonProperty("id")]
                public ulong Id { get; set; }

                /// <summary>
                /// Amount
                /// </summary>
                [JsonProperty("amount")]
                public int Amount { get; set; } = 1;

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
                [JsonProperty("status")]
                public int Status { get; set; }

                /// <summary>
                /// 
                /// </summary>
                [JsonProperty("new_contextid")]
                public string NewContextid { get; set; } = string.Empty;

                /// <summary>
                /// 新资产Id
                /// </summary>
                [JsonProperty("new_id")]
                public ulong NewId { get; set; }

                /// <summary>
                /// 
                /// </summary>
                [JsonProperty("currency")]
                public int Currency { get; set; }
            }
        }

        /// <summary>
        /// 商品
        /// </summary>
        public class Listing
        {
            /// <summary>
            /// 商品编号
            /// </summary>
            [JsonProperty("listingid")]
            public string ListingId { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("price")]
            public int Price { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("fee")]
            public int Fee { get; set; }

            /// <summary>
            /// 收取手续费游戏Id
            /// </summary>
            [JsonProperty("publisher_fee_app")]
            public string PublisherFeeApp { get; set; } = string.Empty;

            /// <summary>
            /// 游戏手续费费率
            /// </summary>
            [JsonProperty("publisher_fee_percent")]
            public decimal PublisherFeePercent { get; set; }

            /// <summary>
            /// 原始订单金额
            /// 分
            /// </summary>
            [JsonProperty("original_price")]
            public int OriginalPrice { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("currencyid")]
            public int CurrencyId { get; set; }

            /// <summary>
            /// 资产
            /// </summary>
            [JsonProperty("asset")]
            public ListingAsset Asset { get; set; } = new ListingAsset();

            /// <summary>
            /// 商品资产
            /// </summary>
            public class ListingAsset
            {
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
                [JsonProperty("id")]
                public ulong Id { get; set; }

                /// <summary>
                /// Amount
                /// </summary>
                [JsonProperty("amount")]
                public int Amount { get; set; } = 1;

                /// <summary>
                /// 
                /// </summary>
                [JsonProperty("currency")]
                public int Currency { get; set; }
            }
        }

        /// <summary>
        /// 市场操作
        /// </summary>
        public class Event
        {
            /// <summary>
            /// 商品编号
            /// </summary>
            [JsonProperty("listingid")]
            public string ListingId { get; set; } = string.Empty;

            /// <summary>
            /// 订单编号
            /// </summary>
            [JsonProperty("purchaseid")]
            public string PurchaseId { get; set; } = string.Empty;

            /// <summary>
            /// 操作类型
            /// 出售：3
            /// 购买：4
            /// </summary>
            [JsonProperty("event_type")]
            public int EventType { get; set; }

            /// <summary>
            /// 操作时间
            /// </summary>
            [JsonProperty("time_event")]
            public long TimeEvent { get; set; }

            /// <summary>
            /// 操作时间毫秒及其之后的部分
            /// </summary>
            [JsonProperty("time_event_fraction")]
            public long TimeEventFraction { get; set; }

            /// <summary>
            /// 买方SteamId
            /// </summary>
            [JsonProperty("steamid_actor")]
            public string SteamidActor { get; set; } = string.Empty;

            /// <summary>
            /// 操作日期
            /// </summary>
            [JsonProperty("date_event")]
            public string DateEvent { get; set; } = string.Empty;
        }
    }
}
