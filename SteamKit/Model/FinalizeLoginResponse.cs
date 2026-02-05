
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 完成登录响应
    /// </summary>
    public class FinalizeLoginResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; } = true;

        /// <summary>
        /// 错误状态码
        /// Success为false生效
        /// </summary>
        [JsonProperty("error")]
        public ErrorCodes Error { get; set; } = ErrorCodes.OK;

        /// <summary>
        /// SteamId
        /// </summary>
        [JsonProperty("steamID")]
        public string? SteamId { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        [JsonProperty("redir")]
        public string? Redir { get; set; }

        /// <summary>
        /// 保存登录Token子域信息
        /// </summary>
        [JsonProperty("transfer_info")]
        public List<TransferInfo>? Transfer { get; set; }

        /// <summary>
        /// 主域地址
        /// </summary>
        [JsonProperty("primary_domain")]
        public string? PrimaryDomain { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// 保存登录Token子域信息
        /// </summary>
        public class TransferInfo
        {
            /// <summary>
            /// 登录Token请求地址
            /// </summary>
            [JsonProperty("url")]
            public string Url { get; set; } = string.Empty;

            /// <summary>
            /// 登录Token请求参数
            /// </summary>
            [JsonProperty("params")]
            public TransferParams? Params { get; set; }
        }

        /// <summary>
        /// 登录Token请求参数
        /// </summary>
        public class TransferParams
        {
            /// <summary>
            /// 随机码
            /// </summary>
            [JsonProperty("nonce")]
            public string Nonce { get; set; } = string.Empty;

            /// <summary>
            /// 授权Token
            /// </summary>
            [JsonProperty("auth")]
            public string Auth { get; set; } = string.Empty;
        }
    }
}
