using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 用户所在国家响应
    /// </summary>
    public class UserCountryResponse
    {
        /// <summary>
        /// 国家代码
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;
    }
}
