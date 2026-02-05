using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询商品售价数据响应
    /// </summary>
    public class QueryOrdersHistogramResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 最高求购价
        /// </summary>
        [JsonProperty("highest_buy_order")]
        public int HighestBuyOrder { get; set; }

        /// <summary>
        /// 最低出售价
        /// </summary>
        [JsonProperty("lowest_sell_order")]
        public int LowestSellOrder { get; set; }

        /// <summary>
        /// 求购价格数据
        /// </summary>
        public List<OrderGraph> BuyOrderGraph { get; set; } = new List<OrderGraph>();

        /// <summary>
        /// 出售价格数据
        /// </summary>
        public List<OrderGraph> SellOrderGraph { get; set; } = new List<OrderGraph>();
    }

    /// <summary>
    /// 商品订单价格数据
    /// </summary>
    public class OrderGraph
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Summary { get; set; } = string.Empty;
    }
}
