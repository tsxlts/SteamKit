using SteamKit.Model;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;
using static SteamKit.SteamEnum;

namespace SteamKit.WebClient
{
    /// <summary>
    /// Steam客服客户端
    /// </summary>
    public class SteamHelpClient : SteamWebClient
    {
        /// <summary>
        /// 
        /// </summary>
        public SteamHelpClient() : base()
        {
        }

        /// <summary>
        /// 检测自己是否交易
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <exception cref="NotLoginException"></exception>
        /// <returns></returns>
        public async Task<TradableCheckResponse?> TradableCheckAsync(CancellationToken cancellationToken = default)
        {
            this.CheckLogon();

            var response = await SteamHelpApi.TradableCheckAsync(this.WebCookie, cancellationToken).ConfigureAwait(false);
            return ThrowIfError(response).Body;
        }

        /// <summary>
        /// 站点
        /// </summary>
        protected override Website Website => Website.Help;

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
            var checkToken = await SteamApi.GetAsync($"{proxy.SteamHelp}/zh-cn/", null, cookies, proxy.WebProxy, cancellationToken).ConfigureAwait(false);
            bool invalid = checkToken.Cookies.Any(c => "deleted".Equals(c.Value, StringComparison.InvariantCultureIgnoreCase)
            && (Extensions.SteamAccessTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase) || "steamLogin".Equals(c.Name, StringComparison.CurrentCultureIgnoreCase)));
            if (invalid)
            {
                return (false, new CookieCollection());
            }

            cookies.Add(checkToken.Cookies);
            return (true, cookies);
        }
    }
}
