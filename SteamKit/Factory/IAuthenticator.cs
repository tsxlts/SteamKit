using SteamKit.Client.Model;
using SteamKit.Model;

namespace SteamKit.Factory
{
    /// <summary>
    /// 令牌验证器
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// 令牌确认
        /// </summary>
        /// <param name="guardTypes">可用的验证方式</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>是否确认登录</returns>
        public Task<GuardConfirmationResult> GuardConfirmationAsync(IEnumerable<EAuthSessionGuardType> guardTypes, CancellationToken cancellationToken = default);
    }
}
