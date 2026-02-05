using SteamKit.Client.Internal.Header;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    public abstract class ClientBaseMsg<THeader> : IClientMsg where THeader : IHeader, new()
    {
        /// <summary>
        /// Gets a value indicating whether this client message is protobuf backed.
        /// Client messages of this type are always protobuf backed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is protobuf backed; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsProto();

        /// <summary>
        /// Gets the network message type of this client message.
        /// </summary>
        /// <value>
        /// The network message type.
        /// </value>
        public EMsg MsgType => Header.Msg;

        /// <summary>
        /// Gets or sets the session id for this client message.
        /// </summary>
        /// <value>
        /// The session id.
        /// </value>
        public abstract int SessionID { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SteamID"/> for this client message.
        /// </summary>
        /// <value>
        /// The <see cref="SteamID"/>.
        /// </value>
        public abstract ulong? SteamID { get; set; }

        /// <summary>
        /// Gets or sets the target job id for this client message.
        /// </summary>
        /// <value>
        /// The target job id.
        /// </value>
        public abstract ulong TargetJobID { get; set; }

        /// <summary>
        /// Gets or sets the source job id for this client message.
        /// </summary>
        /// <value>
        /// The source job id.
        /// </value>
        public abstract ulong SourceJobID { get; set; }

        /// <summary>
        /// Gets the header for this message type. 
        /// </summary>
        internal THeader Header;

        /// <summary>
        /// Returns a <see cref="MemoryStream"/> which is the backing stream for client message payload data.
        /// </summary>
        public MemoryStream Payload { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMsgProtobuf&lt;BodyType&gt;"/> class.
        /// This is a client send constructor.
        /// </summary>
        /// <param name="eMsg">The network message type this client message represents.</param>
        /// <param name="payloadReserve">The number of bytes to initialize the payload capacity to.</param>
        public ClientBaseMsg(EMsg eMsg, int payloadReserve = 64)
        {
            Header = new THeader();
            Payload = new MemoryStream(payloadReserve);

            Header.Msg = eMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Serialize();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public abstract void Deserialize(byte[] data);
    }
}
