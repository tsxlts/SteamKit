using SteamKit.Client.Model;
using SteamKit.Model;

namespace SteamKit.Factory
{
    internal class DefaultAuthenticator : IAuthenticator
    {
        /// <summary>
        /// 令牌确认
        /// </summary>
        /// <param name="guardTypes">可用的验证方式</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>是否确认登录</returns>
        public Task<GuardConfirmationResult> GuardConfirmationAsync(IEnumerable<EAuthSessionGuardType> guardTypes, CancellationToken cancellationToken = default)
        {
            if (!guardTypes.Any())
            {
                return Task.FromResult(new GuardConfirmationResult
                {
                    Confirmed = true,
                    ConfirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_None,
                    GuardCode = null
                });
            }

            if (guardTypes.Contains(EAuthSessionGuardType.k_EAuthSessionGuardType_None))
            {
                return Task.FromResult(new GuardConfirmationResult
                {
                    Confirmed = true,
                    ConfirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_None,
                    GuardCode = null
                });
            }

            throw new NotImplementedException("您需要提供可用的令牌验证器");
        }
    }
}
