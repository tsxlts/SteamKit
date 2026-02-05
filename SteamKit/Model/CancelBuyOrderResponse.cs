using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 取消订购单响应
    /// </summary>
    public class CancelBuyOrderResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("error")]
        public string? Error { get; set; }
    }
}
