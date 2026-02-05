
namespace SteamKit.Model
{
    /// <summary>
    /// 查询用户是否可交易响应
    /// </summary>
    public class CanTradableResponse
    {
        /// <summary>
        /// 是否可交易
        /// </summary>
        public bool Tradable { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string? Message { get; set; }
    }
}
