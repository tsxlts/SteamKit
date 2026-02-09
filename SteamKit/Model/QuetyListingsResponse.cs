using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询出售的商品和订购单响应
    /// </summary>
    public class QuetyListingsResponse
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
        /// 
        /// </summary>
        [JsonProperty("num_active_listings")]
        public int NumActiveListings { get; set; }

        /// <summary>
        /// 已上架商品
        /// </summary>
        [JsonProperty("listings")]
        public List<MarketListing> Listings { get; set; } = new List<MarketListing>();

        /// <summary>
        /// 等待确认上架的商品
        /// </summary>
        [JsonProperty("listings_to_confirm")]
        public List<MarketListing> ListingsToConfirm { get; set; } = new List<MarketListing>();

        /// <summary>
        /// 市场暂挂的商品
        /// </summary>
        [JsonProperty("listings_on_hold")]
        public List<MarketListing> ListingsOnHold { get; set; } = new List<MarketListing>();

        /// <summary>
        /// 订购单
        /// </summary>
        [JsonProperty("buy_orders")]
        public List<MarketBuyOrder> BuyOrders { get; set; } = new List<MarketBuyOrder>();
    }

    /// <summary>
    /// 出售商品
    /// </summary>
    public class MarketListing
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("listingid")]
        public string ListingId { get; set; } = string.Empty;

        /// <summary>
        /// 卖家货币类型Id
        /// </summary>
        [JsonProperty("currencyid")]
        public int CurrencyId { get; set; }

        /// <summary>
        /// 出售价格
        /// 实际到账金额
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// 手续费总金额
        /// </summary>
        [JsonProperty("fee")]
        public int Fee { get; set; }

        /// <summary>
        /// 游戏平台收取手续费费用
        /// </summary>
        [JsonProperty("publisher_fee")]
        public int PublisherFee { get; set; }

        /// <summary>
        /// 平台收取手续费费用
        /// </summary>
        [JsonProperty("steam_fee")]
        public int SteamFee { get; set; }

        /// <summary>
        /// 转换后的货币类型Id
        /// </summary>
        [JsonProperty("converted_currencyid")]
        public int ConvertedCurrencyId { get; set; }

        /// <summary>
        /// 转换后出售价格
        /// 实际到账金额
        /// </summary>
        [JsonProperty("converted_price")]
        public int ConvertedPrice { get; set; }

        /// <summary>
        /// 转换后的手续费总金额
        /// </summary>
        [JsonProperty("converted_fee")]
        public int ConvertedFee { get; set; }

        /// <summary>
        /// 转换后的游戏平台收取手续费费用
        /// </summary>
        [JsonProperty("converted_publisher_fee")]
        public int ConvertedPublisherFee { get; set; }

        /// <summary>
        /// 转换后的Steam平台收取手续费费用
        /// </summary>
        [JsonProperty("converted_steam_fee")]
        public int ConvertedSteamFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_created")]
        public long TimeCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// 资产描述
        /// </summary>
        [JsonProperty("asset")]
        public ListingAssetDescription Asset { get; set; } = new ListingAssetDescription();
    }

    /// <summary>
    /// 订购单
    /// </summary>
    public class MarketBuyOrder
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hash_name")]
        public string HashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_currency")]
        public Enums.Currency WalletCurrency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("quantity_remaining")]
        public int QuantityRemaining { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("buy_orderid")]
        public string BuyOrderId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("description")]
        public BaseDescription Description { get; set; } = new BaseDescription();
    }
}
