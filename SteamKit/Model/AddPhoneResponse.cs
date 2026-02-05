using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 添加手机号响应
    /// </summary>
    public class AddPhoneResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 是否显示重新发送
        /// </summary>
        [JsonProperty("showResend")]
        public bool ShowResend { get; set; }

        /// <summary>
        /// 状态
        /// email_verification
        /// get_sms_code
        /// done
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errorText")]
        public string ErrorText { get; set; } = string.Empty;

        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

    /// <summary>
    /// 添加手机号进度
    /// </summary>
    public enum AddPhoneOperate
    {
        /// <summary>
        /// 开始
        /// 开始之前需要验证手机号
        /// </summary>
        Begin = 1,

        /// <summary>
        /// 发送验证码
        /// 发送验证码之前需要邮箱链接确认
        /// </summary>
        SendSmsCode = 2,

        /// <summary>
        /// 短信验证
        /// 短信验证之前需要发送验证码
        /// </summary>
        VerificationSmsCode = 3
    }
}
