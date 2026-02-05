using SteamKit.Client.Model.Cloud;

namespace SteamKit.Client.Server
{
    /// <summary>
    /// 云服务
    /// </summary>
    public interface ICloud
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CCloud_AppLaunchIntent_Response SignalAppLaunchIntent(CCloud_AppLaunchIntent_Request request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CCloud_GetAppFileChangelist_Response GetAppFileChangelist(CCloud_GetAppFileChangelist_Request request);
    }
}
