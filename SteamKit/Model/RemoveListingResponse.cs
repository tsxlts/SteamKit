using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 下架商品响应
    /// </summary>
    public class RemoveListingResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sessionid")]
        public string SessionId { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
