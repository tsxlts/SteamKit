
namespace SteamKit.Client.Model
{
    /// <summary>
    /// 登录游戏响应
    /// </summary>
    public class LoginGameResponse
    {
        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? Error { get; set; }
    }
}
