
using static SteamKit.SteamEnum;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticatorStatusResult
    {
        /// <summary>
        /// 
        /// </summary>
        public uint State { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public SteamGuardScheme GuardScheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TokenGID { get; set; } = string.Empty;

        /// <summary>
        /// Version
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint InactivationReason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint AuthenticatorType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AuthenticatorAllowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EmailValidated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TimeCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TimeTransferred { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint RevocationAttemptsRemaining { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassifiedAgent { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ulong LastSeenAuthTokenId { get; set; }
    }
}
