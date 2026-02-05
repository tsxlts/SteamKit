

namespace SteamKit.Client.Internal.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DisconnectedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public DisconnectedEventArgs(DisconnectType type)
        {
            Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public DisconnectType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public enum DisconnectType
        {
            /// <summary>
            /// 
            /// </summary>
            Exception = 1,

            /// <summary>
            /// 
            /// </summary>
            UserInitiated = 2,

            /// <summary>
            /// 
            /// </summary>
            ConnectionError = 3,

            /// <summary>
            /// 
            /// </summary>
            ConnectionClosed = 4,

            /// <summary>
            /// 
            /// </summary>
            Dispose = 91,

            /// <summary>
            /// 
            /// </summary>
            Unknown = 99
        }
    }
}
