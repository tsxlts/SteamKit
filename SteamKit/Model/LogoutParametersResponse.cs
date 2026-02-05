
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 注销登录参数响应
    /// </summary>
    public class LogoutParametersResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("in_transfer")]
        public int InTransfer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("auth", NullValueHandling = NullValueHandling.Ignore)]
        public string Auth { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<string> Url { get; set; } = new List<string>();
    }
}
