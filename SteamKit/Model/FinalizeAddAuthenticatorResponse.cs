using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 确认添加令牌验证器响应
    /// </summary>
    public class FinalizeAddAuthenticatorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("server_time")]
        public long ServerTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public FinalizeAuthenticatorStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public enum FinalizeAuthenticatorStatus
        {
            /// <summary>
            /// 添加完成
            /// </summary>
            Finalize = 2,

            /// <summary>
            /// 令牌码错误
            /// </summary>
            UnableToGenerateCorrectCodes = 88,

            /// <summary>
            /// 手机验证码错误
            /// </summary>
            BadSMSCode = 89
        }
    }
}
