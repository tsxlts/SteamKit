
namespace SteamKit.Client.Model
{
    /// <summary>
    /// ClientResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientResult<T>
    {
        /// <summary>
        /// Result
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// ErrorCode
        /// </summary>
        public EResult ErrorCode { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
