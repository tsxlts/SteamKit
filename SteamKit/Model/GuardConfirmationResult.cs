using SteamKit.Client.Model;

namespace SteamKit.Model
{
    /// <summary>
    /// 令牌确认结果
    /// </summary>
    public class GuardConfirmationResult
    {
        /// <summary>
        /// 是否已确认
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        /// 确认方式
        /// </summary>
        public EAuthSessionGuardType ConfirmationType { get; set; }

        /// <summary>
        /// 令牌码
        /// </summary>
        public string? GuardCode { get; set; }
    }
}
