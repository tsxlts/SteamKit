using System.Security.Cryptography;
using System.Text;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Model;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;

namespace SteamKit
{
    /// <summary>
    /// Steam登录授权
    /// </summary>
    public static class SteamAuthentication
    {
        private readonly static string machineName = $"{Environment.MachineName}@SteamApi";

        /// <summary>
        /// 获取登录RSA公钥
        /// </summary>
        /// <param name="username">登录用户名</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_GetPasswordRSAPublicKey_Response>> QueryRsaPublicKeyAsync(string username, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_GetPasswordRSAPublicKey_Request
            {
                account_name = username
            };

            return await SteamWebApi.SendAsync<CAuthentication_GetPasswordRSAPublicKey_Request, CAuthentication_GetPasswordRSAPublicKey_Response>(proxy, null, null,
                "IAuthenticationService", "GetPasswordRSAPublicKey",
                HttpMethod.Get,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 开始登录授权
        /// </summary>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="guardData">令牌信息</param>
        /// <param name="platformType">登录平台</param>
        /// <param name="rsaResponse">登录RSA公钥</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_BeginAuthSessionViaCredentials_Response>> BeginAuthSessionViaCredentialsAsync(string username, string password, string? guardData, EAuthTokenPlatformType platformType, CAuthentication_GetPasswordRSAPublicKey_Response rsaResponse, CancellationToken cancellationToken = default)
        {
            byte[] encryptedPasswordBytes;
            using (var rsaEncryptor = new RSACryptoServiceProvider())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var rsaParameters = rsaEncryptor.ExportParameters(false);
                rsaParameters.Exponent = HexStringToByteArray(rsaResponse.publickey_exp);
                rsaParameters.Modulus = HexStringToByteArray(rsaResponse.publickey_mod);
                rsaEncryptor.ImportParameters(rsaParameters);
                encryptedPasswordBytes = rsaEncryptor.Encrypt(passwordBytes, false);
            }
            string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

            Proxy proxy = GetProxy();

            var body = new CAuthentication_BeginAuthSessionViaCredentials_Request
            {
                account_name = username,
                encrypted_password = encryptedPassword,
                encryption_timestamp = rsaResponse.timestamp,
                platform_type = platformType,
                persistence = ESessionPersistence.k_ESessionPersistence_Persistent,
                remember_login = true,
                device_friendly_name = $"{machineName}",
                guard_data = guardData ?? "",
                device_details = new CAuthentication_DeviceDetails
                {
                    device_friendly_name = $"{machineName}",
                    platform_type = platformType,
                    os_type = 0
                }
            };

            return await SteamWebApi.SendAsync<CAuthentication_BeginAuthSessionViaCredentials_Request, CAuthentication_BeginAuthSessionViaCredentials_Response>(proxy, null, null,
                "IAuthenticationService", "BeginAuthSessionViaCredentials",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 开始授权二维码登录
        /// </summary>
        /// <param name="platformType">登录平台</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_BeginAuthSessionViaQR_Response>> BeginAuthSessionViaQRAsync(EAuthTokenPlatformType platformType, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_BeginAuthSessionViaQR_Request
            {
                platform_type = platformType,
                device_friendly_name = $"{machineName}",
                device_details = new CAuthentication_DeviceDetails
                {
                    device_friendly_name = $"{machineName}",
                    platform_type = platformType,
                    os_type = 0
                }
            };

            return await SteamWebApi.SendAsync<CAuthentication_BeginAuthSessionViaQR_Request, CAuthentication_BeginAuthSessionViaQR_Response>(proxy, null, null,
                "IAuthenticationService", "BeginAuthSessionViaQR",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查询登录授权状态
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="requestId">RequestId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_PollAuthSessionStatus_Response>> PollAuthSessionStatusAsync(ulong clientId, byte[] requestId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_PollAuthSessionStatus_Request
            {
                client_id = clientId,
                request_id = requestId
            };

            return await SteamWebApi.SendAsync<CAuthentication_PollAuthSessionStatus_Request, CAuthentication_PollAuthSessionStatus_Response>(proxy, null, null,
                "IAuthenticationService", "PollAuthSessionStatus",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 令牌码确认登录
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="guardType">令牌认证方式</param>
        /// <param name="guardCode">令牌码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_UpdateAuthSessionWithSteamGuardCode_Response>> UpdateAuthSessionWithSteamGuardCodeAsync(ulong steamId, ulong clientId, EAuthSessionGuardType guardType, string guardCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_UpdateAuthSessionWithSteamGuardCode_Request
            {
                steamid = steamId,
                client_id = clientId,
                code_type = guardType,
                code = guardCode
            };

            return await SteamWebApi.SendAsync<CAuthentication_UpdateAuthSessionWithSteamGuardCode_Request, CAuthentication_UpdateAuthSessionWithSteamGuardCode_Response>(proxy, null, null,
                "IAuthenticationService", "UpdateAuthSessionWithSteamGuardCode",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查询待授权登录信息
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_GetAuthSessionsForAccount_Response>> QueryAuthSessionsForAccountAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_GetAuthSessionsForAccount_Request
            {
            };

            return await SteamWebApi.SendAsync<CAuthentication_GetAuthSessionsForAccount_Request, CAuthentication_GetAuthSessionsForAccount_Response>(proxy, null, accessToken,
                "IAuthenticationService", "GetAuthSessionsForAccount",
                HttpMethod.Get,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查询待授权登录Session信息
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_GetAuthSessionInfo_Response>> QueryAuthSessionInfoAsync(string accessToken, ulong clientId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_GetAuthSessionInfo_Request
            {
                client_id = clientId,
            };

            return await SteamWebApi.SendAsync<CAuthentication_GetAuthSessionInfo_Request, CAuthentication_GetAuthSessionInfo_Response>(proxy, null, accessToken,
                "IAuthenticationService", "GetAuthSessionInfo",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 手机确认登录
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="version">版本</param>
        /// <param name="signature">令牌签名</param>
        /// <param name="confirm">是否确认登录</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_UpdateAuthSessionWithMobileConfirmation_Response>> UpdateAuthSessionWithMobileConfirmationAsync(string accessToken, ulong steamId, ulong clientId, int version, byte[] signature, bool confirm, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_UpdateAuthSessionWithMobileConfirmation_Request
            {
                version = version,
                steamid = steamId,
                client_id = clientId,
                signature = signature,
                confirm = confirm,
                persistence = ESessionPersistence.k_ESessionPersistence_Persistent
            };

            return await SteamWebApi.SendAsync<CAuthentication_UpdateAuthSessionWithMobileConfirmation_Request, CAuthentication_UpdateAuthSessionWithMobileConfirmation_Response>(proxy, null, accessToken,
                "IAuthenticationService", "UpdateAuthSessionWithMobileConfirmation",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取App登录Token
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="renewal">是否续期RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_AccessToken_GenerateForApp_Response>> GenerateAppAccessTokenAsync(ulong steamId, string refreshToken, bool renewal = true, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_AccessToken_GenerateForApp_Request
            {
                refresh_token = refreshToken,
                steamid = steamId,
                renewal_type = renewal ? ETokenRenewalType.k_ETokenRenewalType_Allow : ETokenRenewalType.k_ETokenRenewalType_None
            };

            return await SteamWebApi.SendAsync<CAuthentication_AccessToken_GenerateForApp_Request, CAuthentication_AccessToken_GenerateForApp_Response>(proxy, null, null,
                "IAuthenticationService", "GenerateAccessTokenForApp",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 撤销RefreshToken
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_Token_Revoke_Response>> RevokeTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_Token_Revoke_Request
            {
                token = refreshToken,
                revoke_action = EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent
            };

            return await SteamWebApi.SendAsync<CAuthentication_Token_Revoke_Request, CAuthentication_Token_Revoke_Response>(proxy, null, accessToken,
                "IAuthenticationService", "RevokeToken",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取已登录的Token
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="includeRevoked">是否包含已失效的Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_RefreshToken_Enumerate_Response>> QueryTokensAsync(string accessToken, bool includeRevoked = false, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CAuthentication_RefreshToken_Enumerate_Request
            {
                include_revoked = includeRevoked,
            };

            return await SteamWebApi.SendAsync<CAuthentication_RefreshToken_Enumerate_Request, CAuthentication_RefreshToken_Enumerate_Response>(proxy, null, accessToken,
                "IAuthenticationService", "EnumerateTokens",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 撤销指定的RefreshToken
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="tokenId">
        /// 需要撤销的TokenId
        /// <para>传入空时撤销当前登录的RefreshToken</para>
        /// </param>
        /// <param name="sharedSecret">
        /// Steam共享秘钥
        /// <para>tokenId不为空时此参数必传</para>
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CAuthentication_RefreshToken_Revoke_Response>> RevokeRefreshTokenAsync(string accessToken, ulong steamId, ulong? tokenId = null, string? sharedSecret = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            byte[]? signature = null;
            if (tokenId.HasValue)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(sharedSecret, nameof(sharedSecret));

                signature = GuardCodeGenerator.GenerateSignature(Encoding.UTF8.GetBytes($"{tokenId.Value}"), Convert.FromBase64String(sharedSecret));
            }

            var body = new CAuthentication_RefreshToken_Revoke_Request
            {
                steamid = steamId,
                token_id = tokenId ?? 0,
                signature = signature ?? [],
                revoke_action = EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent
            };

            return await SteamWebApi.SendAsync<CAuthentication_RefreshToken_Revoke_Request, CAuthentication_RefreshToken_Revoke_Response>(proxy, null, accessToken,
                "IAuthenticationService", "RevokeRefreshToken",
                HttpMethod.Post,
                body,
                version: 1,
                cancellationToken: cancellationToken);
        }
    }
}
