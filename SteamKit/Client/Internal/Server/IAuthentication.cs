using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Internal.Server
{
    internal interface IAuthentication
    {
        public CAuthentication_GetPasswordRSAPublicKey_Response GetPasswordRSAPublicKey(CAuthentication_GetPasswordRSAPublicKey_Request message);

        public CAuthentication_BeginAuthSessionViaCredentials_Response BeginAuthSessionViaCredentials(CAuthentication_BeginAuthSessionViaCredentials_Request message);

        public CAuthentication_BeginAuthSessionViaQR_Response BeginAuthSessionViaQR(CAuthentication_BeginAuthSessionViaQR_Request request);

        public CAuthentication_UpdateAuthSessionWithSteamGuardCode_Response UpdateAuthSessionWithSteamGuardCode(CAuthentication_UpdateAuthSessionWithSteamGuardCode_Request message);

        public CAuthentication_UpdateAuthSessionWithMobileConfirmation_Response UpdateAuthSessionWithMobileConfirmation(CAuthentication_UpdateAuthSessionWithMobileConfirmation_Request request);

        public CAuthentication_PollAuthSessionStatus_Response PollAuthSessionStatus(CAuthentication_PollAuthSessionStatus_Request message);

        public CAuthentication_GetAuthSessionsForAccount_Response GetAuthSessionsForAccount(CAuthentication_GetAuthSessionsForAccount_Request request);

        public CAuthentication_GetAuthSessionInfo_Response GetAuthSessionInfo(CAuthentication_GetAuthSessionInfo_Request request);

        public CAuthentication_AccessToken_GenerateForApp_Response GenerateAccessTokenForApp(CAuthentication_AccessToken_GenerateForApp_Request request);

        public CAuthentication_MigrateMobileSession_Response MigrateMobileSession(CAuthentication_MigrateMobileSession_Request request);

        public CAuthentication_RefreshToken_Enumerate_Response EnumerateTokens(CAuthentication_RefreshToken_Enumerate_Request request);

        public CAuthentication_Token_Revoke_Response RevokeToken(CAuthentication_Token_Revoke_Request request);

        public CAuthentication_RefreshToken_Revoke_Response RevokeRefreshToken(CAuthentication_RefreshToken_Revoke_Request request);
    }
}
