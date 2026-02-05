using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询商品最近动态信息响应
    /// </summary>
    public class QueryOrdersActivityResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 动态
        /// </summary>
        [JsonProperty("activity")]
        public List<string> Activity { get; set; } = new List<string>();

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
