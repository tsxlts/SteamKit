namespace SteamKit.Client.Model
{
    /// <summary>
    /// Represents a unified interface into client messages.
    /// </summary>
    public interface IClientMsg
    {
        /// <summary>
        /// Gets a value indicating whether this client message is protobuf backed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is protobuf backed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProto();

        /// <summary>
        /// Gets the network message type of this client message.
        /// </summary>
        /// <value>
        /// The message type.
        /// </value>
        public EMsg MsgType { get; }

        /// <summary>
        /// Gets or sets the session id for this client message.
        /// </summary>
        /// <value>
        /// The session id.
        /// </value>
        public int SessionID { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SteamID"/> for this client message.
        /// </summary>
        /// <value>
        /// The <see cref="SteamID"/>.
        /// </value>
        public ulong? SteamID { get; set; }

        /// <summary>
        /// Gets or sets the target job id for this client message.
        /// </summary>
        /// <value>
        /// The target job id.
        /// </value>
        public ulong TargetJobID { get; set; }

        /// <summary>
        /// Gets or sets the source job id for this client message.
        /// </summary>
        /// <value>
        /// The source job id.
        /// </value>
        public ulong SourceJobID { get; set; }

        /// <summary>
        /// Serializes this client message instance to a byte array.
        /// </summary>
        /// <returns>Data representing a client message.</returns>
        public byte[] Serialize();
    }
}
