using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Server
{
    /// <summary>
    /// 令牌
    /// </summary>
    public interface ITwoFactor
    {
        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_Time_Response QueryTime(CTwoFactor_Time_Request message);

        /// <summary>
        /// 查询令牌验证器状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_Status_Response QueryStatus(CTwoFactor_Status_Request message);

        /// <summary>
        /// 添加令牌验证器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_AddAuthenticator_Response AddAuthenticator(CTwoFactor_AddAuthenticator_Request message);

        /// <summary>
        /// 确认添加令牌验证器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_FinalizeAddAuthenticator_Response FinalizeAddAuthenticator(CTwoFactor_FinalizeAddAuthenticator_Request message);

        /// <summary>
        /// 移除令牌验证器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_RemoveAuthenticator_Response RemoveAuthenticator(CTwoFactor_RemoveAuthenticator_Request message);

        /// <summary>
        /// 移动令牌验证器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_RemoveAuthenticatorViaChallengeStart_Response RemoveAuthenticatorViaChallengeStart(CTwoFactor_RemoveAuthenticatorViaChallengeStart_Request message);

        /// <summary>
        /// 确认移动令牌验证器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Response RemoveAuthenticatorViaChallengeContinue(CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Request message);

        /// <summary>
        /// 更新令牌验证器版本
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_UpdateTokenVersion_Response UpdateTokenVersion(CTwoFactor_UpdateTokenVersion_Request message);

        /// <summary>
        /// 创建备用码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_CreateEmergencyCodes_Response CreateEmergencyCodes(CTwoFactor_CreateEmergencyCodes_Request message);

        /// <summary>
        /// 撤销备用码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_DestroyEmergencyCodes_Response DestroyEmergencyCodes(CTwoFactor_DestroyEmergencyCodes_Request message);

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public CTwoFactor_ValidateToken_Response ValidateToken(CTwoFactor_ValidateToken_Request message);
    }
}
