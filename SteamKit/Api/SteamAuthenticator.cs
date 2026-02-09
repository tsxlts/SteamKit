using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Model;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.SteamEnum;

namespace SteamKit.Api
{
    /// <summary>
    /// Steam验证器
    /// </summary>
    public static class SteamAuthenticator
    {
        /// <summary>
        /// 令牌版本
        /// V2
        /// </summary>
        public const uint AuthenticatorV2 = 2;

        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <param name="senderTime">发送方时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_Time_Response>> QueryServerTimeAsync(ulong senderTime, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/QueryTime/v1/?" +
                $"sender_time={senderTime}");

            var body = new CTwoFactor_Time_Request
            {
                sender_time = senderTime,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_Time_Request, CTwoFactor_Time_Response>(proxy, null, null,
               "ITwoFactorService", "QueryTime",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 添加令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="version">
        /// 令牌版本
        /// <para>当前版本：2</para>
        /// </param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_AddAuthenticator_Response>> AddAuthenticatorAsync(string accessToken, ulong steamId, string deviceId, uint version, ulong timestamp, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_AddAuthenticator_Request
            {
                steamid = steamId,
                device_identifier = deviceId,
                serial_number = timestamp,
                authenticator_time = timestamp,
                authenticator_type = 1,
                version = version,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_AddAuthenticator_Request, CTwoFactor_AddAuthenticator_Response>(proxy, null, accessToken,
               "ITwoFactorService", "AddAuthenticator",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 确认添加令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="verificationCode">验证码</param>
        /// <param name="authenticatorCode">令牌确认码</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_FinalizeAddAuthenticator_Response>> FinalizeAddAuthenticatorAsync(string accessToken, ulong steamId, string verificationCode, string authenticatorCode, ulong timestamp, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_FinalizeAddAuthenticator_Request
            {
                steamid = steamId,
                authenticator_code = authenticatorCode,
                activation_code = verificationCode,
                authenticator_time = timestamp,
                validate_sms_code = true,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_FinalizeAddAuthenticator_Request, CTwoFactor_FinalizeAddAuthenticator_Response>(proxy, null, accessToken,
               "ITwoFactorService", "FinalizeAddAuthenticator",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查询令牌验证器状态
        /// </summary>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_Status_Response>> QueryAuthenticatorStatusAsync(string accessToken, ulong steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_Status_Request
            {
                steamid = steamId,
                include = ETwoFactorStatusFieldFlag.k_ETwoFactorStatusFieldFlag_LastUsage,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_Status_Request, CTwoFactor_Status_Response>(proxy, null, accessToken,
               "ITwoFactorService", "QueryStatus",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 移除令牌验证器
        /// 设置令牌验证方案
        /// </summary>
        /// <param name="guardScheme">设置后的令牌验证方案</param>
        /// <param name="revocationCode">撤销码</param>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_RemoveAuthenticator_Response>> RemoveAuthenticatorAsync(string accessToken, SteamGuardScheme guardScheme, string revocationCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_RemoveAuthenticator_Request
            {
                revocation_code = revocationCode,
                steamguard_scheme = (uint)guardScheme,
                remove_all_steamguard_cookies = false,
                revocation_reason = 1,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_RemoveAuthenticator_Request, CTwoFactor_RemoveAuthenticator_Response>(proxy, null, accessToken,
               "ITwoFactorService", "RemoveAuthenticator",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 开始移动令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_RemoveAuthenticatorViaChallengeStart_Response>> BeginMoveAuthenticatorAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_RemoveAuthenticatorViaChallengeStart_Request
            {
            };

            return await SteamWebApi.SendAsync<CTwoFactor_RemoveAuthenticatorViaChallengeStart_Request, CTwoFactor_RemoveAuthenticatorViaChallengeStart_Response>(proxy, null, accessToken,
               "ITwoFactorService", "RemoveAuthenticatorViaChallengeStart",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 确认移动令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="smsCode">短信验证码</param>
        /// <param name="version">令牌版本</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Response>> FinalizeMoveAuthenticatorAsync(string accessToken, string smsCode, uint version = 2, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Request
            {
                sms_code = smsCode,
                version = version,
                generate_new_token = true,
            };

            return await SteamWebApi.SendAsync<CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Request, CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Response>(proxy, null, accessToken,
               "ITwoFactorService", "RemoveAuthenticatorViaChallengeContinue",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 创建备用码
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="smsCode">
        /// 验证码
        /// 开始传入空发送验证码
        /// 发送验证码后传入验证码确认创建备用码
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_CreateEmergencyCodes_Response>> CreateEmergencyCodesAsync(string accessToken, string? smsCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_CreateEmergencyCodes_Request
            {
                code = smsCode ?? ""
            };

            return await SteamWebApi.SendAsync<CTwoFactor_CreateEmergencyCodes_Request, CTwoFactor_CreateEmergencyCodes_Response>(proxy, null, accessToken,
               "ITwoFactorService", "CreateEmergencyCodes",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 销毁备用码
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_DestroyEmergencyCodes_Response>> DestroyEmergencyCodesAsync(string accessToken, ulong steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_DestroyEmergencyCodes_Request
            {
                steamid = steamId
            };

            return await SteamWebApi.SendAsync<CTwoFactor_DestroyEmergencyCodes_Request, CTwoFactor_DestroyEmergencyCodes_Response>(proxy, null, accessToken,
               "ITwoFactorService", "DestroyEmergencyCodes",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="guardCode">令牌码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CTwoFactor_ValidateToken_Response>> ValidateTokenAsync(string accessToken, string guardCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CTwoFactor_ValidateToken_Request
            {
                code = guardCode
            };

            return await SteamWebApi.SendAsync<CTwoFactor_ValidateToken_Request, CTwoFactor_ValidateToken_Response>(proxy, null, accessToken,
               "ITwoFactorService", "ValidateToken",
               HttpMethod.Post,
               body,
               version: 1,
               cancellationToken: cancellationToken);
        }
    }
}
