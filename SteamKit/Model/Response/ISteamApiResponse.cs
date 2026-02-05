
namespace SteamKit.Model
{
    /// <summary>
    /// SteamApiResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISteamApiResponse<T> : IWebResponse<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ErrorCodes ResultCode { get; }
    }
}
