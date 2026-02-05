using SteamKit.Client.Model;

namespace SteamKit.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthException() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AuthException(string message) : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public EResult EResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Error { get; set; }
    }
}
