using Newtonsoft.Json;
using static SteamKit.SteamEnum;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询登录Token信息响应
    /// </summary>
    public class QueryTokensResponse
    {
        /// <summary>
        /// 已登录的Token
        /// </summary>
        [JsonProperty("refresh_tokens", NullValueHandling = NullValueHandling.Ignore)]
        public List<RefreshTokenInfo> RefreshTokens { get; set; } = new List<RefreshTokenInfo>();

        /// <summary>
        /// 当前请求的Token
        /// </summary>
        [JsonProperty("requesting_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestingToken { get; set; } = string.Empty;
    }

    /// <summary>
    /// RefreshToken信息
    /// </summary>
    public class RefreshTokenInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_description")]
        public string TokenDescription { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time_updated")]
        public long TimeUpdated { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        [JsonProperty("platform_type")]
        public AuthTokenPlatformType PlatformType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("logged_in")]
        public bool LoggedIn { get; set; }

        /// <summary>
        /// 授权方式
        /// </summary>
        [JsonProperty("auth_type")]
        public AuthConfirmationType AuthType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("first_seen")]
        public TokenUsageEvent FirstSeen { get; set; } = new TokenUsageEvent();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("last_seen")]
        public TokenUsageEvent LastSeen { get; set; } = new TokenUsageEvent();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("os_type")]
        public OSType OSType { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        [JsonProperty("os_platform")]
        public int OSPlatform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("authentication_type")]
        public int AuthenticationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("gaming_device_type")]
        public int GamingDeviceType { get; set; }
    }

    /// <summary>
    /// Token授权信息
    /// </summary>
    public class TokenUsageEvent
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ip")]
        public IpAddress? Ip { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [JsonProperty("city")]
        public string? City { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class IpAddress
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("v4")]
        public long? V4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("v6")]
        public long? V6 { get; set; }
    }
}
