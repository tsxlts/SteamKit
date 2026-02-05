using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询账户手机号状态响应
    /// </summary>
    public class QueryAccountPhoneStatusResponse
    {
        /// <summary>
        /// 是否已验证手机号
        /// </summary>
        [JsonProperty("verified_phone")]
        public bool VerifiedPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("can_add_two_factor_phone")]
        public bool CanAddTwoFactorPhone { get; set; }
    }
}
