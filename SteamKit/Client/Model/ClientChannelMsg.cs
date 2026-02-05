using SteamKit.Client.Internal.Header;

namespace SteamKit.Client.Model
{
    public class ClientChannelMsg<TBody> : IClientMsg where TBody : ISteamSerializable, new()
    {
        readonly TBody _body;
        readonly BinaryReader reader;
        readonly BinaryWriter writer;


        /// <summary>
        /// Initializes a new instance of the <see cref="MsgBase"/> class.
        /// </summary>
        /// <param name="payloadReserve">The number of bytes to initialize the payload capacity to.</param>
        public ClientChannelMsg(EMsg msgType, int payloadReserve = 0)
        {
            _body = new TBody();

            Header = new ChannelHeader();

            Payload = new MemoryStream(payloadReserve);
            Header.Msg = msgType;

            reader = new BinaryReader(Payload);
            writer = new BinaryWriter(Payload);
        }

        public void Write(byte[] data)
        {
            writer.Write(data);
        }

        public void Write(uint data)
        {
            writer.Write(data);
        }

        public bool IsProto() => false;

        public ChannelHeader Header { get; }

        public EMsg MsgType => Header.Msg;

        /// <summary>
        /// Gets or sets the session id for this client message.
        /// </summary>
        /// <value>
        /// The session id.
        /// </value>
        public int SessionID
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the <see cref="SteamID"/> for this client message.
        /// </summary>
        /// <value>
        /// The <see cref="SteamID"/>.
        /// </value>
        public ulong? SteamID
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the target job id for this client message.
        /// </summary>
        /// <value>
        /// The target job id.
        /// </value>
        public ulong TargetJobID
        {
            get => Header.TargetJobID;
            set => Header.TargetJobID = value;
        }

        /// <summary>
        /// Gets or sets the source job id for this client message.
        /// </summary>
        /// <value>
        /// The source job id.
        /// </value>
        public ulong SourceJobID
        {
            get => Header.SourceJobID;
            set => Header.SourceJobID = value;
        }

        /// <summary>
        /// Gets the body structure of this message.
        /// </summary>
        public TBody Body => _body;

        /// <summary>
        /// Returns a <see cref="MemoryStream"/> which is the backing stream for client message payload data.
        /// </summary>
        public MemoryStream Payload { get; }

        /// <summary>
        /// Serializes this client message instance to a byte array.
        /// </summary>
        /// <exception cref="NotSupportedException">This class is for reading Protobuf messages only. If you want to create a protobuf message, use <see cref="ClientMsgProtobuf&lt;T&gt;"/>.</exception>
        public byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Header.Serialize(ms);

                Body.Serialize(ms);

                Payload.WriteTo(ms);

                return ms.ToArray();
            }
        }
    }
}
