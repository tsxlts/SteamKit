using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;

namespace SteamKit
{
    /// <summary>
    /// SteamApi
    /// </summary>
    public partial class SteamStoreApi
    {
        /// <summary>
        /// 提交批准交易请求
        /// </summary>
        /// <param name="appId">AppId 游戏Id</param>
        /// <param name="orderId">订单号</param>
        /// <param name="loginCookies">用户登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> FinalizeStorePurchaseAsync(string appId, string orderId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/buyitem/{appId}/finalize/{orderId}?" +
                $"canceledurl={Uri.EscapeDataString("https://store.steampowered.com")}" +
                $"&returnhost={Uri.EscapeDataString("store.steampowered.com")}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> steamResponse = new WebResponse<string>(response);
                return steamResponse;
            }
        }

        /// <summary>
        /// 注销用户所有登录Token
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="loginCookies">用户登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> DeleteAuthorizeAsync(string currentSessionId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/twofactor/manage_action");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"action","deauthorize" },
                {"sessionid",currentSessionId }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> deleteAuthorizeResponse = new WebResponse<string>(response);
                return deleteAuthorizeResponse;
            }
        }

        /// <summary>
        /// 获取用户帐号设置
        /// </summary>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AccountSettingResponse>> GetAccountSettingAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/account/?l=schinese");
            CookieCollection cookies = new CookieCollection
            {
                loginCookies ,
                //new Cookie(Extension.SteamLanguageCookeName, "english")
            };
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AccountSettingResponse> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    var steamId = string.Empty;
                    var accountName = string.Empty;
                    var accountId = string.Empty;
                    var countryCode = string.Empty;
                    var country = string.Empty;

                    var balance = string.Empty;

                    var email = string.Empty;
                    var phone = string.Empty;

                    Match match = match = Regex.Match(html ?? "", @"data-userinfo=""(?<userinfo>.+?)""");
                    if (match.Success)
                    {
                        var json = HttpUtility.HtmlDecode(match.Groups["userinfo"].Value);
                        JToken userInfo = JsonConvert.DeserializeObject<JToken>(json)!;

                        steamId = userInfo.Value<string>("steamid");
                        accountName = userInfo.Value<string>("account_name");
                        accountId = userInfo.Value<string>("accountid");
                        countryCode = userInfo.Value<string>("country_code");
                    }

                    match = Regex.Match(html ?? "", @"(class=""accountData price"")([\w\W]*?)>(?<balance>[^><]+)<");
                    if (match.Success)
                    {
                        balance = match.Groups["balance"].Value;
                    }

                    match = Regex.Match(html ?? "", @"(Email address|电子邮件地址)([:：])([\w\W]*?)<span([\w\W]*?)class=""account_data_field""([\w\W]*?)>(?<email>.+?)</span>");
                    if (match.Success)
                    {
                        email = match.Groups["email"].Value;
                    }

                    match = Regex.Match(html ?? "", @"(Phone|手机)([:：])([\w\W]*?)<span([\w\W]*?)class=""account_data_field""([\w\W]*?)>(?<phone>.+?)</span>");
                    if (match.Success)
                    {
                        phone = match.Groups["phone"].Value;
                    }

                    match = Regex.Match(html ?? "", @"(Country|国家/地区)([:：])([\w\W]*?)<span([\w\W]*?)class=""account_data_field""([\w\W]*?)>(?<country>.+?)</span>");
                    if (match.Success)
                    {
                        country = match.Groups["country"].Value;
                    }

                    return new AccountSettingResponse
                    {
                        AccountName = accountName,
                        AccountId = accountId,
                        SteamId = steamId,
                        CountryCode = countryCode,
                        Country = country,
                        Balance = balance,
                        Email = email,
                        Phone = phone
                    };
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="phone">
        /// 手机号
        /// 需要带上国际区号
        /// +86 15022223333
        /// </param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<ValidatePhoneResponse>> ValidatePhoneAsync(string currentSessionId, string phone, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/phone/validate");
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{SteamBulider.DefaultSteamStore}/phone/add" }
            };
            headers = SetDefaultHeaders(proxy, headers);
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionID",currentSessionId },
                { "phoneNumber",phone }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<ValidatePhoneResponse> validatePhoneResponse = new WebResponse<ValidatePhoneResponse>(response);
                return validatePhoneResponse;
            }
        }

        /// <summary>
        /// 添加手机号
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        ///  <param name="operate">当前流程</param>
        /// <param name="operateValue">
        /// 1、开始传入手机号,需要带上国际区号,+86 15022223333
        /// 2、发送验证码,传入空
        /// 3、短信验证,传入短信验证码
        /// </param>
        /// <param name="userCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AddPhoneResponse>> AddPhoneAsync(string currentSessionId, AddPhoneOperate operate, string operateValue, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/phone/add_ajaxop");
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{SteamBulider.DefaultSteamStore}/phone/add" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            string op = "get_phone_number";
            switch (operate)
            {
                case AddPhoneOperate.Begin:
                    op = "get_phone_number";
                    break;

                case AddPhoneOperate.SendSmsCode:
                    op = "email_verification";
                    break;

                case AddPhoneOperate.VerificationSmsCode:
                    op = "get_sms_code";
                    break;
            }
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionID",currentSessionId },
                { "input",operateValue },
                { "op",op },
                { "confirmed","1" },
                { "checkfortos","1" },
                { "bisediting","0" },
                { "token","0" }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AddPhoneResponse> addPhoneResponse = new WebResponse<AddPhoneResponse>(response);
                return addPhoneResponse;
            }
        }

        /// <summary>
        /// 移除令牌验证器
        /// </summary>
        /// <param name="currentSessionId">当前登录用户的SessionId</param>
        /// <param name="revocationCode">令牌恢复码</param>
        /// <param name="currentCookies">当前登录用户的Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<bool>> RemoveAuthenticatorAsync(string currentSessionId, string revocationCode, CookieCollection currentCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamStore}/twofactor/manage_remove_revocation_code");
            IDictionary<string, string> headers = SetDefaultHeaders(proxy);
            CookieCollection cookies = new CookieCollection
            {
                currentCookies ,
                new Cookie(Extensions.SteamLanguageCookeName, "english")
            };
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid",currentSessionId },
                { "revocation_code",revocationCode }
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<bool> removeAuthenticatorResponse = new WebResponse<string>(response).Convert((html) =>
                {
                    if (string.IsNullOrWhiteSpace(html))
                    {
                        return false;
                    }

                    return html.Contains("Authenticator removed", StringComparison.CurrentCultureIgnoreCase) || html.Contains("You successfully removed the Authenticator from your account", StringComparison.CurrentCultureIgnoreCase);
                });
                return removeAuthenticatorResponse;
            }
        }
    }
}
