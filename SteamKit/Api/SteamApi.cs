using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamKit.Builder;
using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Enums;
using static SteamKit.Internal.Utils;
using static SteamKit.Model.QueryInventoryHistoryResponse;

namespace SteamKit.Api
{
    /// <summary>
    /// SteamApi
    /// </summary>
    public partial class SteamApi
    {
        /// <summary>
        /// 获取RsaKey
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [Obsolete]
        public static async Task<IWebResponse<RsaResponse>> GetRsaKeyAsync(string username, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/login/getrsakey");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "username",username }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<RsaResponse> rsaResponse = new WebResponse<RsaResponse>(response);
                return rsaResponse;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login">登录信息</param>
        /// <param name="rsaResponse">Rsa数据</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [Obsolete]
        public static async Task<IWebResponse<LoginResponse>> LoginAsync(LoginRequest login, RsaResponse rsaResponse, CancellationToken cancellationToken = default)
        {
            return await LoginAsync(login, rsaResponse, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 用户手机端登录
        /// </summary>
        /// <param name="login">登录信息</param>
        /// <param name="rsaResponse">Rsa数据</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [Obsolete]
        public static async Task<IWebResponse<LoginResponse>> MobileLoginAsync(LoginRequest login, RsaResponse rsaResponse, CancellationToken cancellationToken = default)
        {
            CookieCollection userCookies = SetMobileCookies();
            return await LoginAsync(login, rsaResponse, userCookies, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login">登录信息</param>
        /// <param name="rsaResponse">Rsa数据</param>
        /// <param name="userCookies">当前用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [Obsolete]
        public static async Task<IWebResponse<LoginResponse>> LoginAsync(LoginRequest login, RsaResponse rsaResponse, CookieCollection? userCookies, CancellationToken cancellationToken = default)
        {
            byte[] encryptedPasswordBytes;
            using (var rsaEncryptor = new RSACryptoServiceProvider())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(login.Password);
                var rsaParameters = rsaEncryptor.ExportParameters(false);
                rsaParameters.Exponent = HexStringToByteArray(rsaResponse.Exponent);
                rsaParameters.Modulus = HexStringToByteArray(rsaResponse.Modulus);
                rsaEncryptor.ImportParameters(rsaParameters);
                encryptedPasswordBytes = rsaEncryptor.Encrypt(passwordBytes, false);
            }
            string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/login/dologin/");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                { "username",login.UserName },
                { "password",encryptedPassword },
                { "twofactorcode",login.TwoFactorCode },
                { "captchagid",login.CaptchagId },
                { "captcha_text",login.CaptchaText },
                { "emailsteamid",login.SteamId },
                { "emailauth",login.EmailAuth },
                { "rsatimestamp",$"{rsaResponse.Timestamp}" },
                { "remember_login","false" },
                { "oauth_client_id", "DE45CD61" },
                { "oauth_scope", "read_profile write_profile read_client write_client" }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<LoginResponse> loginResponse = new WebResponse<LoginResponse>(response);
                return loginResponse;
            }
        }

        /// <summary>
        /// Steam OpenId登录验证
        /// </summary>
        /// <param name="verifyParameters">用户授权登录响应参数</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<OpenIdAuthResponse>> OpenIdAuthAsync(string verifyParameters, CancellationToken cancellationToken = default)
        {
            var parameters = Regex.Replace(verifyParameters, @"(?<=openid.mode=).+?(?=&)", "check_authentication", RegexOptions.IgnoreCase);

            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/openid/login?{parameters}");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<OpenIdAuthResponse> loginResponse = new WebResponse<string>(response).Convert(text =>
                {
                    var match = Regex.Match(text ?? "", @"is_valid:true", RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        return new OpenIdAuthResponse
                        {
                            IsValid = false,
                            SteamId = null
                        };
                    }

                    match = Regex.Match(Uri.UnescapeDataString(parameters), @"openid\.(claimed_id|identity)=https?://steamcommunity\.com/openid/id/(?<steamId>\d+)/?", RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        match = Regex.Match(Uri.UnescapeDataString(parameters), @"openid\.(claimed_id|identity)=https?://.+?/openid/id/(?<steamId>\d+)/?", RegexOptions.IgnoreCase);
                    }

                    return new OpenIdAuthResponse
                    {
                        IsValid = true,
                        SteamId = match.Success ? match.Groups["steamId"].Value : null
                    };
                });
                return loginResponse;
            }
        }

        /// <summary>
        /// 完成登录
        /// </summary>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<FinalizeLoginResponse>> FinalizeLoginAsync(string refreshToken, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamLogin}/jwt/finalizelogin");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                { "nonce",refreshToken },
                { "redir",DefaultSteamCommunity }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<FinalizeLoginResponse> finalizeTokenResponse = new WebResponse<FinalizeLoginResponse>(response);
                return finalizeTokenResponse;
            }
        }

        /// <summary>
        /// 保存登录Token
        /// </summary>
        /// <param name="uri">保存Token地址</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="auth">授权Token</param>
        /// <param name="nonce">随机码</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="headers">请求头</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<SaveTokenResponse>> SaveTokenAsync(Uri uri, string steamId, string auth, string nonce, CookieCollection userCookies, IDictionary<string, string>? headers = null, IWebProxy? proxy = null, CancellationToken cancellationToken = default)
        {
            var @params = new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                { "steamID",steamId },
                { "auth",auth },
                { "nonce",nonce },
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(Proxy.Instance, headers), cookies: SetDefaultCookies(Proxy.Instance, userCookies), proxy: proxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<SaveTokenResponse> saveTokenResponse = new WebResponse<SaveTokenResponse>(response);
                //saveTokenResponse.Cookies.SetDoamin(uri.Host, $"{uri.AbsolutePath.TrimEnd('/')}/".Replace("login/settoken/", ""));
                return saveTokenResponse;
            }
        }

        /// <summary>
        /// 刷新登录Token
        /// </summary>
        /// <param name="website">站点</param>
        /// <param name="loginCookies">
        /// 用户登录Cookie
        /// 需要包含有效的RefreshToken
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<RefreshTokenResponse>> AjaxRefreshTokenAsync(Website website, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamLogin}/jwt/ajaxrefresh");

            string redir = website switch
            {
                var site when site.HasFlag(Website.Community) => DefaultSteamCommunity,
                var site when site.HasFlag(Website.Store) => DefaultSteamStore,
                var site when site.HasFlag(Website.Help) => DefaultSteamHelp,
                var site when site.HasFlag(Website.Checkout) => DefaultSteamCheckout,
                _ => DefaultSteamCommunity
            };
            string origin = website switch
            {
                var site when site.HasFlag(Website.Community) => DefaultSteamCommunity,
                var site when site.HasFlag(Website.Store) => DefaultSteamStore,
                var site when site.HasFlag(Website.Help) => DefaultSteamHelp,
                var site when site.HasFlag(Website.Checkout) => DefaultSteamCheckout,
                _ => DefaultSteamCommunity
            };

            var @params = new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                { "redir",redir }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Origin",origin }
            };

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy, headers), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<RefreshTokenResponse> refreshTokenResponse = new WebResponse<RefreshTokenResponse>(response);
                return refreshTokenResponse;
            }
        }

        /// <summary>
        /// 刷新登录Token
        /// </summary>
        /// <param name="website">站点</param>
        /// <param name="loginCookies">
        /// 用户登录Cookie
        /// 需要包含有效的RefreshToken
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> RefreshTokenAsync(Website website, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            string redir = website switch
            {
                var site when site.HasFlag(Website.Community) => DefaultSteamCommunity,
                var site when site.HasFlag(Website.Store) => DefaultSteamStore,
                var site when site.HasFlag(Website.Help) => DefaultSteamHelp,
                var site when site.HasFlag(Website.Checkout) => DefaultSteamCheckout,
                _ => DefaultSteamCommunity
            };

            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamLogin}/jwt/refresh?" +
                $"redir={Uri.EscapeDataString(redir)}");

            IDictionary<string, string> headers = new Dictionary<string, string>();
            CookieCollection cookies = new CookieCollection
            {
                loginCookies
            };

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, autoRedirect: true, headers: SetDefaultHeaders(proxy, headers), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var webResponse = new WebResponse<string>(response);
                var refreshTokenResponse = webResponse.Convert(c => webResponse.Cookies.Any(c => Extensions.SteamAccessTokenCookeName.Equals(c.Name, StringComparison.OrdinalIgnoreCase) && !"deleted".Equals(c.Value, StringComparison.OrdinalIgnoreCase)));

                return refreshTokenResponse;
            }
        }

        /// <summary>
        /// 获取网页客户端登录Token
        /// </summary>
        /// <param name="loginCookies">Steam社区用户登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<GetClientWebTokenResponse>> GetClientWebTokenAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/chat/clientjstoken");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<GetClientWebTokenResponse> checkTokenResponse = new WebResponse<GetClientWebTokenResponse>(response);
                return checkTokenResponse;
            }
        }

        /// <summary>
        /// 获取手机端摘要信息
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="authenticatorGid">验证器GID</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryMobileSummaryResponse>> QueryMobileSummaryAsync(string accessToken, string? authenticatorGid, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IMobileAppService/GetMobileSummary/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"authenticator_gid",authenticatorGid??"" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryMobileSummaryResponse> mobileSummaryResponse = SteamApiResponse<ApiResponse<QueryMobileSummaryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return mobileSummaryResponse;
            }
        }

        /// <summary>
        /// 获取Ajax请求配置
        /// </summary>
        /// <param name="loginCookies">用户登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AjaxAsyncConfigResponse>> QueryAjaxConfigAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/pointssummary/ajaxgetasyncconfig");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AjaxAsyncConfigResponse> configResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    JToken? success = null;
                    if (!obj?.TryGetValue("success", StringComparison.CurrentCultureIgnoreCase, out success) ?? false)
                    {
                        return null;
                    }

                    AjaxAsyncConfigResponse result = new AjaxAsyncConfigResponse
                    {
                        Success = success!.Value<bool>(),
                        Data = null
                    };
                    if (!result.Success)
                    {
                        return result;
                    }

                    var data = obj!.GetValue("data");
                    if (data == null || data is JArray)
                    {
                        return result;
                    }

                    result.Data = data.ToObject<AjaxAsyncConfig>();
                    return result;
                });
                return configResponse;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="steamId">
        /// SteamId
        /// 与参数<paramref name="vanityURL"/>不能同时为空
        /// </param>
        /// <param name="vanityURL">
        /// 自定义个人资料链接
        /// 与参数<paramref name="steamId"/>不能同时为空
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IWebResponse<PlayerProfilesResponse>> QueryPlayerProfilesAsync(string? steamId, string? vanityURL, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var path = !string.IsNullOrWhiteSpace(steamId) ? $"profiles/{steamId}" : $"id/{vanityURL}";
            Uri uri = new Uri($"{proxy.SteamCommunity}/{path}/?xml=1");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<PlayerProfilesResponse> playerResponse = new WebResponse<string>(response).Convert(xml =>
                {
                    var match = Regex.Match(xml ?? "", @"<steamID64>(?<steamId>\d+?)</steamID64>");
                    if (!match.Success)
                    {
                        return null;
                    }

                    var steamId = match.Groups["steamId"].Value;

                    match = Regex.Match(xml ?? "", @"<steamID><!\[CDATA\[(?<steamName>.+?)\]\]></steamID>", RegexOptions.IgnorePatternWhitespace);
                    var steamName = match.Success ? match.Groups["steamName"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<realname><!\[CDATA\[(?<realname>.+?)\]\]></realname>", RegexOptions.IgnorePatternWhitespace);
                    var realname = match.Success ? match.Groups["realname"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<customURL><!\[CDATA\[(?<customURL>.+?)\]\]></customURL>", RegexOptions.IgnorePatternWhitespace);
                    var customURL = match.Success ? match.Groups["customURL"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<avatarIcon><!\[CDATA\[(?<avatar>.+?)\]\]></avatarIcon>");
                    var avatar = match.Success ? match.Groups["avatar"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<avatarMedium><!\[CDATA\[(?<avatarMedium>.+?)\]\]></avatarMedium>");
                    var avatarMedium = match.Success ? match.Groups["avatarMedium"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<avatarFull><!\[CDATA\[(?<avatarFull>.+?)\]\]></avatarFull>");
                    var avatarFull = match.Success ? match.Groups["avatarFull"].Value : string.Empty;

                    match = Regex.Match(xml ?? "", @"<vacBanned>(?<vacBanned>\d+?)</vacBanned>");
                    var vacBanned = match.Success ? int.Parse(match.Groups["vacBanned"].Value) > 0 : false;

                    match = Regex.Match(xml ?? "", @"<isLimitedAccount>(?<isLimitedAccount>\d+?)</isLimitedAccount>");
                    var isLimitedAccount = match.Success ? int.Parse(match.Groups["isLimitedAccount"].Value) > 0 : false;

                    return new PlayerProfilesResponse
                    {
                        SteamId = steamId,
                        SteamName = steamName,
                        Realname = realname,
                        CustomURL = customURL,
                        Avatar = avatar,
                        AvatarMedium = avatarMedium,
                        AvatarFull = avatarFull,
                        VacBanned = vacBanned,
                        IsLimitedAccount = isLimitedAccount,
                    };
                });
                return playerResponse;
            }
        }

        /// <summary>
        /// 编辑用户个人资料
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="profiles">个人资料</param>
        /// <param name="loginCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<EditAccountProfilesResponse>> EditAccountProfilesAsync(string currentSessionId, string steamId, EditAccountProfilesParameter profiles, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/edit/");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{ProxyBulider.DefaultSteamCommunity}/profiles/{steamId}/edit/info" }
            };

            headers = SetDefaultHeaders(proxy, headers);
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "personaName", profiles.PersonaName },
                { "real_name", profiles.RealName ?? "" },
                { "customURL", profiles.CustomURL ?? "" },
                { "country", profiles.CountryCode ?? "" },
                { "state", profiles.StateCode ?? "" },
                { "city", profiles.CityCode ?? "" },
                { "summary", profiles.Summary ?? "" },
                { "hide_profile_awards", profiles.HideProfileAwards ? "1" : "0" },

                { "weblink_1_title", "" },
                { "weblink_1_url", "" },
                { "weblink_2_title", "" },
                { "weblink_2_url", "" },
                { "weblink_3_title", "" },
                { "weblink_3_url", "" },

                { "sessionID", currentSessionId },
                { "type", "profileSave" },
                { "json", "1" },
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<EditAccountProfilesResponse> validatePhoneResponse = new WebResponse<EditAccountProfilesResponse>(response);
                return validatePhoneResponse;
            }
        }

        /// <summary>
        /// 获取用户隐私设置
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AccountPrivacySettingResponse>> QueryAccountPrivacySettingAsync(string currentSessionId, string steamId, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/ajaxsetprivacy/");
            var privacy = new
            {
                PrivacyProfile = -1,
                PrivacyInventory = -1,
                PrivacyInventoryGifts = -1,
                PrivacyOwnedGames = -1,
                PrivacyPlaytime = -1,
                PrivacyFriendsList = -1
            };
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "Privacy",JsonConvert.SerializeObject(privacy) },
                { "eCommentPermission","-1" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/profiles/{steamId}/edit/settings" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AccountPrivacySettingResponse> objResponse = new WebResponse<AccountPrivacySettingResponse>(response);
                return objResponse;
            }
        }

        /// <summary>
        /// 设置隐私设置
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="privacySetting">隐私设置</param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AccountPrivacySettingResponse>> SetAccountPrivacySettingAsync(string currentSessionId, string steamId, AccountPrivacy privacySetting, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/ajaxsetprivacy/");
            var privacy = new
            {
                PrivacyProfile = privacySetting.PrivacySettings.PrivacyProfile,
                PrivacyInventory = privacySetting.PrivacySettings.PrivacyInventory,
                PrivacyInventoryGifts = privacySetting.PrivacySettings.PrivacyInventoryGifts,
                PrivacyOwnedGames = privacySetting.PrivacySettings.PrivacyOwnedGames,
                PrivacyPlaytime = privacySetting.PrivacySettings.PrivacyPlaytime,
                PrivacyFriendsList = privacySetting.PrivacySettings.PrivacyFriendsList
            };
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "Privacy",JsonConvert.SerializeObject(privacy) },
                { "eCommentPermission",$"{(int)privacySetting.CommentPermission}" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/profiles/{steamId}/edit/settings" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AccountPrivacySettingResponse> objResponse = new WebResponse<AccountPrivacySettingResponse>(response);
                return objResponse;
            }
        }

        /// <summary>
        /// 设置手机号
        /// </summary>
        /// <param name="phoneNumber">
        /// 手机号
        /// 需要带上国际区号,+86 15022223333
        /// </param>
        /// <param name="phoneNumberCountry">
        /// 手机号所在国家编号
        /// 中国:CN
        /// </param>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<SetAccountPhoneNumberResponse>> SetAccountPhoneNumberAsync(string accessToken, string phoneNumber, string phoneNumberCountry, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/SetAccountPhoneNumber/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"phone_number",phoneNumber },
                {"phone_country_code",phoneNumberCountry }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<SetAccountPhoneNumberResponse> setAccountPhoneNumberResponse = SteamApiResponse<ApiResponse<SetAccountPhoneNumberResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return setAccountPhoneNumberResponse;
            }
        }

        /// <summary>
        /// 是否等待邮箱确认
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<IsAccountWaitingForEmailConfirmationResponse>> IsAccountWaitingForEmailConfirmationAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/IsAccountWaitingForEmailConfirmation/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<IsAccountWaitingForEmailConfirmationResponse> waitingEmailConfirmationResponse = SteamApiResponse<ApiResponse<IsAccountWaitingForEmailConfirmationResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return waitingEmailConfirmationResponse;
            }
        }

        /// <summary>
        /// 确认添加手机号
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="stoken">验证Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<ConfirmAddPhoneToAccountResponse>> ConfirmAddPhoneToAccountAsync(string accessToken, string steamId, string stoken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/ConfirmAddPhoneToAccount/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "steamid",Uri.EscapeDataString(steamId) },
                { "stoken",Uri.EscapeDataString(stoken) }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<ConfirmAddPhoneToAccountResponse> confirmAddPhoneToAccountResponse = SteamApiResponse<ApiResponse<ConfirmAddPhoneToAccountResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return confirmAddPhoneToAccountResponse;
            }
        }

        /// <summary>
        /// 查询账户手机号状态
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryAccountPhoneStatusResponse>> QueryAccountPhoneStatusAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/AccountPhoneStatus/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryAccountPhoneStatusResponse> phoneStatusResponse = SteamApiResponse<ApiResponse<QueryAccountPhoneStatusResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return phoneStatusResponse;
            }
        }

        /// <summary>
        /// 邮箱验证
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="stoken">验证Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<ValidateEmailAddressResponse>> ValidateEmailAddressAsync(string accessToken, string stoken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ICredentialsService/ValidateEmailAddress/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "stoken",Uri.EscapeDataString(stoken) }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<ValidateEmailAddressResponse> validateEmailAddressResponse = SteamApiResponse<ApiResponse<ValidateEmailAddressResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return validateEmailAddressResponse;
            }
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<SendPhoneVerificationCodeResponse>> SendPhoneVerificationCodeAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/SendPhoneVerificationCode/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<SendPhoneVerificationCodeResponse> sendPhoneVerificationCodeResponse = SteamApiResponse<ApiResponse<SendPhoneVerificationCodeResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return sendPhoneVerificationCodeResponse;
            }
        }

        /// <summary>
        /// 验证手机验证码
        /// 完成设置手机号
        /// </summary>
        /// <param name="smsCode">手机验证码</param>
        /// <param name="accessToken">登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<VerifyAccountPhoneWithCodeResponse>> VerifyAccountPhoneWithCodeAsync(string accessToken, string smsCode, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPhoneService/VerifyAccountPhoneWithCode/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code",smsCode }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<VerifyAccountPhoneWithCodeResponse> verifyAccountPhoneWithCodeResponse = SteamApiResponse<ApiResponse<VerifyAccountPhoneWithCodeResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return verifyAccountPhoneWithCodeResponse;
            }
        }

        /// <summary>
        /// 创建交易链接
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> CreateTradeLinkAsync(string currentSessionId, string steamId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/tradeoffers/newtradeurl");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    string? token = obj?.Value<string>();
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        return null;
                    }

                    string partner = Extensions.GetPartner(steamId);
                    return $"{DefaultSteamCommunity}/tradeoffer/new/?partner={partner}&token={token}";
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 检查用户Apikey是否有效
        /// 检查Apikey是否合法,验证Apikey是否属于用户
        /// </summary>
        /// <param name="steamId">被检测用户SteamId</param>
        /// <param name="apiKey">被检测用户Apikey</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<bool>> CheckApikeyAsync(string steamId, string apiKey, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPlayerService/GetPurchasedAndUpgradedProfileCustomizations/v1/?" +
                $"key={apiKey}" +
                $"&steamid={steamId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = SteamApiResponse<ApiResponse<JObject>>.JsonResponse(response);
                ISteamApiResponse<bool> stringResponse = objResponse.Convert(obj =>
                {
                    if (objResponse.HttpStatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    return objResponse.ResultCode == ErrorCodes.OK;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 检查用户交易链接Token是否有效
        /// 通过查询发起交易后交易暂挂时间方式进行验证
        /// </summary>
        /// <param name="apiKey">
        /// 检测方ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 检测方登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">被检测用户SteamId</param>
        /// <param name="token">被检测用户交易链接Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<bool>> CheckTradeOfferAccessTokenAsync(string? apiKey, string? accessToken, string steamId, string token, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeHoldDurations/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid_target={steamId}" +
                $"&trade_offer_access_token={token}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = SteamApiResponse<ApiResponse<JObject>>.JsonResponse(response);
                ISteamApiResponse<bool> stringResponse = objResponse.Convert(obj =>
                {
                    if (objResponse.HttpStatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    return objResponse.ResultCode == ErrorCodes.OK;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 查询发起交易后交易暂挂时间
        /// </summary>
        /// <param name="apiKey">
        /// 检测方ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 检测方登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">被检测用户SteamId</param>
        /// <param name="token">被检测用户交易链接Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryTradeHoldDurationsResponse>> QueryTradeHoldDurationsAsync(string? apiKey, string? accessToken, string steamId, string token, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeHoldDurations/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid_target={steamId}" +
                $"&trade_offer_access_token={token}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = SteamApiResponse<ApiResponse<QueryTradeHoldDurationsResponse>>.JsonResponse(response);
                ISteamApiResponse<QueryTradeHoldDurationsResponse> escrowResponse = objResponse.Convert(obj =>
                {
                    if (objResponse.HttpStatusCode != HttpStatusCode.OK || objResponse.ResultCode != ErrorCodes.OK)
                    {
                        return null;
                    }

                    return obj?.Response;
                });
                return escrowResponse;
            }
        }

        /// <summary>
        /// 查询好友
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryFriendsResponse>> QueryFriendsAsync(string? apiKey, string? accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamUser/GetFriendList/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid={steamId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryFriendsResponse> friendsResponse = new WebResponse<QueryFriendsResponse>(response);
                return friendsResponse;
            }
        }

        /// <summary>
        /// 查询自己的好友
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryOwnedFriendsResponse>> QueryFriendsAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IFriendsListService/GetFriendsList/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryOwnedFriendsResponse> friendsResponse = SteamApiResponse<ApiResponse<QueryOwnedFriendsResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return friendsResponse;
            }
        }

        /// <summary>
        /// 查询用户拥有的游戏
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">SteamId</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<OwnedGamesResponse>> QueryOwnedGamesAsync(string? apiKey, string? accessToken, string steamId, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPlayerService/GetOwnedGames/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid={steamId}" +
                $"&include_appinfo=true" +
                $"&include_played_free_games=true" +
                $"&include_free_sub=true" +
                $"&include_extended_appinfo=true" +
                $"&skip_unvetted_apps=false" +
                $"&format=json" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<OwnedGamesResponse> gamesResponse = SteamApiResponse<ApiResponse<OwnedGamesResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return gamesResponse;
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamIds">SteamId</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<PlayerSummariesResponse>> QueryPlayerSummariesAsync(string? apiKey, string? accessToken, IEnumerable<string> steamIds, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamUser/GetPlayerSummaries/v2/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamids={string.Join(',', steamIds)}" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<PlayerSummariesResponse> palyerResponse = SteamApiResponse<ApiResponse<PlayerSummariesResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="steamIds">SteamId</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<PlayerSummariesResponse>> QueryPlayerSummariesAsync(string accessToken, IEnumerable<string> steamIds, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamUserOAuth/GetUserSummaries/v2/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamids={string.Join(',', steamIds)}" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<PlayerSummariesResponse> palyerResponse = SteamApiResponse<PlayerSummariesResponse>.JsonResponse(response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询用户等级
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QuerySteamLevelResponse>> QuerySteamLevelAsync(string? apiKey, string? accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPlayerService/GetSteamLevel/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid={steamId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QuerySteamLevelResponse> palyerResponse = SteamApiResponse<ApiResponse<QuerySteamLevelResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询用户徽章
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamId">SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryBadgesResponse>> QueryBadgesAsync(string? apiKey, string? accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IPlayerService/GetBadges/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid={steamId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryBadgesResponse> palyerResponse = SteamApiResponse<ApiResponse<QueryBadgesResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询用户封禁信息
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="steamIds">SteamId</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<PlayerBansResponse>> QueryPlayerBansAsync(string? apiKey, string? accessToken, IEnumerable<string> steamIds, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamUser/GetPlayerBans/v1?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamids={string.Join(',', steamIds)}" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<PlayerBansResponse> palyerResponse = new WebResponse<PlayerBansResponse>(response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询用户所在国家
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<UserCountryResponse>> QueryUserCountryAsync(string accessToken, string steamId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IUserAccountService/GetUserCountry/v1/?" +
                $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"steamid", steamId }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<UserCountryResponse> palyerResponse = SteamApiResponse<ApiResponse<UserCountryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<GamesResponse>> QueryGamesAsync(CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamApps/GetAppList/v2");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<GamesResponse> palyerResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;
                    return obj?.GetValue("applist")?.ToObject<GamesResponse>();
                });
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询游戏资产筛选器
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="language">语言类型</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<GameAssetsFilterResponse>> QueryGameAssetsFilterAsync(string appId, Language language = Language.Schinese, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/appfilters/{appId}");
            CookieCollection cookies = new CookieCollection
            {
                userCookies ?? new CookieCollection() ,
                new Cookie(Extensions.SteamLanguageCookeName, language.GetApiCode())
            };
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<GameAssetsFilterResponse> palyerResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject? obj = token.Value<JObject>()!;

                    GameAssetsFilterResponse filterResponse = new GameAssetsFilterResponse
                    {
                        Success = obj.Value<bool>("success"),
                        Filters = new List<GameAssetsFilter>()
                    };

                    obj = obj.GetValue("facets") as JObject;
                    if (obj == null)
                    {
                        return filterResponse;
                    }

                    GameAssetsFilter filter;
                    foreach (var item in obj)
                    {
                        filter = new GameAssetsFilter
                        {
                            AppId = item.Value!.Value<string>("appid")!,
                            CategoryKey = item.Key,
                            Name = item.Value!.Value<string>("name")!,
                            LocalizedName = item.Value!.Value<string>("localized_name")!,
                            Tags = new List<GameAssetsFilter.Tag>()
                        };
                        foreach (var tag in item.Value!.Value<JObject>("tags") ?? new JObject())
                        {
                            filter.Tags.Add(new GameAssetsFilter.Tag
                            {
                                TagKey = tag.Key,
                                Name = tag.Key,
                                LocalizedName = tag.Value!.Value<string>("localized_name")!,
                                Matches = tag.Value!.Value<string>("matches") ?? "0",
                            });
                        }

                        filterResponse.Filters.Add(filter);
                    }

                    return filterResponse;
                });
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询游戏内物品价格
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="appId">游戏Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryAssetPricesResponse>> QueryAssetPricesAsync(string? apiKey, string? accessToken, string appId, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamEconomy/GetAssetPrices/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&appid={appId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryAssetPricesResponse> palyerResponse = new WebResponse<QueryAssetPricesResponse>(response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 查询资产分类信息
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="appId">游戏Id</param>
        /// <param name="classIds">分类Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AssetClassInfoResponse>> QueryAssetClassInfoAsync(string? apiKey, string? accessToken, string appId, IEnumerable<QueryAssetClassInfoParameter> classIds, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            StringBuilder query = new StringBuilder();
            int count = 0;
            foreach (var item in classIds)
            {
                query.Append($"classid{count}={item.ClassId}&instanceid{count}={item.InstanceId}&");
                count++;
            }

            Proxy proxy = GetProxy();
            //https://api.steampowered.com/ISteamEconomy/GetAssetClassInfo/v1/?key=*****&appid=730&language=zh-cn&class_count=1&classid0=4966008639&instanceid0=188530139
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamEconomy/GetAssetClassInfo/v0001?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&format=json" +
                $"&language={language.GetWebApiCode()}" +
                $"&appid={appId}" +
                $"&class_count={count}" +
                $"&{query}".TrimEnd('&'));

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AssetClassInfoResponse> assetsResponse = new WebResponse<JToken>(response)
                    .Convert(token =>
                    {
                        if (token == null || token.Type == JTokenType.Null || token is JArray)
                        {
                            return null;
                        }

                        JObject? obj = token.Value<JObject>()!;
                        obj = obj?.GetValue("result") as JObject;
                        if (obj == null)
                        {
                            return null;
                        }

                        var response = new AssetClassInfoResponse
                        {
                            Success = obj.Value<bool>("success"),
                            Error = obj.Value<string>("error"),
                            Assets = new List<AssetClassInfo>()
                        };
                        if (!response.Success)
                        {
                            return response;
                        }

                        foreach (var item in obj)
                        {
                            if (!(item.Value is JObject))
                            {
                                continue;
                            }

                            response.Assets.Add(item.Value!.ToObject<AssetClassInfo>()!);
                        }

                        return response;
                    });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询资产属性定义
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="language"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<AssetPropertySchemaResponse>> QueryAssetPropertySchemaAsync(string appId, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetAssetPropertySchema/v1/?" +
                $"appid={appId}" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<AssetPropertySchemaResponse> palyerResponse = SteamApiResponse<ApiResponse<AssetPropertySchemaResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return palyerResponse;
            }
        }

        /// <summary>
        /// 市场资格校验
        /// </summary>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<MarketEligibilityResponse>> MarketEligibilityCheckAsync(CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/eligibilitycheck/?" +
                $"goto=/market/");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = new WebResponse<string>(response);

                IWebResponse<MarketEligibilityResponse> checkResponse = objResponse.Convert(obj =>
                {
                    var checkCookie = objResponse.Cookies.FirstOrDefault(c => Extensions.WebTradeEligibilityCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (string.IsNullOrWhiteSpace(checkCookie?.Value))
                    {
                        return null;
                    }

                    return JsonConvert.DeserializeObject<MarketEligibilityResponse>(checkCookie.Value);
                });
                return checkResponse;
            }
        }

        /// <summary>
        /// 市场商品搜索
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="name">查询关键词</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<MarketSearchResponse>> MarketSearchAsync(string appId, string name, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/searchsuggestionsresults?" +
                $"q={Uri.EscapeDataString(name)}" +
                $"&appid={appId}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<MarketSearchResponse> assetsResponse = new WebResponse<MarketSearchResponse>(response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询市场商品
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="count">查询数量</param>
        /// <param name="start">查询起始索引</param>
        /// <param name="query">查询关键词</param>
        /// <param name="categories">分类信息</param>
        /// <param name="sortColumn">
        /// 排序列
        /// <para>name</para> 
        /// <para>popular</para>
        /// <para>quantity</para> 
        /// <para>price</para> 
        /// </param>
        /// <param name="sortType">
        /// 排序方式
        /// <para>asc</para> 
        /// <para>desc</para>
        /// </param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<MarketResponse>> QueryMarketAsync(string appId, int count, int start, string? query = null, IEnumerable<AssetFilter>? categories = null, string sortColumn = "default", string sortType = "asc", CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            StringBuilder queryBuilder = new StringBuilder();
            if (categories?.Any() ?? false)
            {
                foreach (var item in categories)
                {
                    if (item.TagValues?.Any() ?? false)
                    {
                        foreach (var value in item.TagValues)
                        {
                            queryBuilder.Append($"{item.Category}[]={value}&");
                        }
                    }
                }
            }

            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/search/render/?" +
                $"norender=1" +
                $"&search_descriptions=0" +
                $"&sort_column={sortColumn}" +
                $"&sort_dir={sortType}" +
                $"&query={Uri.EscapeDataString(query ?? "")}" +
                $"&appid={appId}" +
                $"&count={count}" +
                $"&start={start}" +
                $"&{queryBuilder}".TrimEnd('&'));

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<MarketResponse> assetsResponse = new WebResponse<MarketResponse>(response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询市场商品列表
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">商品名称</param>
        /// <param name="start">分页开始索引</param>
        /// <param name="count">分页大小</param>
        /// <param name="currency">货币类型</param>
        /// <param name="language">语言类型</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<MarketListingsResponse>> QueryMarketListingsAsync(string appId, string hashName, int start, int count, Currency currency = Currency.CNY, Language language = Language.Schinese, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            var method = MethodBase.GetCurrentMethod();
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/listings/{appId}/{Uri.EscapeDataString(hashName)}/render/?" +
                $"query=" +
                $"&start={start}" +
                $"&count={count}" +
                $"&currency={(int)currency}" +
                $"&country=CN" +
                $"&language={language.GetApiCode()}" +
                $"&t={Extensions.GetSystemMilliTimestamp()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<MarketListingsResponse> marketListingsResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;
                    MarketListingsResponse result = new MarketListingsResponse
                    {
                        Success = obj.Value<bool>("success"),
                        Start = obj.Value<int>("start"),
                        PageSize = obj.Value<int>("pagesize"),
                        TotalCount = obj.Value<int>("total_count"),
                        Listings = new List<MarketListings>(),
                        Assets = new List<MarketListingsDescription>()
                    };

                    if (result.Success && obj.ContainsKey("listinginfo"))
                    {
                        JToken listingToken = obj.GetValue("listinginfo")!;
                        var listinginfo = (listingToken as JObject) ?? new JObject();
                        foreach (var item in listinginfo)
                        {
                            result.Listings.Add(item.Value!.ToObject<MarketListings>()!);
                        }
                    }
                    if (result.Success && obj.ContainsKey("assets"))
                    {
                        var assetsToken = obj.GetValue("assets")!;
                        JObject asset = (assetsToken as JObject)?.Value<JObject>(appId) ?? new JObject();
                        foreach (var appItem in asset)
                        {
                            foreach (var assetItem in appItem.Value!.Value<JObject>()!)
                            {
                                result.Assets.Add(assetItem.Value!.ToObject<MarketListingsDescription>()!);
                            }
                        }
                    }

                    return result;
                });
                return marketListingsResponse;
            }
        }

        /// <summary>
        /// 查询市场价格
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">商品名称</param>
        /// <param name="currency">货币类型</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<MarketPriceResponse>> QueryMarketPriceAsync(string appId, string hashName, Currency currency = Currency.CNY, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/priceoverview/?" +
                $"appid={appId}" +
                $"&market_hash_name={Uri.EscapeDataString(hashName)}" +
                $"&currency={(int)currency}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<MarketPriceResponse> assetsResponse = new WebResponse<MarketPriceResponse>(response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询历史价格曲线图
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">商品名称</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<List<PriceGraphResponse>>> QueryPriceGraphAsync(string appId, string hashName, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/pricehistory/?" +
               $"appid={appId}" +
               $"&market_hash_name={Uri.EscapeDataString(hashName)}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<List<PriceGraphResponse>> graphResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return new List<PriceGraphResponse>();
                    }

                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    if (!obj!.Value<bool>("success"))
                    {
                        return new List<PriceGraphResponse>();
                    }

                    string currency = obj.Value<string>("price_prefix") ?? "$";
                    JArray array = obj.Value<JArray>("prices")!;

                    List<PriceGraphResponse> result = new List<PriceGraphResponse>();
                    PriceGraphResponse data;
                    JToken[] dataArray;
                    foreach (var item in array)
                    {
                        dataArray = item.ToObject<JToken[]>()!;

                        //May 04 2022 01: +0
                        DateTime.TryParseExact(dataArray[0].Value<string>(), "MMM d yyyy HH: z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time);
                        data = new PriceGraphResponse
                        {
                            Time = time.ToLocalTime(),
                            Price = dataArray[1].Value<decimal>(),
                            Count = dataArray[2].Value<int>(),
                            Currency = currency
                        };
                        result.Add(data);
                    }

                    return result;
                });
                return graphResponse;
            }
            ;
        }

        /// <summary>
        /// 查询商品售价数据
        /// </summary>
        /// <param name="Id">市场商品Id</param>
        /// <param name="currency">货币类型</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryOrdersHistogramResponse>> QueryOrdersHistogramAsync(string Id, Currency currency = Currency.CNY, CookieCollection? userCookies = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/itemordershistogram?" +
                $"language={language.GetApiCode()}" +
                $"&currency={(int)currency}" +
                $"&item_nameid={Id}" +
                $"&two_factor=0");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryOrdersHistogramResponse> graphResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    QueryOrdersHistogramResponse? result = obj?.ToObject<QueryOrdersHistogramResponse>();

                    if (result == null || result.Success != ErrorCodes.OK)
                    {
                        return result;
                    }

                    JArray array = obj!.Value<JArray>("sell_order_graph")!;
                    OrderGraph graph;
                    JToken[] dataArray;
                    foreach (var item in array)
                    {
                        dataArray = item.ToObject<JToken[]>()!;
                        graph = new OrderGraph
                        {
                            Price = dataArray[0].Value<decimal>(),
                            Count = dataArray[1].Value<int>(),
                            Summary = dataArray[2].Value<string>()!,
                        };
                        result.SellOrderGraph.Add(graph);
                    }

                    array = obj!.Value<JArray>("buy_order_graph")!;
                    foreach (var item in array)
                    {
                        dataArray = item.ToObject<JToken[]>()!;
                        graph = new OrderGraph
                        {
                            Price = dataArray[0].Value<decimal>(),
                            Count = dataArray[1].Value<int>(),
                            Summary = dataArray[2].Value<string>()!,
                        };
                        result.BuyOrderGraph.Add(graph);
                    }

                    return result;
                });
                return graphResponse;
            }
            ;
        }

        /// <summary>
        /// 查询商品动态
        /// </summary>
        /// <param name="Id">市场商品Id</param>
        /// <param name="currency">货币类型</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryOrdersActivityResponse>> QueryOrdersActivityAsync(string Id, Currency currency = Currency.CNY, CookieCollection? userCookies = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/itemordersactivity?" +
                $"country=CN" +
                $"&language={language.GetApiCode()}" +
                $"&currency={(int)currency}" +
                $"&item_nameid={Id}" +
                $"&two_factor=0" +
                $"&t={Extensions.GetSystemMilliTimestamp()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryOrdersActivityResponse> graphResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    QueryOrdersActivityResponse result = obj.ToObject<QueryOrdersActivityResponse>()!;
                    return result;
                });
                return graphResponse;
            }
            ;
        }

        /// <summary>
        /// 查询钱包余额
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryWalletDetailsResponse>> QueryWalletDetailsAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IUserAccountService/GetClientWalletDetails/v1?" +
                $"access_token={Uri.EscapeDataString(accessToken)}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "include_balance_in_usd","1" },
                { "include_formatted_balance","1" }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryWalletDetailsResponse> walletResponse = SteamApiResponse<ApiResponse<QueryWalletDetailsResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return walletResponse;
            }
        }

        /// <summary>
        /// 查询Steam通知
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="includeHidden">是否包含已隐藏的</param>
        /// <param name="includeRead">是否包含已读的</param>
        /// <param name="includeConfirmation">是否包含确认信息</param>
        /// <param name="includePinned">
        /// 是否包含固定的通知数据
        /// PendingGift
        /// PendingFriend
        /// PendingFamilyInvite
        /// </param>
        /// <param name="countOnly">是否只查询数量</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QuerySteamNotificationsResponse>> QuerySteamNotificationsAsync(string accessToken, bool includeHidden, bool includeConfirmation, bool includePinned, bool includeRead, bool countOnly, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamNotificationService/GetSteamNotifications/v1?" +
                $"access_token={Uri.EscapeDataString(accessToken)}" +
                $"&include_hidden={(includeHidden ? 1 : 0)}" +
                $"&include_confirmation_count={(includeConfirmation ? 1 : 0)}" +
                $"&include_pinned_counts={(includePinned ? 1 : 0)}" +
                $"&include_read={(includeRead ? 1 : 0)}" +
                $"&count_only={(countOnly ? 1 : 0)}" +
                $"&language={(int)language}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QuerySteamNotificationsResponse> notificationsResponse = SteamApiResponse<ApiResponse<QuerySteamNotificationsResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return notificationsResponse;
            }
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="notificationType">通知类型</param>
        /// <param name="markAllRead">是否标记所有消息已读</param>
        /// <param name="notificationIds">通知Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<NoResponse>> MarkNotificationsReadAsync(string accessToken, SteamNotificationType? notificationType, bool markAllRead, IEnumerable<long>? notificationIds, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamNotificationService/MarkNotificationsRead/v1?" +
                $"access_token={Uri.EscapeDataString(accessToken)}");

            var keyValues = new Dictionary<string, string>
            {
                { "timestamp",$"{Extensions.GetSystemTimestamp()}" },
                { "notification_type",$"{(int?)notificationType}" },
                { "mark_all_read",$"{(markAllRead ? 1 : 0)}" }
            };
            if (notificationIds?.Any() ?? false)
            {
                int index = 0;
                foreach (var value in notificationIds)
                {
                    keyValues.Add($"notification_ids[{index}]", $"{value}");
                    index++;
                }
            }

            var @params = new FormUrlEncodedContent(keyValues);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<NoResponse> notificationsResponse = new WebResponse<NoResponse>(response).Convert(c => new NoResponse());
                return notificationsResponse;
            }
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="notificationIds">通知Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<NoResponse>> HideNotificationAsync(string accessToken, IEnumerable<long> notificationIds, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamNotificationService/HideNotification/v1?" +
                $"access_token={Uri.EscapeDataString(accessToken)}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>(notificationIds.Select((value, index) => new KeyValuePair<string, string>($"notification_ids[{index}]", $"{value}")))
            {

            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<NoResponse> notificationsResponse = new WebResponse<NoResponse>(response).Convert(c => new NoResponse());
                return notificationsResponse;
            }
        }

        /// <summary>
        /// 出售资产
        /// 上架商品
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="assetId">资产Id</param>
        /// <param name="price">
        /// 出售价格
        /// 实际收款金额
        /// 单位：分
        /// </param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<SellAssetResponse>> SellAssetAsync(string currentSessionId, string appId, string contextId, ulong assetId, int price, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/sellitem/");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "appid",appId },
                { "contextid",contextId },
                { "assetid",$"{assetId}" },
                { "price",$"{price}" },
                { "amount","1" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<SellAssetResponse> sellResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    if (obj == null || obj.Type == JTokenType.Null || obj is JArray)
                    {
                        return null;
                    }

                    return obj.ToObject<SellAssetResponse>();
                });
                return sellResponse;
            }
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="listingId">商品Id</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<RemoveListingResponse>> RemoveListingAsync(string currentSessionId, string listingId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/removelisting/{listingId}");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = new WebResponse<JToken>(response);

                IWebResponse<RemoveListingResponse> removeListingResponse = objResponse.Convert(obj =>
                {
                    if (objResponse.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return new RemoveListingResponse
                        {
                            Success = true,
                            SessionId = currentSessionId
                        };
                    }

                    if (obj is JObject)
                    {
                        return obj.ToObject<RemoveListingResponse>();
                    }

                    return new RemoveListingResponse
                    {
                        Success = false,
                        SessionId = currentSessionId
                    };
                });

                return removeListingResponse;
            }
        }

        /// <summary>
        /// 查询自己的市场商品和订购单
        /// </summary>
        /// <param name="start">分页开始索引</param>
        /// <param name="count">分页大小</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QuetyListingsResponse>> QuetyListingsAsync(int start, int count, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/mylistings/render/?" +
                $"query=" +
                $"&norender=1" +
                $"&start={start}" +
                $"&count={count}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QuetyListingsResponse> assetsResponse = new WebResponse<QuetyListingsResponse>(response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询自己的市场订单历史记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="currentCookies"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IWebResponse<QuetyMarketHistoryResponse>> QueryListingsHistoryAsync(int start, int count, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/myhistory/render/?" +
                $"query=" +
                $"&norender=1" +
                $"&start={start}" +
                $"&count={count}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QuetyMarketHistoryResponse> assetsResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    JToken? success = null;
                    if (!obj?.TryGetValue("success", StringComparison.CurrentCultureIgnoreCase, out success) ?? false)
                    {
                        return null;
                    }

                    var response = new QuetyMarketHistoryResponse
                    {
                        Success = success!.Value<bool>(),
                        Start = obj!.Value<int>("start"),
                        PageSize = obj.Value<int>("pagesize"),
                        TotalCount = obj.Value<int>("total_count"),
                        Events = obj.Value<JToken>("events")?.ToObject<List<QuetyMarketHistoryResponse.Event>>() ?? new(),
                        Listings = new List<QuetyMarketHistoryResponse.Listing>(),
                        Purchases = new List<QuetyMarketHistoryResponse.Purchase>(),
                    };

                    var purchases = obj.Value<JToken>("purchases");
                    if (purchases is JObject purchasesObj)
                    {
                        QuetyMarketHistoryResponse.Purchase purchase;
                        foreach (var item in purchasesObj)
                        {
                            purchase = item.Value!.ToObject<QuetyMarketHistoryResponse.Purchase>()!;

                            response.Purchases.Add(purchase);
                        }
                    }

                    var listings = obj.Value<JToken>("listings");
                    if (listings is JObject listingObj)
                    {
                        QuetyMarketHistoryResponse.Listing listing;
                        foreach (var item in listingObj)
                        {
                            listing = item.Value!.ToObject<QuetyMarketHistoryResponse.Listing>()!;

                            response.Listings.Add(listing);
                        }
                    }

                    return response;
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="listingId">商品Id</param>
        /// <param name="price">
        /// 商品单价
        /// 不包含手续费
        /// 单位：分
        /// </param>
        /// <param name="fee">
        /// 商品手续费
        /// 单位：分
        /// </param>
        /// <param name="currency">货币类型</param>
        /// <param name="quantity">购买数量</param>
        /// <param name="confirmationId">令牌确认Id</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<BuyAssetResponse>> BuyAssetAsync(string currentSessionId, string listingId, int price, int fee, Currency currency, int quantity, string? confirmationId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/buylisting/{listingId}");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "currency",$"{(int)currency}" },
                { "subtotal",$"{price}" },
                { "fee",$"{fee}" },
                { "total",$"{price+fee}" },
                { "quantity",$"{quantity}" },
                { "billing_state",$"" },
                { "save_my_address",$"0" },
                { "confirmation",$"{confirmationId}" },
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<BuyAssetResponse> assetsResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    if (obj == null || obj.Type == JTokenType.Null || obj is JArray)
                    {
                        return null;
                    }

                    return obj.ToObject<BuyAssetResponse>();
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 创建订购单
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">HashName</param>
        /// <param name="price">
        /// 订购价格
        /// 单位：分
        /// </param>
        /// <param name="tradefeeTax">
        /// 交易手续费
        /// 单位：分
        /// </param>
        /// <param name="currency">货币类型</param>
        /// <param name="quantity">购买数量</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<CreateBuyOrderResponse>> CreateBuyOrderAsync(string currentSessionId, string appId, string hashName, int price, int tradefeeTax, Currency currency, int quantity, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/createbuyorder/");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "currency",$"{(int)currency}" },
                { "appid",$"{appId}" },
                { "market_hash_name",$"{hashName}" },
                { "price_total",$"{price}" },
                { "tradefee_tax",$"{tradefeeTax}" },
                { "quantity",$"{quantity}" },
                { "billing_state",$"" },
                { "save_my_address",$"0" },
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<CreateBuyOrderResponse> assetsResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    if (obj == null || obj.Type == JTokenType.Null || obj is JArray)
                    {
                        return null;
                    }

                    return obj.ToObject<CreateBuyOrderResponse>();
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询订购单状态
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="buyOrderId">订购单Id</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryBuyOrderStatusResponse>> QueryBuyOrderStatusAsync(string currentSessionId, string buyOrderId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/getbuyorderstatus/?" +
                $"sessionid={currentSessionId}" +
                $"&buy_orderid={buyOrderId}");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryBuyOrderStatusResponse> assetsResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    if (obj == null || obj.Type == JTokenType.Null || obj is JArray)
                    {
                        return null;
                    }

                    return obj.ToObject<QueryBuyOrderStatusResponse>();
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 取消订购单
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="buyOrderId">订购单Id</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<CancelBuyOrderResponse>> CancelBuyOrderAsync(string currentSessionId, string buyOrderId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/cancelbuyorder/");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "buy_orderid",$"{buyOrderId}" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/market/" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<CancelBuyOrderResponse> assetsResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    if (obj == null || obj.Type == JTokenType.Null || obj is JArray)
                    {
                        return null;
                    }

                    return obj.ToObject<CancelBuyOrderResponse>();
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询用户库存
        /// </summary>
        /// <param name="steamId">待查询用户SteamId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="count">查询数量</param>
        /// <param name="lastMaxAssetId">上一次查询最大Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<InventoryResponse>> QueryInventoryAsync(string steamId, string appId, string contextId, int count = 0, ulong? lastMaxAssetId = null, Language language = Language.Schinese, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/inventory/{steamId}/{appId}/{contextId}?" +
                $"l={language.GetApiCode()}" +
                $"{(lastMaxAssetId > 0 ? $"&start_assetid={lastMaxAssetId}" : null)}" +
                $"{(count > 0 ? $"&count={count}" : null)}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<InventoryResponse> assetsResponse = new WebResponse<InventoryResponse>(response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询自己的库存
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="steamId">SteamId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="count">查询数量</param>
        /// <param name="lastMaxAssetId">上一次查询最大Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<InventoryResponse>> QueryInventoryAsync(string? accessToken, string steamId, string appId, string contextId, int count = 0, ulong? lastMaxAssetId = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetInventoryItemsWithDescriptions/v1/?" +
                $"language={language.GetApiCode()}" +
                $"&get_descriptions=1" +
                $"&get_asset_properties=1" +
                $"&key={null}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&steamid={steamId}" +
                $"&appid={appId}" +
                $"&contextid={contextId}" +
                $"{(count > 0 ? $"&count={count}" : null)}" +
                $"{(lastMaxAssetId > 0 ? $"&start_assetid={lastMaxAssetId}" : null)}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<InventoryResponse> assetsResponse = SteamApiResponse<ApiResponse<InventoryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询自己的库存
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="trading">
        /// 是否仅用于交易的库存
        /// <para>传入true：仅能查询 可交易的</para>
        /// <para>传入false：可查询 可交易的 和 不可交易的</para>
        /// </param>
        /// <param name="language">语言类型</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<SelfInventoryResponse>> QueryInventoryAsync(string steamId, string appId, string contextId, bool trading, Language language, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/inventory/json/{appId}/{contextId}/?" +
               $"trading={(trading ? 1 : 0)}" +
               $"&l={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<SelfInventoryResponse> assetsResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    SelfInventoryResponse inventoryResponse = new SelfInventoryResponse
                    {
                        Success = obj.Value<bool>("success"),
                        Error = obj.Value<string>("error"),
                        More = obj.Value<bool>("more"),
                        MoreStart = null,
                        Currency = obj.Value<JToken>("rgCurrency"),
                        Inventories = new List<Inventory>(),
                        Descriptions = new List<SelfInventoryDescription>()
                    };
                    if (inventoryResponse.More && obj.ContainsKey("more_start"))
                    {
                        inventoryResponse.MoreStart = obj.Value<string>("more_start");
                    }
                    if (obj.ContainsKey("rgInventory"))
                    {
                        var rgInventory = obj.Value<JToken>("rgInventory")!;
                        if (rgInventory is JObject keyValues)
                        {
                            foreach (var item in keyValues)
                            {
                                inventoryResponse.Inventories.Add(item.Value!.ToObject<Inventory>()!);
                            }
                        }
                    }
                    if (obj.ContainsKey("rgDescriptions"))
                    {
                        var rgDescriptions = obj.Value<JToken>("rgDescriptions")!;
                        if (rgDescriptions is JObject keyValues)
                        {
                            foreach (var item in keyValues)
                            {
                                inventoryResponse.Descriptions.Add(item.Value!.ToObject<SelfInventoryDescription>()!);
                            }
                        }
                    }
                    return inventoryResponse;
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询对方的库存
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="partnerSteamId">对方的SteamId</param>
        /// <param name="partnerToken">对方的交易链接Token</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="language">语言类型</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<SelfInventoryResponse>> QueryPartnerInventoryAsync(string currentSessionId, string partnerSteamId, string? partnerToken, string appId, string contextId, Language language, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/tradeoffer/new/partnerinventory/?" +
                $"sessionid={currentSessionId}" +
                $"&partner={partnerSteamId}" +
                $"&appid={appId}" +
                $"&contextid={contextId}" +
                $"&l={language.GetApiCode()}");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/tradeoffer/new/?partner={Extensions.GetPartner(partnerSteamId)}&token={partnerToken}" }
            };
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy, headers), cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<SelfInventoryResponse> assetsResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    SelfInventoryResponse inventoryResponse = new SelfInventoryResponse
                    {
                        Success = obj.Value<bool>("success"),
                        Error = obj.Value<string>("error"),
                        More = obj.Value<bool>("more"),
                        MoreStart = null,
                        Currency = obj.Value<JToken>("rgCurrency"),
                        Inventories = new List<Inventory>(),
                        Descriptions = new List<SelfInventoryDescription>()
                    };
                    if (inventoryResponse.More && obj.ContainsKey("more_start"))
                    {
                        inventoryResponse.MoreStart = obj.Value<string>("more_start");
                    }
                    if (obj.ContainsKey("rgInventory"))
                    {
                        var rgInventory = obj.Value<JToken>("rgInventory")!;
                        if (rgInventory is JObject keyValues)
                        {
                            foreach (var item in keyValues)
                            {
                                inventoryResponse.Inventories.Add(item.Value!.ToObject<Inventory>()!);
                            }
                        }
                    }
                    if (obj.ContainsKey("rgDescriptions"))
                    {
                        var rgDescriptions = obj.Value<JToken>("rgDescriptions")!;
                        if (rgDescriptions is JObject keyValues)
                        {
                            foreach (var item in keyValues)
                            {
                                inventoryResponse.Descriptions.Add(item.Value!.ToObject<SelfInventoryDescription>()!);
                            }
                        }
                    }
                    return inventoryResponse;
                });
                return assetsResponse;
            }
        }

        /// <summary>
        /// 查询库存历史记录
        /// </summary>
        /// <param name="steamId">当前登录用户的SteamId</param>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="cursor">索引游标</param>
        /// <param name="language">语言类型</param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryInventoryHistoryResponse>> QueryInventoryHistoryAsync(string steamId, string currentSessionId, IEnumerable<string>? appId, InventoryHistoryCursor? cursor, Language language, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            StringBuilder query = new StringBuilder();
            foreach (var item in appId ?? new string[0])
            {
                query.Append($"app[]={item}&");
            }

            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/inventoryhistory/?" +
                $"ajax=1" +
                $"&l={language.GetApiCode()}" +
                $"&sessionid={currentSessionId}" +
                $"&cursor[time]={cursor?.Time}" +
                $"&cursor[time_frac]={cursor?.TimeFrac}" +
                $"&cursor[s]={cursor?.S}" +
                $"&{query}".TrimEnd('&'));

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, autoRedirect: true, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryInventoryHistoryResponse> inventoryHistoryResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return null;
                    }

                    JObject obj = token.Value<JObject>()!;

                    QueryInventoryHistoryResponse inventoryResponse = new QueryInventoryHistoryResponse
                    {
                        Success = obj.Value<bool>("success"),
                        Num = obj.Value<int>("num"),
                        Html = obj.Value<string>("html"),
                        Cursor = obj.Value<JObject>("cursor")?.ToObject<InventoryHistoryCursor>(),
                        Descriptions = new List<InventoryHistoryDescription>()
                    };

                    if (inventoryResponse.Num > 0 && obj.ContainsKey("descriptions"))
                    {
                        var descriptions = obj.Value<JObject>("descriptions")!;
                        List<InventoryHistoryDescription> baseDescriptions = new List<InventoryHistoryDescription>();
                        InventoryHistoryDescription baseDescription;
                        JToken jToken;
                        foreach (var appDescription in descriptions)
                        {
                            foreach (var description in appDescription.Value!)
                            {
                                jToken = description.First()!;
                                jToken["appid"] = appDescription.Key;
                                baseDescription = jToken.ToObject<InventoryHistoryDescription>()!;
                                baseDescriptions.Add(baseDescription);
                            }
                        }
                        inventoryResponse.Descriptions = baseDescriptions;
                    }

                    return inventoryResponse;
                });
                return inventoryHistoryResponse;
            }
        }

        /// <summary>
        /// 确认报价公告
        /// </summary>
        /// <param name="steamId"></param>
        /// <param name="currentSessionId"></param>
        /// <param name="userCookies"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> AcknowledgeOfferAsync(string steamId, string currentSessionId, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/trade/new/acknowledge");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "message","1" },
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/profiles/{steamId}/tradeoffers/" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<bool> acknowledgeResponse = new WebResponse<JToken>(response).Convert(obj =>
                {
                    return response.StatusCode == HttpStatusCode.OK;
                });
                return acknowledgeResponse;
            }
        }

        /// <summary>
        /// 发送报价
        /// </summary>
        /// <param name="currentSessionId">
        /// 当前登录用户的SessionId
        /// 报价发起方的SessionId
        /// </param>
        /// <param name="receiverSteamId">报价接收方Steam用户Id</param>
        /// <param name="receiverPartner">
        /// 报价接收方Partner
        /// 来自报价接收方的交易连接
        /// </param>
        /// <param name="receiverToken">
        /// 报价接收方Token</param>
        /// 来自报价接收方的交易连接
        /// <param name="currentAssets">报价发起方交换的资产</param>
        /// <param name="receiverAssets">报价接收方交换的资产</param>
        /// <param name="postscript">交易附言</param>
        /// <param name="currentCookies">
        /// 当前登录用户Cookie
        /// 报价发起方的Cookie
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<SendOfferResponse>> SendOfferAsync(string currentSessionId, string receiverSteamId, string receiverPartner, string receiverToken, IEnumerable<SendOfferAsset>? currentAssets, IEnumerable<SendOfferAsset>? receiverAssets, string? postscript, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/tradeoffer/new/send");
            var tradeOffer = new
            {
                newversion = true,
                version = 2,
                me = new { assets = currentAssets ?? new List<SendOfferAsset>(), currency = new object[0], ready = false },
                them = new { assets = receiverAssets ?? new List<SendOfferAsset>(), currency = new object[0], ready = false }
            };
            var tradeOfferParams = new
            {
                trade_offer_access_token = receiverToken
            };
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "serverid","1" },
                { "partner",receiverSteamId },
                { "tradeoffermessage",postscript??"" },
                { "json_tradeoffer",JsonConvert.SerializeObject(tradeOffer) },
                { "trade_offer_create_params",JsonConvert.SerializeObject(tradeOfferParams) },
                { "captcha","" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/tradeoffer/new/?partner={receiverPartner}&token={receiverToken}" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<SendOfferResponse> senfOfferResponse = new WebResponse<SendOfferResponse>(response);
                return senfOfferResponse;
            }
        }

        /// <summary>
        /// 查询报价
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="sentOffer">查询发送的报价</param>
        /// <param name="receivedOffer">查询接收的报价</param>
        /// <param name="onlyActive">只查询活跃的报价</param>
        /// <param name="getDescriptions">是否获取资产描述信息</param>
        /// <param name="cursor">开始索引</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<OffersResponse>> QueryOffersAsync(string accessToken, bool sentOffer = true, bool receivedOffer = true, bool onlyActive = true, bool getDescriptions = true, long cursor = 0, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            ulong time_historical_cutoff = await Extensions.GetSteamTimestampAsync().ConfigureAwait(false);
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeOffers/v1/?" +
                $"key={null}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&get_sent_offers={(sentOffer ? 1 : 0)}" +
                $"&get_received_offers={(receivedOffer ? 1 : 0)}" +
                $"&active_only={(onlyActive ? 1 : 0)}" +
                $"&get_descriptions={(getDescriptions ? 1 : 0)}" +
                $"&language={language.GetApiCode()}" +
                $"&time_historical_cutoff={time_historical_cutoff}" +
                $"&cursor={cursor}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<OffersResponse> offerResponse = SteamApiResponse<ApiResponse<OffersResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 查询报价
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="offerId">报价Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="getDescriptions">是否获取资产描述信息</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<OfferResponse>> QueryOfferAsync(string accessToken, string offerId, bool getDescriptions = true, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeOffer/v1/?" +
                $"key={null}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&tradeofferid={offerId}" +
                $"&get_descriptions={(getDescriptions ? 1 : 0)}" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<OfferResponse> offerResponse = SteamApiResponse<ApiResponse<OfferResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 查询交易历史
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="count">数量</param>
        /// <param name="afterTime">上一次返回的最后一条记录时间</param>
        /// <param name="afterTradeId">上一次返回的最后一条记录交易Id</param>
        /// <param name="includeFailed">是否包含失败的交易</param>
        /// <param name="getDescriptions">是否获取资产描述信息</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<TradeHistoryResponse>> QueryTradeHistoryAsync(string accessToken, int count = 10, long? afterTime = null, string? afterTradeId = null, bool includeFailed = false, bool getDescriptions = true, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeHistory/v1/?" +
                $"key={null}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&max_trades={count}" +
                $"&start_after_time={afterTime}" +
                $"&start_after_tradeid={afterTradeId}" +
                $"&include_failed={(includeFailed ? 1 : 0)}" +
                $"&get_descriptions={(getDescriptions ? 1 : 0)}" +
                $"&navigating_back=0" +
                $"&include_total=1" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<TradeHistoryResponse> offerResponse = SteamApiResponse<ApiResponse<TradeHistoryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 查询交易状态
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="tradeId">交易Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<TradeStatusResponse>> QueryTradeStatusAsync(string accessToken, string tradeId, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeStatus/v1/?" +
                $"key={null}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}" +
                $"&tradeid={tradeId}" +
                $"&get_descriptions=1" +
                $"&language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<TradeStatusResponse> offerResponse = SteamApiResponse<ApiResponse<TradeStatusResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 查询交易报价摘要
        /// 查询待处理和新交易报价的数量
        /// </summary>
        /// <param name="apiKey">
        /// 用户ApiKey
        /// 与accessToken不能同时为空
        /// </param>
        /// <param name="accessToken">
        /// 用户登录Token
        /// 与apiKey不能同时为空
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryTradeOffersSummaryResponse>> QueryTradeOffersSummaryAsync(string? apiKey, string? accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IEconService/GetTradeOffersSummary/v1/?" +
                $"key={apiKey}" +
                $"&access_token={Uri.EscapeDataString(accessToken ?? "")}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryTradeOffersSummaryResponse> offerResponse = SteamApiResponse<ApiResponse<QueryTradeOffersSummaryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 接受报价
        /// </summary>
        /// <param name="currentSessionId">
        /// 当前登录用户的SessionId
        /// 报价接收方的SessionId
        /// </param>
        /// <param name="offerId">报价Id</param>
        /// <param name="currentCookies">
        /// 当前登录用户Cookie
        /// 报价接收方的Cookie
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AcceptOfferResponse>> AcceptOfferAsync(string currentSessionId, string offerId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/tradeoffer/{offerId}/accept");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "serverid","1" },
                //{ "partner",senderSteamId },
                { "tradeofferid",offerId },
                { "captcha","" }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/tradeoffer/{offerId}" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AcceptOfferResponse> offerResponse = new WebResponse<AcceptOfferResponse>(response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 拒绝报价
        /// </summary>
        /// <param name="currentSessionId">
        /// 当前登录用户的SessionId
        /// 报价接收方的SessionId
        /// </param>
        /// <param name="offerId">报价Id</param>
        /// <param name="currentCookies">
        /// 当前登录用户Cookie
        /// 报价接收方的Cookie
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<DeclineOfferResponse>> DeclineOfferAsync(string currentSessionId, string offerId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/tradeoffer/{offerId}/decline");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/tradeoffer/{offerId}" },
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<DeclineOfferResponse> offerResponse = new WebResponse<DeclineOfferResponse>(response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 取消报价
        /// </summary>
        /// <param name="currentSessionId">
        /// 当前登录用户的SessionId
        /// 报价发起方的SessionId
        /// </param>
        /// <param name="offerId">报价Id</param>
        /// <param name="currentCookies">
        /// 当前登录用户Cookie
        /// 报价发起方的Cookie
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<CancelOfferResponse>> CancelOfferAsync(string currentSessionId, string offerId, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/tradeoffer/{offerId}/cancel");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCommunity}/tradeoffer/{offerId}"/*$"{SteamCommunity}/profiles/{currentSteamId}/tradeoffers/sent/"*/ }
            };
            headers = SetDefaultHeaders(proxy, headers);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, currentCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<CancelOfferResponse> offerResponse = new WebResponse<CancelOfferResponse>(response);
                return offerResponse;
            }
        }

        /// <summary>
        /// 查询待确认信息
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryConfirmationsResponse>> QueryConfirmationsAsync(string currentSteamId, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/mobileconf/getlist?{InitConfirmationParams(currentSteamId, deviceId, identitySecret, "list", timestamp)}");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent",$"okhttp/3.12.12" }
            };
            headers = SetMobileHeaders(proxy, headers);
            headers = SetDefaultHeaders(proxy, headers);
            CookieCollection cookies = SetMobileCookies(currentCookies);
            cookies = SetDefaultCookies(proxy, cookies);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: cookies, proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryConfirmationsResponse> confirmationResponse = new WebResponse<QueryConfirmationsResponse>(response);
                return confirmationResponse;
            }
        }

        /// <summary>
        /// 查询待确认信息详情
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="confirmationId">待确认信息Id</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>HTML</returns>
        public static async Task<IWebResponse<string>> ConfirmationDetailAsync(string currentSteamId, string confirmationId, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/mobileconf/detailspage/{confirmationId}?{InitConfirmationParams(currentSteamId, deviceId, identitySecret, "detail", timestamp)}");

            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers = SetMobileHeaders(proxy, headers);
            headers = SetDefaultHeaders(proxy, headers);
            CookieCollection cookies = SetMobileCookies(currentCookies);
            cookies = SetDefaultCookies(proxy, cookies);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: headers, cookies: cookies, proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> detailResponse = new WebResponse<string>(response);
                return detailResponse;
            }
        }

        /// <summary>
        /// 接受确认信息
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="confirmationId">待确认信息Id</param>
        /// <param name="confirmationKey">待确认信息Key</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> AllowConfirmationAsync(string currentSteamId, string confirmationId, string confirmationKey, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            return await SendConfirmationAsync(proxy, currentSteamId, confirmationId, confirmationKey, deviceId, identitySecret, "allow", timestamp, currentCookies, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消确认信息
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="confirmationId">待确认信息Id</param>
        /// <param name="confirmationKey">待确认信息Key</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> CancelConfirmationAsync(string currentSteamId, string confirmationId, string confirmationKey, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            return await SendConfirmationAsync(proxy, currentSteamId, confirmationId, confirmationKey, deviceId, identitySecret, "cancel", timestamp, currentCookies, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 接受确认信息
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="confirmations">待确认信息</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> AllowMultiConfirmationAsync(string currentSteamId, IEnumerable<ConfirmationBase> confirmations, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            return await SendMultiConfirmationAsync(proxy, currentSteamId, confirmations, deviceId, identitySecret, "allow", timestamp, currentCookies, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消确认信息
        /// </summary>
        /// <param name="currentSteamId">当前登录用户SteamId</param>
        /// <param name="confirmations">待确认信息</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="identitySecret">身份秘钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> CancelMultiConfirmationAsync(string currentSteamId, IEnumerable<ConfirmationBase> confirmations, string deviceId, string identitySecret, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            return await SendMultiConfirmationAsync(proxy, currentSteamId, confirmations, deviceId, identitySecret, "cancel", timestamp, currentCookies, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询国家
        /// </summary>
        /// <param name="language">语言</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<QueryCountryResponse>> QueryCountryAsync(Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IStoreTopSellersService/GetCountryList/v1/?" +
                $"language={language.GetApiCode()}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<QueryCountryResponse> countryResponse = SteamApiResponse<ApiResponse<QueryCountryResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return countryResponse;
            }
        }

        /// <summary>
        /// 获取服务器连接地址
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CMListResponse>> QuetyCMListAsync(int count, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamDirectory/GetCMList/v1/?" +
                $"format=json" +
                $"&cellid=" +
                $"&maxcount={count}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<CMListResponse> steamResponse = SteamApiResponse<ApiResponse<CMListResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return steamResponse;
            }
        }

        /// <summary>
        /// 获取服务器连接地址
        /// </summary>
        /// <param name="type">
        /// 连接类型
        /// websockets
        /// netfilter
        /// </param>
        /// <param name="count">数量</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CMListForConnectResponse>> QuetyCMListForConnectAsync(string? type, int count = 100, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/ISteamDirectory/GetCMListForConnect/v1/?" +
                $"format=json" +
                $"&cellid=" +
                $"&cmtype={type}" +
                $"&maxcount={count}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<CMListForConnectResponse> steamResponse = SteamApiResponse<ApiResponse<CMListForConnectResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return steamResponse;
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">请求头</param>
        /// <param name="currentCookies">用户Cookie</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse> GetAsync(string url, IDictionary<string, string>? headers = null, CookieCollection? currentCookies = null, IWebProxy? proxy = null, CancellationToken cancellationToken = default)
        {
            using (var response = await InternalHttpClient.SendAsync(new Uri(url), HttpMethod.Get, null, InternalHttpClient.ResultType.TextPlain, headers: SetDefaultHeaders(Proxy.Instance, headers), cookies: SetDefaultCookies(Proxy.Instance, currentCookies), proxy: proxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse streamResponse = new Model.Internal.WebResponse(response);
                return streamResponse;
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">请求头</param>
        /// <param name="currentCookies">用户Cookie</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<T>> GetAsync<T>(string url, IDictionary<string, string>? headers = null, CookieCollection? currentCookies = null, IWebProxy? proxy = null, CancellationToken cancellationToken = default)
        {
            using (var response = await InternalHttpClient.SendAsync(new Uri(url), HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(Proxy.Instance, headers), cookies: SetDefaultCookies(Proxy.Instance, currentCookies), proxy: proxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<T> streamResponse = new WebResponse<T>(response);
                return streamResponse;
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="content">消息体</param>
        /// <param name="headers">请求头</param>
        /// <param name="currentCookies">用户Cookie</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<T>> PostAsync<T>(string url, HttpContent? content, IDictionary<string, string>? headers = null, CookieCollection? currentCookies = null, IWebProxy? proxy = null, CancellationToken cancellationToken = default)
        {
            using (var response = await InternalHttpClient.SendAsync(new Uri(url), HttpMethod.Post, content, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(Proxy.Instance, headers), cookies: SetDefaultCookies(Proxy.Instance, currentCookies), proxy: proxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<T> streamResponse = new WebResponse<T>(response);
                return streamResponse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="currentSteamId"></param>
        /// <param name="confirmationId"></param>
        /// <param name="confirmationKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="identitySecret"></param>
        /// <param name="tag"></param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        private static async Task<IWebResponse<bool>> SendConfirmationAsync(Proxy proxy, string currentSteamId, string confirmationId, string confirmationKey, string deviceId, string identitySecret, string tag, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri($"{proxy.SteamCommunity}/mobileconf/ajaxop?{InitConfirmationParams(currentSteamId, deviceId, identitySecret, tag, timestamp)}" +
                $"&op={tag}" +
                $"&cid={confirmationId}" +
                $"&ck={confirmationKey}");

            var @params = JsonContent.Create(new
            {
                withCredentials = true
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent",$"okhttp/3.12.12" }
            };
            headers = SetMobileHeaders(proxy, headers);
            headers = SetDefaultHeaders(proxy, headers);
            CookieCollection cookies = SetMobileCookies(currentCookies);
            cookies = SetDefaultCookies(proxy, cookies);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: cookies, proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<bool> configResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return false;
                    }

                    JObject obj = token.Value<JObject>()!;

                    return obj?.Value<bool>("success") ?? false;
                });
                return configResponse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="currentSteamId"></param>
        /// <param name="confirmations"></param>
        /// <param name="deviceId"></param>
        /// <param name="identitySecret"></param>
        /// <param name="tag"></param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="currentCookies"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<IWebResponse<bool>> SendMultiConfirmationAsync(Proxy proxy, string currentSteamId, IEnumerable<ConfirmationBase> confirmations, string deviceId, string identitySecret, string tag, ulong timestamp, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri($"{proxy.SteamCommunity}/mobileconf/multiajaxop?{InitConfirmationParams(currentSteamId, deviceId, identitySecret, tag, timestamp)}" +
                $"&op={tag}" +
                $"&{string.Join('&', confirmations.Select(c => $"cid[]={c.Id}&ck[]={c.Key}"))}");
            var @params = new StringContent(uri.Query.TrimStart('?'), Encoding.UTF8, MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded"));

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent",$"okhttp/3.12.12" }
            };
            headers = SetMobileHeaders(proxy, headers);
            headers = SetDefaultHeaders(proxy, headers);
            CookieCollection cookies = SetMobileCookies(currentCookies);
            cookies = SetDefaultCookies(proxy, cookies);
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: cookies, proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<bool> configResponse = new WebResponse<JToken>(response).Convert(token =>
                {
                    if (token == null || token.Type == JTokenType.Null || token is JArray)
                    {
                        return false;
                    }

                    JObject obj = token.Value<JObject>()!;
                    return obj?.Value<bool>("success") ?? false;
                });
                return configResponse;
            }
        }

        /// <summary>
        /// m=
        /// tag=
        /// p=
        /// a=
        /// k=
        /// t=
        /// </summary>
        /// <param name="currentSteamId"></param>
        /// <param name="deviceId"></param>
        /// <param name="identitySecret"></param>
        /// <param name="tag"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private static string InitConfirmationParams(string currentSteamId, string deviceId, string identitySecret, string tag, ulong timestamp)
        {
            string confirmationHash = GuardCodeGenerator.GenerateConfirmationCode(timestamp, identitySecret, tag)!;

            string @params = $"m=react" +
                $"&tag={tag}" +
                $"&p={deviceId}" +
                $"&a={currentSteamId}" +
                $"&k={confirmationHash}" +
                $"&t={timestamp}";

            return @params;
        }
    }
}
