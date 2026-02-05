
using SteamKit.Client.Internal;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class LogonParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public LogonParameter()
        {
            AccountInstance = Model.SteamId.DesktopInstance;
            AccountId = 0;

            OSType = SteamHelpers.GetOSType();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 手机令牌码
        /// </summary>
        public string? TwoFactorCode { get; set; }

        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public string? AuthCode { get; set; }

        /// <summary>
        /// SteamId
        /// </summary>
        public ulong SteamId { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// QosLevel
        /// </summary>
        public uint QosLevel { get; set; }

        /// <summary>
        /// 获取或设置帐户实例；1代表PC实例，2代表Console（PS3）实例
        /// </summary>
        /// <seealso cref="SteamId.DesktopInstance"/>
        /// <seealso cref="SteamId.ConsoleInstance"/>
        public uint AccountInstance { get; set; }

        /// <summary>
        /// 获取或设置使用Console实例时用于连接客户端的帐户Id
        /// </summary>
        /// <seealso cref="LogonParameter.AccountInstance"/>
        public uint AccountId { get; set; }

        /// <summary>
        /// OSType
        /// </summary>
        public EOSType OSType { get; set; }

        /// <summary>
        /// GamingDeviceType
        /// </summary>
        /// <value>The gaming device type.</value>
        public EGamingDeviceType GamingDeviceType { get; set; } = EGamingDeviceType.Unknown;

        /// <summary>
        /// 登录Steam的聊天模式
        /// </summary>
        public ChatMode ChatMode { get; set; } = ChatMode.Default;

        /// <summary>
        /// UIMode
        /// </summary>
        /// <value>The ui mode.</value>
        public EUIMode UIMode { get; set; } = EUIMode.Unknown;
    }

    /// <summary>
    /// 登录Steam的聊天模式
    /// </summary>
    public enum ChatMode : uint
    {
        /// <summary>
        /// 默认聊天模式
        /// </summary>
        Default = 0,

        /// <summary>
        /// 新Steam群聊的聊天模式
        /// </summary>
        NewSteamChat = 2,
    }
}
