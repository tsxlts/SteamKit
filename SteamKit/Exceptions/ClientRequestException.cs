namespace SteamKit.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientRequestException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ClientRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ClientRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
