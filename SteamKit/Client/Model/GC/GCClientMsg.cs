using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class GCClientMsg<TBody> : GCClientBaseMsg<GCHeader> where TBody : ISteamSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="payloadReserve"></param>
        public GCClientMsg(uint msgType, int payloadReserve = 0) : base(msgType, payloadReserve)
        {
            Body = new TBody();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => false;

        /// <summary>
        /// 
        /// </summary>
        public override ulong TargetJobID
        {
            get => Header.TargetJobID;
            set => Header.TargetJobID = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public override ulong SourceJobID
        {
            get => Header.SourceJobID;
            set => Header.SourceJobID = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public TBody Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Header.Serialize(ms);
                Body.Serialize(ms);
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
                Body.Deserialize(ms);

                int payloadOffset = (int)ms.Position;
                int payloadLen = (int)(ms.Length - ms.Position);
                Payload.Write(data, payloadOffset, payloadLen);
            }
        }
    }
}
