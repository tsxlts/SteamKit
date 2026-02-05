using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHeader : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public EMsg Msg { get; set; }
    }
}
