
namespace SteamKit.Model
{
    /// <summary>
    /// 是否看交易响应
    /// </summary>
    public class TradableCheckResponse
    {
        /// <summary>
        /// 是否可交易
        /// </summary>
        public bool Tradable { get; set; }

        /// <summary>
        /// 不可交易的原因
        /// </summary>
        public string? Reason { get; set; }
    }
}
