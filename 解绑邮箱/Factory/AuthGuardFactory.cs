using SteamKit;
using SteamKit.Client.Model;
using SteamKit.Factory;
using SteamKit.Model;

namespace 解绑邮箱.Factory
{
    public class AuthGuardFactory : IAuthenticator
    {
        private readonly CancellationTokenSource cts;
        private readonly SteamGuard _steamGuard;
        public AuthGuardFactory(SteamGuard steamGuard, CancellationTokenSource cts)
        {
            _steamGuard = steamGuard;
            this.cts = cts;
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
                string? guardCode;
                if (_steamGuard != null)
                {
                    guardCode = GuardCodeGenerator.GenerateAuthCode(Extensions.GetSteamTimestampAsync().Result, _steamGuard.SharedSecret);
                }
                else
                {
                    cts.CancelAfter(TimeSpan.FromMinutes(1));

                    Console.WriteLine("请输入手机令牌");
                    guardCode = Console.ReadLine()!.ToUpper();
                }

                return Task.FromResult(new GuardConfirmationResult
                {
                    Confirmed = true,
                    ConfirmationType = EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode,
                    GuardCode = guardCode
                });
            }

            if (guardTypes.Contains(EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode))
            {
                cts.CancelAfter(TimeSpan.FromMinutes(1));

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
