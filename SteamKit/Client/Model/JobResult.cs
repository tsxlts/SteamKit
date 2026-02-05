
namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JobResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public ulong JobId { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public EMsg MsgType { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public EResult EResult { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ErrorMessage { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public T? Result { get; internal set; }
    }
}
