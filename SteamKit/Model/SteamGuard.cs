
namespace SteamKit.Model
{
    /// <summary>
    /// Steam令牌秘钥信息
    /// </summary>
    public class SteamGuard
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; } = "";

        /// <summary>
        /// 共享秘钥
        /// </summary>
        public string SharedSecret { get; set; } = "";

        /// <summary>
        /// 身份秘钥
        /// </summary>
        public string IdentitySecret { get; set; } = "";

        /// <summary>
        /// 撤销码
        /// </summary>
        public string RevocationCode { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public long SerialNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GuardScheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string URI { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string TokenGID { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Secret1 { get; set; } = "";
    }
}
