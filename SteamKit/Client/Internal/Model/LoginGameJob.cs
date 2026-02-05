using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Model
{
    internal class LoginGameJob
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginGameJob()
        {
            Lock = new AsyncLock();
        }

        /// <summary>
        /// 
        /// </summary>
        public AsyncLock Lock { get; }

        /// <summary>
        /// 
        /// </summary>
        public AsyncJob<LoginGameResponse>? Task { get; set; }
    }
}
