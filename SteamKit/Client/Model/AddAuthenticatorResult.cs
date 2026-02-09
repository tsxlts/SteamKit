
using static SteamKit.Enums;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 添加令牌响应
    /// </summary>
    public class AddAuthenticatorResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

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
        public ulong SerialNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SteamGuardScheme GuardScheme { get; set; }

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
