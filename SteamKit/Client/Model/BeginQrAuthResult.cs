
using SteamKit.Client.Internal;
using SteamKit.Client.Model.Proto;
using SteamKit.Exceptions;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 二维码登录身份认证
    /// </summary>
    public class BeginQrAuthResult : BeginAuthResult
    {
        internal BeginQrAuthResult(BaseClient baseClient) : base(baseClient)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string ChallengeURL { get; internal set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        internal Action? ChallengeURLChanged { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal Action? DrawQRCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlChanged"></param>
        /// <returns></returns>
        public BeginQrAuthResult WithUrlChanged(Action urlChanged)
        {
            ChallengeURLChanged = urlChanged;

            return this;
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <returns></returns>
        public BeginQrAuthResult WithQrCode(Action drawQRCode)
        {
            DrawQRCode = drawQRCode;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<JobResult<CAuthentication_PollAuthSessionStatus_Response>> PollAuthSessionStatusAsync(CancellationToken cancellationToken = default)
        {
            var response = await base.PollAuthSessionStatusAsync(cancellationToken);
            if (response.EResult == EResult.OK && !string.IsNullOrWhiteSpace(response.Result?.new_challenge_url))
            {
                ChallengeURL = response.Result.new_challenge_url;
                ChallengeURLChanged?.Invoke();
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<AuthTokenResult> PollingAuthTokenAsync(CancellationToken cancellationToken = default)
        {
            AuthTokenResult authTokenResult;
            do
            {
                authTokenResult = await base.PollingAuthTokenAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(authTokenResult.AccountName) && !string.IsNullOrWhiteSpace(authTokenResult.RefreshToken))
                {
                    return authTokenResult;
                }

                if (authTokenResult.EResult != EResult.OK)
                {
                    throw new AuthException("登录失败")
                    {
                        EResult = authTokenResult?.EResult ?? EResult.Fail,
                        Error = "登录失败"
                    };
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    return await Task.FromCanceled<AuthTokenResult>(cancellationToken).ConfigureAwait(false);
                }

                await WaitPollingAsync(cancellationToken).ConfigureAwait(false);
            }
            while (true);
        }
    }
}
