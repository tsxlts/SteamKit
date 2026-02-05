
namespace SteamKit
{
    /// <summary>
    /// SteamTokenException
    /// </summary>
    public class SteamTokenException : SteamArgumentException
    {
        /// <summary>
        /// SteamTokenException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public SteamTokenException(string? message, string? paramName) : base(message, paramName)
        {
        }
    }
}
