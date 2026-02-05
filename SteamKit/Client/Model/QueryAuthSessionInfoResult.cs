namespace SteamKit.Client.Model
{
    /// <summary>
    /// 查询登录授权Session信息返回
    /// </summary>
    public class QueryAuthSessionInfoResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Geoloc { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DeviceFriendlyName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public EAuthTokenPlatformType PlatformType { get; set; } = EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
    }
}
