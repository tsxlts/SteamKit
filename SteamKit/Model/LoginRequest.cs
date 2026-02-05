
namespace SteamKit.Model
{
    /// <summary>
    /// 登录请求数据
    /// </summary>
    public class LoginRequest
    {
        private string twoFactorCode;

        private string steamId;
        private string emailAuth;

        private string captchagId;
        private string captchaText;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public LoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;

            twoFactorCode = string.Empty;
            steamId = string.Empty;
            emailAuth = string.Empty;

            captchagId = "-1";
            captchaText = string.Empty;

        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TwoFactorCode
        {
            get
            {
                return twoFactorCode;
            }
            set
            {
                steamId = string.Empty;
                emailAuth = string.Empty;
                captchagId = "-1";
                captchaText = string.Empty;

                twoFactorCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SteamId
        {
            get
            {
                return steamId;
            }
            set
            {
                twoFactorCode = string.Empty;
                captchagId = "-1";
                captchaText = string.Empty;

                steamId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAuth
        {
            get
            {
                return emailAuth;
            }
            set
            {
                twoFactorCode = string.Empty;
                captchagId = "-1";
                captchaText = string.Empty;

                emailAuth = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CaptchagId
        {
            get
            {
                return captchagId;
            }
            set
            {
                steamId = string.Empty;
                emailAuth = string.Empty;
                twoFactorCode = string.Empty;

                captchagId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CaptchaText
        {
            get
            {
                return captchaText;
            }
            set
            {
                steamId = string.Empty;
                emailAuth = string.Empty;
                twoFactorCode = string.Empty;

                captchaText = value;
            }
        }
    }
}
