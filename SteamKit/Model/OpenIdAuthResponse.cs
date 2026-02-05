
namespace SteamKit.Model
{
    /// <summary>
    /// Steam OpenId登录验证响应
    /// </summary>
    public class OpenIdAuthResponse
    {
        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        public string? SteamId { get; set; }
    }
}
