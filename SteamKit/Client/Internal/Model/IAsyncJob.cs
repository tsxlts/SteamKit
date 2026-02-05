
namespace SteamKit.Client.Internal.Model
{
    internal interface IAsyncJob
    {
        /// <summary>
        /// 
        /// </summary>
        public ulong JobId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool SetResult(object result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public bool SetException(Exception exception);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SetCancel();
    }
}
