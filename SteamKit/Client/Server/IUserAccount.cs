using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Server
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccount
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_GetAvailableValveDiscountPromotions_Response GetAvailableValveDiscountPromotions(CUserAccount_GetAvailableValveDiscountPromotions_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_GetWalletDetails_Response GetClientWalletDetails(CUserAccount_GetClientWalletDetails_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_GetAccountLinkStatus_Response GetAccountLinkStatus(CUserAccount_GetAccountLinkStatus_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_CancelLicenseForApp_Response CancelLicenseForApp(CUserAccount_CancelLicenseForApp_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_GetUserCountry_Response GetUserCountry(CUserAccount_GetUserCountry_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_CreateFriendInviteToken_Response CreateFriendInviteToken(CUserAccount_CreateFriendInviteToken_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_GetFriendInviteTokens_Response GetFriendInviteTokens(CUserAccount_GetFriendInviteTokens_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_ViewFriendInviteToken_Response ViewFriendInviteToken(CUserAccount_ViewFriendInviteToken_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_RedeemFriendInviteToken_Response RedeemFriendInviteToken(CUserAccount_RedeemFriendInviteToken_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_RevokeFriendInviteToken_Response RevokeFriendInviteToken(CUserAccount_RevokeFriendInviteToken_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CUserAccount_RegisterCompatTool_Response RegisterCompatTool(CUserAccount_RegisterCompatTool_Request request);
    }
}
