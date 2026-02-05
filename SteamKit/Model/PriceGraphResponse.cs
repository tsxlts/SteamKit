
namespace SteamKit.Model
{
    /// <summary>
    /// 价格曲线响应
    /// </summary>
    public class PriceGraphResponse
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 售出数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string Currency { get; set; } = "$";
    }
}
