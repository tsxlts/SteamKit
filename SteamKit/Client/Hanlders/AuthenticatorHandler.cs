using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Client.Server;
using static SteamKit.Enums;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 令牌验证器处理器
    /// </summary>
    public class AuthenticatorHandler : MessageHandler
    {
        /// <summary>
        /// 
        /// </summary>
        internal AuthenticatorHandler() : base()
        {
        }

        /// <summary>
        /// 添加令牌验证器
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<ClientResult<AddAuthenticatorResult>> AddAuthenticatorAsync(string deviceId, ulong timestamp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_AddAuthenticator_Response>(c => c.AddAuthenticator(new CTwoFactor_AddAuthenticator_Request
            {
                steamid = Client.SteamId!.Value,
                version = 2,
                authenticator_type = 1,
                serial_number = timestamp,
                authenticator_time = timestamp,
                device_identifier = deviceId
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK || (result.Result?.status ?? -1) != 1)
            {
                return new ClientResult<AddAuthenticatorResult>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = new AddAuthenticatorResult
                    {
                        Success = false
                    }
                };
            }

            return new ClientResult<AddAuthenticatorResult>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = new AddAuthenticatorResult
                {
                    Success = true,
                    AccountName = result.Result!.account_name,
                    SharedSecret = Convert.ToBase64String(result.Result.shared_secret),
                    IdentitySecret = Convert.ToBase64String(result.Result.identity_secret),
                    Secret1 = Convert.ToBase64String(result.Result.secret_1),
                    SerialNumber = result.Result.serial_number,
                    RevocationCode = result.Result.revocation_code,
                    TokenGID = result.Result.token_gid,
                    URI = result.Result.uri,

                    DeviceId = deviceId,
                    GuardScheme = SteamGuardScheme.Device,
                }
            };
        }

        /// <summary>
        /// 添加令牌验证器
        /// 确认验证码
        /// </summary>
        /// <param name="smsCode">手机验证码</param>
        /// <param name="sharedSecret">共享秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<ClientResult<bool>> FinalizeAddAuthenticatorAsync(string smsCode, string sharedSecret, ulong timestamp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var authenticatorCode = GuardCodeGenerator.GenerateAuthCode(timestamp, sharedSecret);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_FinalizeAddAuthenticator_Response>(c => c.FinalizeAddAuthenticator(new CTwoFactor_FinalizeAddAuthenticator_Request
            {
                steamid = Client.SteamId!.Value,
                authenticator_time = timestamp,
                authenticator_code = authenticatorCode,
                validate_sms_code = true,
                activation_code = smsCode
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK || !(result.Result?.success ?? false) || result.Result.status != 2)
            {
                return new ClientResult<bool>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = false
                };
            }

            return new ClientResult<bool>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = true
            };
        }

        /// <summary>
        /// 移除令牌验证器
        /// 设置令牌验证方案
        /// </summary>
        /// <param name="guardScheme">设置后的令牌验证方案</param>
        /// <param name="revocationCode">令牌撤销码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<ClientResult<bool>> RemoveAuthenticatorAsync(SteamGuardScheme guardScheme, string revocationCode, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_RemoveAuthenticator_Response>(c => c.RemoveAuthenticator(new CTwoFactor_RemoveAuthenticator_Request
            {
                revocation_code = revocationCode,
                steamguard_scheme = (uint)guardScheme,
                remove_all_steamguard_cookies = false,
                revocation_reason = 1
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.Result == null)
            {
                return new ClientResult<bool>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = false
                };
            }

            return new ClientResult<bool>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result.success
            };
        }

        /// <summary>
        /// 查询令牌验证器状态
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<ClientResult<AuthenticatorStatusResult>> QueryAuthenticatorStatusAsync(CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_Status_Response>(c => c.QueryStatus(new CTwoFactor_Status_Request
            {
                steamid = Client.SteamId!.Value,
                include = ETwoFactorStatusFieldFlag.k_ETwoFactorStatusFieldFlag_LastUsage
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.Result == null)
            {
                return new ClientResult<AuthenticatorStatusResult>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null
                };
            }

            return new ClientResult<AuthenticatorStatusResult>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = new AuthenticatorStatusResult
                {
                    TokenGID = result.Result.token_gid,
                    GuardScheme = (SteamGuardScheme)result.Result.steamguard_scheme,
                    DeviceId = result.Result.device_identifier,
                    Version = result.Result.version,
                    State = result.Result.state,
                    AuthenticatorAllowed = result.Result.authenticator_allowed,
                    EmailValidated = result.Result.email_validated,
                    AuthenticatorType = result.Result.authenticator_type,
                    InactivationReason = result.Result.inactivation_reason,
                    RevocationAttemptsRemaining = result.Result.revocation_attempts_remaining,
                    TimeCreated = result.Result.time_created,
                    TimeTransferred = result.Result.time_transferred,
                    ClassifiedAgent = result.Result.classified_agent,
                    LastSeenAuthTokenId = result.Result.last_seen_auth_token_id
                }
            };
        }

        /// <summary>
        /// 开始移动令牌验证器
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        private async Task<ClientResult<bool>> BeginMoveAuthenticatorAsync(CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_RemoveAuthenticatorViaChallengeStart_Response>(c => c.RemoveAuthenticatorViaChallengeStart(new CTwoFactor_RemoveAuthenticatorViaChallengeStart_Request
            {

            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.Result == null)
            {
                return new ClientResult<bool>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = false
                };
            }

            return new ClientResult<bool>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result.success
            };
        }

        /// <summary>
        /// 确认移动令牌验证器
        /// </summary>
        /// <param name="smsCode">验证码</param>
        /// <param name="version">令牌版本</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        private async Task<ClientResult<MoveAuthenticatorResult>> FinalizeMoveAuthenticatorAsync(string smsCode, uint version = 2, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var task = Client.ServiceMethodCallAsync<ITwoFactor, CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Response>(c => c.RemoveAuthenticatorViaChallengeContinue(new CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Request
            {
                sms_code = smsCode,
                version = version,
                generate_new_token = true
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.Result == null || !result.Result.success)
            {
                return new ClientResult<MoveAuthenticatorResult>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = new MoveAuthenticatorResult
                    {
                        Success = false
                    }
                };
            }

            return new ClientResult<MoveAuthenticatorResult>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = new MoveAuthenticatorResult
                {
                    Success = result.Result!.success,
                    AccountName = result.Result!.replacement_token.account_name,
                    SharedSecret = Convert.ToBase64String(result.Result.replacement_token.shared_secret),
                    IdentitySecret = Convert.ToBase64String(result.Result.replacement_token.identity_secret),
                    Secret1 = Convert.ToBase64String(result.Result.replacement_token.secret_1),
                    SerialNumber = result.Result.replacement_token.serial_number,
                    RevocationCode = result.Result.replacement_token.revocation_code,
                    TokenGID = result.Result.replacement_token.token_gid,
                    URI = result.Result.replacement_token.uri,

                    DeviceId = Extensions.GetDeviceId($"{result.Result.replacement_token.steamid}"),
                    GuardScheme = (SteamGuardScheme)result.Result.replacement_token.steamguard_scheme,

                    SteamId = result.Result.replacement_token.steamid

                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetMsg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal override Task HandleMsgAsync(IServerMsg packetMsg)
        {
            return Task.CompletedTask;
        }
    }
}
