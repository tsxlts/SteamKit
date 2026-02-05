
using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Server;
using SteamKit.Client.Model.Proto;
using SteamKit.Exceptions;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 登录认证
    /// </summary>
    public abstract class BeginAuthResult
    {

        internal readonly BaseClient Client;

        internal BeginAuthResult(BaseClient baseClient)
        {
            Client = baseClient;
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong ClientId { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan PollingInterval { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RequestId { get; internal set; } = new byte[0];

        /// <summary>
        /// 
        /// </summary>
        public List<CAuthentication_AllowedConfirmation> AllowedConfirmations { get; internal set; } = new List<CAuthentication_AllowedConfirmation>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<JobResult<CAuthentication_PollAuthSessionStatus_Response>> PollAuthSessionStatusAsync(CancellationToken cancellationToken = default)
        {
            var result = await Client.ServiceMethodCallAsync((IAuthentication api) => api.PollAuthSessionStatus(new CAuthentication_PollAuthSessionStatus_Request
            {
                client_id = ClientId,
                request_id = RequestId,
            }), cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result.Result?.new_client_id > 0)
            {
                ClientId = result.Result.new_client_id;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<AuthTokenResult> PollingAuthTokenAsync(CancellationToken cancellationToken = default)
        {
            var authSessionStatusResponse = await PollAuthSessionStatusAsync(cancellationToken).ConfigureAwait(false);
            if (authSessionStatusResponse.EResult != EResult.OK)
            {
                throw new AuthException(authSessionStatusResponse.ErrorMessage ?? "授权失败")
                {
                    EResult = authSessionStatusResponse.EResult,
                    Error = authSessionStatusResponse.ErrorMessage ?? "授权失败"
                };
            }

            var authSessionStatusResult = authSessionStatusResponse.Result;
            return new AuthTokenResult
            {
                EResult = EResult.OK,
                AccessToken = authSessionStatusResult?.access_token,
                RefreshToken = authSessionStatusResult?.refresh_token,
                AccountName = authSessionStatusResult?.account_name,
                NewGuardData = authSessionStatusResult?.new_guard_data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task WaitPollingAsync(CancellationToken cancellationToken = default)
        {
            await Task.Delay(PollingInterval, cancellationToken).ConfigureAwait(false);
        }
    }
}
