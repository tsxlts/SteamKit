
namespace SteamKit
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientBusyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ClientBusyException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ClientBusyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
