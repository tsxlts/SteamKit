using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 开始登录授权响应数据
    /// </summary>
    public class BeginAuthSessionViaCredentialsResponse
    {
        /// <summary>
        /// ClientId
        /// </summary>
        [JsonProperty("client_id")]
        public string? ClientId { get; set; }

        /// <summary>
        /// RequestId
        /// </summary>
        [JsonProperty("request_id")]
        public string? RequestId { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        [JsonProperty("interval")]
        public double Interval { get; set; }

        /// <summary>
        /// AllowedConfirmations
        /// </summary>
        [JsonProperty("allowed_confirmations")]
        public List<Confirmations>? AllowedConfirmations { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        [JsonProperty("steamid")]
        public string? SteamId { get; set; }

        /// <summary>
        /// WeakToken
        /// </summary>
        [JsonProperty("weak_token")]
        public string? WeakToken { get; set; }

        /// <summary>
        /// ExtendedErrorMessage
        /// </summary>
        [JsonProperty("extended_error_message")]
        public string ExtendedErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public class Confirmations
        {
            /// <summary>
            /// ConfirmationType
            /// </summary>
            [JsonProperty("confirmation_type")]
            public AuthConfirmationType ConfirmationType { get; set; }
        }
    }

    /// <summary>
    /// 登录平台
    /// </summary>
    public enum AuthTokenPlatformType
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 
        /// </summary>
        SteamClient = 1,

        /// <summary>
        /// 
        /// </summary>
        WebBrowser = 2,

        /// <summary>
        /// 
        /// </summary>
        MobileApp = 3,
    }

    /// <summary>
    /// AuthConfirmationType
    /// </summary>
    public enum AuthConfirmationType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// None
        /// </summary>
        None = 1,

        /// <summary>
        /// EmailCode
        /// </summary>
        EmailCode = 2,

        /// <summary>
        /// DeviceCode
        /// </summary>
        DeviceCode = 3,

        /// <summary>
        /// DeviceConfirmation
        /// </summary>
        DeviceConfirmation = 4,

        /// <summary>
        /// EmailConfirmation
        /// </summary>
        EmailConfirmation = 5,

        /// <summary>
        /// MachineToken
        /// </summary>
        MachineToken = 6,

        /// <summary>
        /// LegacyMachineAuth
        /// </summary>
        LegacyMachineAuth = 7,
    }
}
