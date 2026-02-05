
namespace SteamKit.Model
{
    /// <summary>
    /// SteamToken
    /// </summary>
    public class SteamToken
    {
        /// <summary>
        /// 
        /// </summary>
        public string iss { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string sub { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<string> aud { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public ulong exp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong nbf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong iat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jti { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ulong oat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong rt_exp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int per { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ip_subject { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ip_confirmer { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRefreshToken()
        {
            return exp >= rt_exp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasMobileToken()
        {
            return aud.Any(c => c.Equals("mobile", StringComparison.OrdinalIgnoreCase));
        }
    }
}
