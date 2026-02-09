using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;
using static SteamKit.SteamEnum;

namespace SteamKit.Api
{
    /// <summary>
    /// Steam验证器
    /// </summary>
    public static class SteamAuthenticatorV1
    {
        /// <summary>
        /// 令牌版本
        /// V2
        /// </summary>
        public const uint AuthenticatorV2 = 2;

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
        public static async Task<ISteamApiResponse<AddAuthenticatorResponse>> AddAuthenticatorAsync(string accessToken, string steamId, string deviceId, uint version, long timestamp, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/AddAuthenticator/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"steamid",steamId },
                {"device_identifier",deviceId },
                {"serial_number",$"{timestamp}" },
                {"authenticator_time",$"{timestamp}" },
                {"authenticator_type","1" },
                //{"sms_phone_id","1" },
                {"version",$"{version}" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<AddAuthenticatorResponse> addAuthenticatorResponse = SteamApiResponse<ApiResponse<AddAuthenticatorResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return addAuthenticatorResponse;
            }
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
        public static async Task<ISteamApiResponse<FinalizeAddAuthenticatorResponse>> FinalizeAddAuthenticatorAsync(string accessToken, string steamId, string verificationCode, string authenticatorCode, long timestamp, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/FinalizeAddAuthenticator/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"steamid",steamId },
                {"authenticator_code",authenticatorCode },
                {"activation_code",verificationCode },
                {"authenticator_time",$"{timestamp}" },
                {"validate_sms_code","1" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<FinalizeAddAuthenticatorResponse> addAuthenticatorResponse = SteamApiResponse<ApiResponse<FinalizeAddAuthenticatorResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return addAuthenticatorResponse;
            }
        }

        /// <summary>
        /// 查询令牌验证器状态
        /// </summary>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryAuthenticatorStatusResponse>> QueryAuthenticatorStatusAsync(string accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/QueryStatus/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"steamid",steamId }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryAuthenticatorStatusResponse> addAuthenticatorResponse = SteamApiResponse<ApiResponse<QueryAuthenticatorStatusResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return addAuthenticatorResponse;
            }
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
        public static async Task<ISteamApiResponse<RemoveAuthenticatorResponse>> RemoveAuthenticatorAsync(string accessToken, SteamGuardScheme guardScheme, string revocationCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/RemoveAuthenticator/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"revocation_code",revocationCode },
                {"steamguard_scheme",$"{(int)guardScheme}" },
                //{"remove_all_steamguard_cookies","1" },
                {"revocation_reason","1" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<RemoveAuthenticatorResponse> addAuthenticatorResponse = SteamApiResponse<ApiResponse<RemoveAuthenticatorResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return addAuthenticatorResponse;
            }
        }

        /// <summary>
        /// 开始移动令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<BeginMoveAuthenticatorResponse>> BeginMoveAuthenticatorAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/RemoveAuthenticatorViaChallengeStart/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<BeginMoveAuthenticatorResponse> moveAuthenticatorResponse = SteamApiResponse<ApiResponse<BeginMoveAuthenticatorResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return moveAuthenticatorResponse;
            }
        }

        /// <summary>
        /// 确认移动令牌验证器
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="smsCode">短信验证码</param>
        /// <param name="version">令牌版本</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<FinalizeMoveAuthenticatorResponse>> FinalizeMoveAuthenticatorAsync(string accessToken, string smsCode, uint version = 2, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/RemoveAuthenticatorViaChallengeContinue/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"sms_code",smsCode },
                {"version",$"{version}" },
                {"generate_new_token","1" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<FinalizeMoveAuthenticatorResponse> moveAuthenticatorResponse = SteamApiResponse<ApiResponse<FinalizeMoveAuthenticatorResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return moveAuthenticatorResponse;
            }
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
        public static async Task<ISteamApiResponse<CreateEmergencyCodesResponse>> CreateEmergencyCodesAsync(string accessToken, string? smsCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/CreateEmergencyCodes/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code",smsCode??"" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<CreateEmergencyCodesResponse> createEmergencyCodesResponse = SteamApiResponse<ApiResponse<CreateEmergencyCodesResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return createEmergencyCodesResponse;
            }
        }

        /// <summary>
        /// 销毁备用码
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<DestroyEmergencyCodesResponse>> DestroyEmergencyCodesAsync(string accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/DestroyEmergencyCodes/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "steamid",steamId }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<DestroyEmergencyCodesResponse> destroyEmergencyCodesResponse = SteamApiResponse<ApiResponse<DestroyEmergencyCodesResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return destroyEmergencyCodesResponse;
            }
        }

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="guardCode">令牌码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<ValidateTokenResponse>> ValidateTokenAsync(string accessToken, string guardCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/ValidateToken/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code",guardCode }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<ValidateTokenResponse> validateTokenResponse = SteamApiResponse<ApiResponse<ValidateTokenResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return validateTokenResponse;
            }
        }

        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <param name="senderTime">发送方时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryServerTimeResponse>> QueryServerTimeAsync(long senderTime, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ITwoFactorService/QueryTime/v1/?" +
                $"sender_time={senderTime}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, new FormUrlEncodedContent(new Dictionary<string, string>()), InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryServerTimeResponse> timeResponse = SteamApiResponse<ApiResponse<QueryServerTimeResponse>>.JsonResponse(response).Convert(obj =>
                {
                    return obj?.Response;
                });
                return timeResponse;
            }
        }
    }
}
