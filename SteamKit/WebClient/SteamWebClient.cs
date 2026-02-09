using System.Diagnostics.CodeAnalysis;
using System.Text;
using SteamKit.Api;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Factory;
using SteamKit.Model;
using static SteamKit.Enums;

namespace SteamKit.WebClient
{
    /// <summary>
    /// SteamWebClient
    /// </summary>
    public abstract class SteamWebClient
    {
        private Timer timer;
        private string? refreshToken;

        /// <summary>
        /// 
        /// </summary>
        public SteamWebClient()
        {
            InitTimer();
        }

        /// <summary>
        /// 开始授权登录
        /// </summary>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="guardData">令牌信息</param>
        /// <param name="platformType">登录平台</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CAuthentication_BeginAuthSessionViaCredentials_Response?> BeginLoginAsync(string username, string password, string? guardData, EAuthTokenPlatformType platformType = EAuthTokenPlatformType.k_EAuthTokenPlatformType_WebBrowser, CancellationToken cancellationToken = default)
        {
            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                if (!string.IsNullOrWhiteSpace(AccessToken))
                {
                    ClearSession();
                }

                var rsaResult = await SteamAuthentication.QueryRsaPublicKeyAsync(username, tokenSource.Token).ConfigureAwait(false);
                if (rsaResult.ResultCode != ErrorCodes.OK || rsaResult.Body == null)
                {
                    return null;
                }

                Logger?.LogDebug($"GetRsaKey HttpStatusCode: {rsaResult.HttpStatusCode}, ResultCode: {rsaResult.ResultCode}");

                var loginResult = await SteamAuthentication.BeginAuthSessionViaCredentialsAsync(username, password, guardData, platformType, rsaResult.Body, tokenSource.Token).ConfigureAwait(false);

                Logger?.LogDebug($"BeginAuthSessionViaCredentials HttpStatusCode: {loginResult.HttpStatusCode}, ResultCode: {loginResult.ResultCode}");

                return loginResult.Body;
            }
        }

        /// <summary>
        /// 查询授权登录状态
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="requestId">RequestId</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> PollLoginStatusAsync(ulong steamId, ulong clientId, byte[] requestId, CancellationToken cancellationToken = default)
        {
            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var loginResult = await SteamAuthentication.PollAuthSessionStatusAsync(clientId, requestId, tokenSource.Token).ConfigureAwait(false);

                Logger?.LogDebug($"PollAuthSessionStatus HttpStatusCode: {loginResult.HttpStatusCode}, ResultCode: {loginResult.ResultCode}");

                if (string.IsNullOrWhiteSpace(loginResult.Body?.refresh_token))
                {
                    return false;
                }

                string refreshToken = $"{steamId}||{loginResult.Body.refresh_token}";
                return await RefreshAccessTokenAsync(refreshToken, tokenSource.Token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 令牌确认登录
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="guardType">令牌方式</param>
        /// <param name="guardCode">令牌码</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<bool> ConfirmLoginWithGuardCodeAsync(ulong steamId, ulong clientId, EAuthSessionGuardType guardType, string guardCode, CancellationToken cancellationToken = default)
        {
            var loginResult = await SteamAuthentication.UpdateAuthSessionWithSteamGuardCodeAsync(steamId, clientId, guardType, guardCode, cancellationToken).ConfigureAwait(false);

            Logger?.LogDebug($"UpdateAuthSessionWithSteamGuardCode HttpStatusCode: {loginResult.HttpStatusCode}, ResultCode: {loginResult.ResultCode}");

            return loginResult.ResultCode == ErrorCodes.OK;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="token">
        /// AccessToken或者RefreshToken
        /// <para>格式SteamId||Token</para>
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string token, CancellationToken cancellationToken = default)
        {
            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                ulong timestamp;
                try
                {
                    timestamp = await Extensions.GetSteamTimestampAsync().ConfigureAwait(false);
                }
                catch
                {
                    timestamp = Extensions.GetSystemTimestamp();
                }

                var steamToken = token.GetSteamToken();
                if (steamToken.exp < timestamp)
                {
                    return false;
                }

                var array = token.Split("||");
                if (array.Length != 2 || array[0] != steamToken.sub)
                {
                    throw new ArgumentException("token错误", nameof(token));
                }

                if (!string.IsNullOrWhiteSpace(AccessToken))
                {
                    ClearSession();
                }

                if (!steamToken.IsRefreshToken())
                {
                    return await CheckAccessTokenAsync(token, tokenSource.Token).ConfigureAwait(false);
                }

                return await RefreshAccessTokenAsync(token, tokenSource.Token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 刷新登录Token
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> RefreshAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(RefreshToken))
            {
                throw new ArgumentNullException(nameof(RefreshToken));
            }

            return await RefreshAccessTokenAsync(RefreshToken, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<bool> LogoutAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(WebApiToken))
                {
                    return true;
                }

                /*
                var logoutParametersRepsonse = await SteamApi.GetLogoutParametersAsync(Website, SessionId, WebCookie, cancellationToken).ConfigureAwait(false);

                Logger?.LogDebug($"GetLogoutParameters HttpStatusCode: {logoutParametersRepsonse.HttpStatusCode}");

                var logoutParameters = logoutParametersRepsonse.Body;
                if (logoutParameters == null)
                {
                    return false;
                }

                var logoutTasks = Task.WhenAll(logoutParameters.Url.Select(url =>
                {
                    try
                    {
                        HttpContent content = new FormUrlEncodedContent(logoutParameters.Parameters);
                        var logoutResponse = SteamApi.PostAsync<string>(url, content, currentCookies: WebCookie).ContinueWith(task =>
                        {
                            var result = task.Result;

                            Logger?.LogDebug($"Logout Url:{url}, HttpStatusCode: {result.HttpStatusCode}");
                        });

                        return logoutResponse;
                    }
                    catch
                    {
                        return Task.CompletedTask;
                    }
                })).ConfigureAwait(false);

                await logoutTasks;
                */

                ulong.TryParse(SteamId ?? "", out var steamId);
                var refreshToken = await SteamAuthentication.RevokeRefreshTokenAsync(WebApiToken, steamId, tokenId: null, sharedSecret: null, cancellationToken).ConfigureAwait(false);

                Logger?.LogDebug($"RevokeRefreshToken HttpStatusCode: {refreshToken.HttpStatusCode}, ResultCode: {refreshToken.ResultCode}");
            }
            finally
            {
                ClearSession();

                await FinalizeLogoutAsync();
            }
            return true;
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language">语言类型</param>
        public void SetLanguage(Language language)
        {
            Language = language;
            WebCookie.SetLanguage(language);
        }

        /// <summary>
        /// 设置Logger
        /// </summary>
        /// <param name="logger"></param>
        public void SetLogger(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"SteamWebClient${Website}#");
            if (!LoggedIn)
            {
                stringBuilder.AppendLine("Anonymous");
                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine($"LoginAccount:{SteamId}");
            stringBuilder.AppendLine($"SessionId:{SessionId}");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            StopTimer();
            timer.Dispose();
        }

        /// <summary>
        /// 验证AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task<(bool Success, CookieCollection Cookies)> VerifyAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// 完成登录
        /// </summary>
        /// <returns></returns>
        protected virtual Task FinalizeLoginAsync()
        {
            WebApiToken = AccessToken?.Split("||")?.LastOrDefault();

            WebCookie.SetWebTradeEligibility();
            WebCookie.SetLanguage(Language);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 完成注销
        /// </summary>
        /// <returns></returns>
        protected virtual Task FinalizeLogoutAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 刷新登录Token
        /// </summary>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        private async Task<bool> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var steamToken = refreshToken.GetSteamToken();
            if (steamToken.HasMobileToken())
            {
                ulong.TryParse(steamToken.sub, out var steamId);
                return await MobileLoginAsync(steamId, refreshToken, cancellationToken);
            }

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var cookies = new CookieCollection
                {
                    new Cookie(Extensions.SteamSessionidCookieName,CreateSessionId()),
                    new Cookie(Extensions.SteamRefreshTokenCookeName, refreshToken)
                };
                var refreshTokenResult = await SteamApi.AjaxRefreshTokenAsync(Website, cookies, tokenSource.Token).ConfigureAwait(false);

                Logger?.LogDebug($"RefreshToken HttpStatusCode: {refreshTokenResult.HttpStatusCode}, Success: {refreshTokenResult.Body?.Success ?? false}, Error: {refreshTokenResult.Body?.Error}");

                if (refreshTokenResult.Body == null || !refreshTokenResult.Body.Success)
                {
                    return false;
                }

                Uri uri = new Uri(refreshTokenResult.Body.LoginUrl!);
                var saveToken = await SteamApi.SaveTokenAsync(uri, refreshTokenResult.Body.SteamId!, refreshTokenResult.Body.Auth!, refreshTokenResult.Body.Nonce!, cookies, cancellationToken: tokenSource.Token).ConfigureAwait(false);

                Logger?.LogDebug($"SaveToken HttpStatusCode: {saveToken.HttpStatusCode}, ResultCode: {saveToken.Body?.Result}");

                if (saveToken.Body == null || saveToken.Body.Result != ErrorCodes.OK)
                {
                    return false;
                }
                cookies.Add(saveToken.Cookies);

                WebCookie.Add(cookies);

                SteamId = refreshTokenResult.Body.SteamId!;
                AccessToken = cookies.First(c => Extensions.SteamAccessTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase)).Value;
                RefreshToken = cookies.FirstOrDefault(c => Extensions.SteamRefreshTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase))?.Value;
                SessionId = cookies.GetSessionId();

                await FinalizeLoginAsync().ConfigureAwait(false);
                return true;
            }
        }

        /// <summary>
        /// 检查登录Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<bool> CheckAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            var verifyAccessToken = await VerifyAccessTokenAsync(accessToken, cancellationToken).ConfigureAwait(false);

            Logger?.LogDebug($"VerifyAccessToken Success: {verifyAccessToken.Success}");

            if (!verifyAccessToken.Success)
            {
                return false;
            }

            var cookies = verifyAccessToken.Cookies;

            WebCookie.Add(cookies);

            SteamId = accessToken.Split("||").First();
            AccessToken = cookies.First(c => Extensions.SteamAccessTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase)).Value;
            RefreshToken = cookies.FirstOrDefault(c => Extensions.SteamRefreshTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase))?.Value;
            SessionId = cookies.GetSessionId();

            await FinalizeLoginAsync().ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// App端登录
        /// </summary>
        /// <param name="steamId"></param>
        /// <param name="refreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<bool> MobileLoginAsync(ulong steamId, string refreshToken, CancellationToken cancellationToken = default)
        {
            var appAccessToken = await SteamAuthentication.GenerateAppAccessTokenAsync(steamId, refreshToken.Split("||").Last(), true, cancellationToken);
            var appAccessTokenResponse = appAccessToken.Body;

            Logger?.LogDebug($"GenerateAppAccessToken HttpStatusCode: {appAccessToken.HttpStatusCode}, Success: {!string.IsNullOrWhiteSpace(appAccessTokenResponse?.access_token)}");

            if (string.IsNullOrWhiteSpace(appAccessTokenResponse?.access_token))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(appAccessTokenResponse.refresh_token))
            {
                refreshToken = $"{steamId}||{appAccessTokenResponse.refresh_token}";
            }

            var cookies = new CookieCollection
            {
                new Cookie(Extensions.SteamSessionidCookieName, CreateSessionId()),
                new Cookie(Extensions.SteamRefreshTokenCookeName, refreshToken),
                new Cookie(Extensions.SteamAccessTokenCookeName, $"{steamId}||{appAccessTokenResponse.access_token}")
            };

            WebCookie.Add(cookies);

            SteamId = $"{steamId}";
            AccessToken = cookies.First(c => Extensions.SteamAccessTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase)).Value;
            RefreshToken = cookies.FirstOrDefault(c => Extensions.SteamRefreshTokenCookeName.Equals(c.Name, StringComparison.CurrentCultureIgnoreCase))?.Value;
            SessionId = cookies.GetSessionId();

            await FinalizeLoginAsync().ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// 清空会话
        /// </summary>
        private void ClearSession()
        {
            SteamId = null;
            SessionId = null;
            AccessToken = null;
            RefreshToken = null;
            WebApiToken = null;

            WebCookie.Clear();
        }

        [MemberNotNull(new[] { nameof(timer) })]
        private void InitTimer()
        {
            timer = new Timer(async (_) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(RefreshToken))
                    {
                        return;
                    }

                    if (!await RefreshAccessTokenAsync(RefreshToken, default).ConfigureAwait(false))
                    {
                        return;
                    }
                }
                catch
                {

                }
            }, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        private async Task RestartTimer()
        {
            ulong timestamp;
            try
            {
                timestamp = await Extensions.GetSteamTimestampAsync().ConfigureAwait(false);
            }
            catch
            {
                timestamp = Extensions.GetSystemTimestamp();
            }

            var token = AccessToken!.GetSteamToken();
            var exp = TimeSpan.FromSeconds(token.exp) - TimeSpan.FromSeconds(timestamp);
            var due = exp > TimeSpan.FromSeconds(600) ? exp - TimeSpan.FromSeconds(600) : TimeSpan.Zero;
            timer!.Change(due, TimeSpan.FromSeconds(300));
        }

        private void StopTimer()
        {
            timer!.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// 创建SessionId
        /// </summary>
        /// <returns></returns>
        protected string CreateSessionId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <exception cref="NotLoginException"></exception>
        [MemberNotNull(new[] { nameof(SteamId), nameof(SessionId), nameof(AccessToken) })]
        protected void CheckLogon()
        {
            if (!LoggedIn)
            {
                throw new NotLoginException("用户未登录");
            }
        }

        /// <summary>
        /// SteamId
        /// </summary>
        public string? SteamId { get; private set; }

        /// <summary>
        /// SessionId
        /// </summary>
        public string? SessionId { get; private set; }

        /// <summary>
        /// 登录Token
        /// </summary>
        public string? AccessToken { get; private set; }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public string? RefreshToken
        {
            get { return refreshToken; }
            private set
            {
                refreshToken = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    StopTimer();
                    return;
                }

                RestartTimer().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// WebApiToken
        /// </summary>
        public string? WebApiToken { get; private set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        public Language Language { get; private set; } = Language.None;

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger? Logger { get; private set; } = new DefaultLogger();

        /// <summary>
        /// 站点
        /// </summary>
        protected abstract Website Website { get; }

        /// <summary>
        /// 是否已登录的
        /// </summary>
        [MemberNotNullWhen(true, new[] { nameof(SteamId), nameof(SessionId), nameof(AccessToken) })]
        public virtual bool LoggedIn => !(string.IsNullOrWhiteSpace(SteamId) || string.IsNullOrWhiteSpace(SessionId) || string.IsNullOrWhiteSpace(AccessToken));

        /// <summary>
        /// Cookie
        /// </summary>
        public readonly CookieCollection WebCookie = new CookieCollection();

    }
}
