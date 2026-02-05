using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 设置手机号响应
    /// </summary>
    public class SetAccountPhoneNumberResponse
    {
        /// <summary>
        /// 邮箱确认地址
        /// </summary>
        [JsonProperty("confirmation_email_address")]
        public string ConfirmationEmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("phone_number_formatted")]
        public string PhoneNumberFormatted { get; set; } = string.Empty;
    }
}
