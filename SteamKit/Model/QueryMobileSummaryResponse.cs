using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 获取手机端摘要信息响应
    /// </summary>
    public class QueryMobileSummaryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("stale_time_seconds")]
        public int StaleTimeSeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("is_authenticator_valid")]
        public bool IsAuthenticatorValid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("owned_games")]
        public int OwnedGames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("friend_count")]
        public int FriendCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_balance", NullValueHandling = NullValueHandling.Ignore)]
        public string WalletBalance { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; } = string.Empty;
    }
}
