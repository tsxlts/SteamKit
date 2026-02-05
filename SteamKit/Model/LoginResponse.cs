using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 登录响应数据
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("login_complete")]
        public bool LoginComplete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("captcha_needed")]
        public bool CaptchaNeeded { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("captcha_gid")]
        public string? CaptchaGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("emailsteamid")]
        public string? EmailSteamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("emailauth_needed")]
        public bool NeedEmailAuth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("requires_twofactor")]
        public bool NeedTwoFactor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("transfer_parameters")]
        public TransferParameters? TransferParameter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("transfer_urls")]
        public List<string>? TransferUrls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("oauth")]
        public string? Auth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OAuth? AuthData
        {
            get
            {
                return Auth != null ? JsonConvert.DeserializeObject<OAuth>(Auth) : null;
            }
        }

        /// <summary>
        /// TransferParameters
        /// </summary>
        public class TransferParameters
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("steamid")]
            public string? SteamId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("token_secure")]
            public string? TokenSecure { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("auth")]
            public string? Auth { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("webcookie")]
            public string? Webcookie { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class OAuth
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("steamid")]
            public string? SteamId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("oauth_token")]
            public string? AuthToken { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("wgtoken")]
            public string? SteamLogin { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("wgtoken_secure")]
            public string? SteamLoginSecure { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("webcookie")]
            public string? Webcookie { get; set; }
        }
    }
}
