
namespace SteamKit.Model
{
    /// <summary>
    /// 响应数据
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Content
        /// </summary>
        public Stream Content { get; set; }
    }
}
