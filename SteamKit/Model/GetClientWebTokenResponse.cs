using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 获取网页客户端登录Token响应
    /// </summary>
    public class GetClientWebTokenResponse
    {
        /// <summary>
        /// 是否已登录
        /// </summary>
        [JsonProperty("logged_in")]
        public bool LoggedIn { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        [JsonProperty("steamid")]
        public string? SteamId { get; set; }

        /// <summary>
        /// PartnerId
        /// </summary>
        [JsonProperty("accountid")]
        public string? AccountId { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [JsonProperty("account_name")]
        public string? AccountName { get; set; }

        /// <summary>
        /// 聊天登录Token
        /// </summary>
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
