using System.Net;
using System.Text.RegularExpressions;
using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Internal.Utils;
using static SteamKit.Builder.ProxyBulider;
using SteamKit.Builder;

namespace SteamKit.Api
{
    /// <summary>
    /// SteamApi
    /// </summary>
    public partial class SteamHelpApi
    {
        /// <summary>
        /// 检测自己是否交易
        /// </summary>
        /// <param name="userCookies">当前登录用户SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<TradableCheckResponse>> TradableCheckAsync(CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamHelp}/zh-cn/wizard/HelpWhyCantITrade");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var objResponse = new WebResponse<string>(response);
                IWebResponse<TradableCheckResponse> stringResponse = objResponse.Convert(html =>
                {
                    if (objResponse.HttpStatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    var match = Regex.Match(html ?? "", "<div class=\"help_game_header\">交易</div><p>我们证实您的帐户(?<name>.+)当前无法使用交易功能。</p><div class=\"info_box\">(?<reason>.+)</div >");
                    if (!match.Success)
                    {
                        return new TradableCheckResponse
                        {
                            Tradable = true
                        };
                    }

                    string reason = match.Groups["reason"].Value;
                    string reasonContent = Regex.Replace(reason, "<[^/>]+>|</[^>]+>$", "");
                    reasonContent = Regex.Replace(reasonContent, "</[^>]+>", Environment.NewLine);

                    return new TradableCheckResponse
                    {
                        Tradable = false,
                        Reason = reasonContent
                    };
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 撤销所有交易
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="userCookies">当前登录用户SteamId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<RevertEligibleTradesResponse>> RevertEligibleTradesAsync(string currentSessionId, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamHelp}/zh-cn/wizard/AjaxRevertEligibleTrades/");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{ProxyBulider.DefaultSteamHelp}/zh-cn/wizard/HelpTradeRestore" }
            };

            headers = SetDefaultHeaders(proxy, headers);
            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "sessionid", currentSessionId },
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: headers, cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<RevertEligibleTradesResponse> steamResponse = new WebResponse<RevertEligibleTradesResponse>(response);
                return steamResponse;
            }
        }
    }
}
