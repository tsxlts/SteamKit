using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;

namespace SteamKit.Api
{
    /// <summary>
    /// SteamApi
    /// </summary>
    public partial class SteamCheckoutApi
    {
        /// <summary>
        /// 提交批准交易请求
        /// </summary>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="transId">交易Id</param>
        /// <param name="approved">是否批准交易</param>
        /// <param name="returnurl">返回地址</param>
        /// <param name="loginCookies">用户登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> ApproveTxnSubmitAsync(string currentSessionId, string transId, bool approved, string? returnurl, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCheckout}/checkout/approvetxnsubmit");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"transaction_id",transId },
                {"approved",$"{(approved ? 1 : 0)}" },
                {"returnurl",returnurl ?? "steam" },
                {"sessionid",currentSessionId }
            });

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Referer",$"{DefaultSteamCheckout}/checkout/approvetxn/{transId}/?returnurl={returnurl ?? "steam"}" }
            };
            headers = SetDefaultHeaders(proxy, headers);

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.HtmlFormate, headers: headers, cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> steamResponse = new WebResponse<string>(response);
                return steamResponse;
            }
        }
    }
}
