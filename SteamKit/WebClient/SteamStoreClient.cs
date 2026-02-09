using SteamKit.Api;
using SteamKit.Model;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Enums;
using static SteamKit.Internal.Utils;

namespace SteamKit.WebClient
{
    /// <summary>
    /// Steam商店客户端
    /// </summary>
    public class SteamStoreClient : SteamWebClient
    {
        /// <summary>
        /// 
        /// </summary>
        public SteamStoreClient() : base()
        {
        }

        /// <summary>
        /// 获取用户帐号设置
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <exception cref="NotLoginException"></exception>
        /// <returns></returns>
        public async Task<AccountSettingResponse?> GetAccountSettingAsync(CancellationToken cancellationToken = default)
        {
            this.CheckLogon();

            var playerResponse = await SteamStoreApi.GetAccountSettingAsync(this.WebCookie, cancellationToken).ConfigureAwait(false);
            return ThrowIfError(playerResponse).Body;
        }

        /// <summary>
        /// 添加手机号
        /// </summary>
        /// <param name="operate">当前流程</param>
        /// <param name="operateValue">
        /// 流程参数
        /// 1、开始传入手机号,需要带上国际区号,+86 15022223333
        /// 2、发送验证码,传入空
        /// 3、短信验证,传入短信验证码
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <exception cref="NotLoginException"></exception>
        /// <returns></returns>
        public async Task<AddPhoneResponse?> AddPhoneAsync(AddPhoneOperate operate, string operateValue, CancellationToken cancellationToken = default)
        {
            this.CheckLogon();

            var inventoryResponse = await SteamStoreApi.AddPhoneAsync(this.SessionId!, operate, operateValue, this.WebCookie, cancellationToken).ConfigureAwait(false);
            return ThrowIfError(inventoryResponse).Body;
        }

        /// <summary>
        /// 移除令牌验证器
        /// </summary>
        /// <param name="revocationCode">令牌恢复码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<bool> RemoveAuthenticatorAsync(string revocationCode, CancellationToken cancellationToken = default)
        {
            this.CheckLogon();

            var response = await SteamStoreApi.RemoveAuthenticatorAsync(this.SessionId!, revocationCode, this.WebCookie, cancellationToken).ConfigureAwait(false);
            return ThrowIfError(response).Body;
        }

        /// <summary>
        /// 注销用户所有登录Token
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<string?> DeleteAuthorizeAsync(CancellationToken cancellationToken = default)
        {
            this.CheckLogon();

            var response = await SteamStoreApi.DeleteAuthorizeAsync(this.SessionId!, this.WebCookie, cancellationToken).ConfigureAwait(false);
            return ThrowIfError(response).Body;
        }

        /// <summary>
        /// 站点
        /// </summary>
        protected override Website Website => Website.Store;

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
            var checkToken = await SteamApi.GetAsync(proxy.SteamStore, null, cookies, proxy.WebProxy, cancellationToken).ConfigureAwait(false);
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
