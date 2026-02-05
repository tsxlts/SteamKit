using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    public class GCClientByteBufferMsg : GCClientBaseMsg<GCHeader>, IDisposable
    {
        private readonly MemoryStream memoryStream;
        private readonly BinaryWriter writer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="payloadReserve"></param>
        public GCClientByteBufferMsg(uint msgType, int payloadReserve = 0) : base(msgType, payloadReserve)
        {
            memoryStream = new MemoryStream(payloadReserve);
            writer = new BinaryWriter(memoryStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => false;

        /// <summary>
        /// Gets or sets the target job id for this client message.
        /// </summary>
        /// <value>
        /// The target job id.
        /// </value>
        public override ulong TargetJobID
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
        public override ulong SourceJobID
        {
            get => Header.SourceJobID;
            set => Header.SourceJobID = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public BinaryWriter Writer => writer;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Header.Serialize(ms);

                byte[] data = memoryStream.ToArray();
                ms.Write(data, 0, data.Length);

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

                int bodyOffset = (int)ms.Position;
                int bodyLen = (int)(ms.Length - ms.Position);
                writer.Write(data, bodyOffset, bodyLen);

                int payloadOffset = (int)ms.Position;
                int payloadLen = (int)(ms.Length - ms.Position);
                Payload.Write(data, payloadOffset, payloadLen);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            writer.Dispose();
            memoryStream.Dispose();
        }
    }
}
