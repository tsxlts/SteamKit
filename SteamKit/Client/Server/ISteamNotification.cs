
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Server
{
    /// <summary>
    /// ISteamNotification
    /// </summary>
    public interface ISteamNotification
    {
        /// <summary>
        /// 获取Steam通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CSteamNotification_GetSteamNotifications_Response GetSteamNotifications(CSteamNotification_GetSteamNotifications_Request request);
    }
}
