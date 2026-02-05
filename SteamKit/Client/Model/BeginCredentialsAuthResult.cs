using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Server;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 帐号登录身份认证
    /// </summary>
    public class BeginCredentialsAuthResult : BeginAuthResult
    {
        internal BeginCredentialsAuthResult(BaseClient baseClient) : base(baseClient)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong SteamId { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? WeakToken { get; internal set; }

        /// <summary>
        /// 确认登录
        /// </summary>
        /// <param name="guardType"></param>
        /// <param name="guardCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EResult> ConfirmLoginAsync(EAuthSessionGuardType guardType, string? guardCode = null, CancellationToken cancellationToken = default)
        {
            switch (guardType)
            {
                case EAuthSessionGuardType.k_EAuthSessionGuardType_None:
                case EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceConfirmation:
                case EAuthSessionGuardType.k_EAuthSessionGuardType_EmailConfirmation:
                    return EResult.OK;

                case EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode:
                case EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode:
                    var updateAuthSessionResponse = await Client.ServiceMethodCallAsync((IAuthentication api) => api.UpdateAuthSessionWithSteamGuardCode(new CAuthentication_UpdateAuthSessionWithSteamGuardCode_Request
                    {
                        steamid = SteamId,
                        client_id = ClientId,

                        code_type = guardType,
                        code = guardCode ?? "",
                    }), cancellationToken: cancellationToken).ConfigureAwait(false);
                    return updateAuthSessionResponse?.EResult ?? EResult.OK;

                default:
                    throw new NotImplementedException($"验证方式不可用");
            }
        }
    }
}
