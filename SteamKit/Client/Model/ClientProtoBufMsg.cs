using ProtoBuf;
using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class ClientProtoBufMsg<TBody> : ClientBaseMsg<ProtoBufHeader> where TBody : IExtensible, new()
    {
        private TBody _body;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMsgProtobuf&lt;BodyType&gt;"/> class.
        /// This is a client send constructor.
        /// </summary>
        /// <param name="eMsg">The network message type this client message represents.</param>
        /// <param name="payloadReserve">The number of bytes to initialize the payload capacity to.</param>
        public ClientProtoBufMsg(EMsg eMsg, int payloadReserve = 64) : base(eMsg, payloadReserve)
        {
            _body = new TBody();
        }

        /// <summary>
        /// Gets the body structure of this message.
        /// </summary>
        public TBody Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        public override bool IsProto() => true;

        /// <summary>
        /// Gets or sets the session id for this client message.
        /// </summary>
        /// <value>
        /// The session id.
        /// </value>
        public override int SessionID
        {
            get => Header.Proto.client_sessionid;
            set => Header.Proto.client_sessionid = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="SteamID"/> for this client message.
        /// </summary>
        /// <value>
        /// The <see cref="SteamID"/>.
        /// </value>
        public override ulong? SteamID
        {
            get => Header.Proto.steamid;
            set => Header.Proto.steamid = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the target job id for this client message.
        /// </summary>
        /// <value>
        /// The target job id.
        /// </value>
        public override ulong TargetJobID
        {
            get => Header.Proto.jobid_target;
            set => Header.Proto.jobid_target = value;
        }

        /// <summary>
        /// Gets or sets the source job id for this client message.
        /// </summary>
        /// <value>
        /// The source job id.
        /// </value>
        public override ulong SourceJobID
        {
            get => Header.Proto.jobid_source;
            set => Header.Proto.jobid_source = value;
        }

        /// <summary>
        /// Serializes this client message instance to a byte array.
        /// </summary>
        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Header.Serialize(ms);

                Serializer.Serialize(ms, Body);

                Payload.WriteTo(ms);

                return ms.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void Deserialize(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (MemoryStream ms = new MemoryStream(data))
            {
                Header.Deserialize(ms);
                Body = Serializer.Deserialize<TBody>(ms);

                int payloadOffset = (int)ms.Position;
                int payloadLen = (int)(ms.Length - ms.Position);
                Payload.Write(data, payloadOffset, payloadLen);
            }
        }
    }
}
