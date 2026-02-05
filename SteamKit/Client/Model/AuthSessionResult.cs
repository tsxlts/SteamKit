namespace SteamKit.Client.Model
{
    /// <summary>
    /// 身份认证返回数据
    /// </summary>
    public class AuthTokenResult
    {
        /// <summary>
        /// 
        /// </summary>
        public EResult EResult { get; internal set; } = EResult.Fail;

        /// <summary>
        /// 
        /// </summary>
        public string? AccountName { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? RefreshToken { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? AccessToken { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? NewGuardData { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Error { get; internal set; }
    }
}
