using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 创建订购单响应
    /// </summary>
    public class CreateBuyOrderResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 订购单Id
        /// </summary>
        [JsonProperty("buy_orderid")]
        public string? BuyOrderId { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
