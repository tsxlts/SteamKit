using SteamKit;
using SteamKit.Client.Model;
using SteamKit.Factory;
using SteamKit.Model;

namespace GameClient.Factory
{
    public class AuthGuardFactory : IAuthenticator
    {
        private readonly SteamGuard _steamGuard;
        public AuthGuardFactory(SteamGuard steamGuard)
        {
            _steamGuard = steamGuard;
        }

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

            if (guardTypes.Contains(EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode))
            {
                var confirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode;
                string? guardCode;
                if (!string.IsNullOrWhiteSpace(_steamGuard?.SharedSecret))
                {
                    guardCode = GuardCodeGenerator.GenerateAuthCode(Extensions.GetSteamTimestampAsync().Result, _steamGuard.SharedSecret);
                }
                else
                {
                    Console.WriteLine("请输入手机令牌");
                    guardCode = Console.ReadLine()!.ToUpper();

                    if (string.IsNullOrWhiteSpace(guardCode))
                    {
                        confirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceConfirmation;
                    }
                }

                return Task.FromResult(new GuardConfirmationResult
                {
                    Confirmed = true,
                    ConfirmationType = confirmationType,
                    GuardCode = guardCode
                });
            }

            if (guardTypes.Contains(EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode))
            {
                Console.WriteLine("请输入邮箱令牌");
                string guardCode = Console.ReadLine()!.ToUpper();

                return Task.FromResult(new GuardConfirmationResult
                {
                    Confirmed = true,
                    ConfirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode,
                    GuardCode = guardCode
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

            return Task.FromResult(new GuardConfirmationResult
            {
                Confirmed = false,
                ConfirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_None,
                GuardCode = null
            });
        }
    }
}
