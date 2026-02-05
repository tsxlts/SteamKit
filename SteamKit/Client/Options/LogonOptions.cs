using SteamKit.Client.Internal;
using SteamKit.Client.Model;

namespace SteamKit.Client.Options
{
    /// <summary>
    /// 登录选项
    /// </summary>
    public class LogonOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public static LogonOptions Default = new LogonOptions();

        private LogonOptions()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osType"></param>
        /// <returns></returns>
        public LogonOptions WithOSType(EOSType osType)
        {
            this.OSType = osType;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamingDeviceType"></param>
        /// <returns></returns>
        public LogonOptions WithGamingDeviceType(EGamingDeviceType gamingDeviceType)
        {
            this.GamingDeviceType = gamingDeviceType;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatMode"></param>
        /// <returns></returns>
        public LogonOptions WithChatMode(ChatMode chatMode)
        {
            this.ChatMode = chatMode;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiMode"></param>
        /// <returns></returns>
        public LogonOptions WithUIMode(EUIMode uiMode)
        {
            this.UIMode = uiMode;
            return this;
        }

        /// <summary>
        /// OSType
        /// </summary>
        public EOSType OSType { get; private set; } = SteamHelpers.GetOSType();

        /// <summary>
        /// GamingDeviceType
        /// </summary>
        /// <value>The gaming device type.</value>
        public EGamingDeviceType GamingDeviceType { get; private set; } = EGamingDeviceType.Unknown;

        /// <summary>
        /// 登录Steam的聊天模式
        /// </summary>
        public ChatMode ChatMode { get; private set; } = ChatMode.Default;

        /// <summary>
        /// UIMode
        /// </summary>
        /// <value>The ui mode.</value>
        public EUIMode UIMode { get; private set; } = EUIMode.Unknown;
    }
}
