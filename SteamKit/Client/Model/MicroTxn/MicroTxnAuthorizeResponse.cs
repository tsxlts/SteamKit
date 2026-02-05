namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MicroTxnAuthorizeResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public uint Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; } = [];
    }
}
