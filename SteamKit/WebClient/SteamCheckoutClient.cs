using SteamKit.Api;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.SteamEnum;

namespace SteamKit.WebClient
{
    /// <summary>
    /// Steam收银台客户端
    /// </summary>
    public class SteamCheckoutClient : SteamWebClient
    {
        /// <summary>
        /// 
        /// </summary>
        public SteamCheckoutClient() : base()
        {
        }

        /// <summary>
        /// 站点
        /// </summary>
        protected override Website Website => Website.Checkout;

        /// <summary>
        /// 验证AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task<(bool Success, CookieCollection Cookies)> VerifyAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var cookies = new CookieCollection
            {
                new Cookie(Extensions.SteamSessionidCookieName,CreateSessionId()),
                new Cookie(Extensions.SteamAccessTokenCookeName, accessToken)
            };
            var checkToken = await SteamApi.GetAsync($"{proxy.SteamCheckout}/checkout?cart={Extensions.GetSystemMilliTimestamp()}&microtxn=-1", null, cookies, proxy.WebProxy, cancellationToken).ConfigureAwait(false);
            bool invalid = !proxy.SteamCheckout.Equals(checkToken.Headers.Location?.ToString()?.TrimEnd('/'), StringComparison.CurrentCultureIgnoreCase);
            if (invalid)
            {
                return (false, new CookieCollection());
            }

            cookies.Add(checkToken.Cookies);
            return (true, cookies);
        }
    }
}
