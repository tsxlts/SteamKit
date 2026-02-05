
namespace SteamKit
{
    /// <summary>
    /// 
    /// </summary>
    public class SteamArgumentException : ArgumentException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public SteamArgumentException(string? message, string? paramName) : base(message, paramName)
        {
        }
    }
}
