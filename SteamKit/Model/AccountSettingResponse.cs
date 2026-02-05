
namespace SteamKit.Model
{
    /// <summary>
    /// 账户设置详情
    /// </summary>
    public class AccountSettingResponse
    {
        /// <summary>
        /// 帐号名
        /// </summary>
        public string? AccountName { get; set; }

        /// <summary>
        /// 帐号Id
        /// </summary>
        public string? AccountId { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        public string? SteamId { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        public string? CountryCode { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public string? Balance { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? Phone { get; set; }
    }
}
