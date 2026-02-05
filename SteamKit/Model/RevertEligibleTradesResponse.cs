using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 撤销所有交易响应
    /// </summary>
    public class RevertEligibleTradesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("redir", NullValueHandling = NullValueHandling.Ignore)]
        public string Redir { get; set; } = string.Empty;
    }
}
