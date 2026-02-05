using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 刷新登录Token响应
    /// </summary>
    public class RefreshTokenResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 错误状态码
        /// Success为false生效
        /// </summary>
        [JsonProperty("error")]
        public ErrorCodes Error { get; set; } = ErrorCodes.OK;

        /// <summary>
        /// 保存Token地址
        /// </summary>
        [JsonProperty("login_url")]
        public string? LoginUrl { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        [JsonProperty("steamID")]
        public string? SteamId { get; set; }

        /// <summary>
        /// 随机码
        /// </summary>
        [JsonProperty("nonce")]
        public string? Nonce { get; set; }

        /// <summary>
        /// 授权Token
        /// </summary>
        [JsonProperty("auth")]
        public string? Auth { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        [JsonProperty("redir")]
        public string? Redir { get; set; }
    }
}
