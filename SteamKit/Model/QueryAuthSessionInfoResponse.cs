using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询待授权登录Session信息响应
    /// </summary>
    public class QueryAuthSessionInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ip")]
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("geoloc")]
        public string Geoloc { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("device_friendly_name")]
        public string DeviceFriendlyName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("platform_type")]
        public AuthTokenPlatformType PlatformType { get; set; } = AuthTokenPlatformType.Unknown;
    }
}
