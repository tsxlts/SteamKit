namespace SteamKit.Client.Model
{
    /// <summary>
    /// 移动令牌响应
    /// </summary>
    public class MoveAuthenticatorResult : AddAuthenticatorResult
    {
        /// <summary>
        /// SteamId
        /// </summary>
        public ulong SteamId { get; set; }
    }
}
