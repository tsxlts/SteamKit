using ProtoBuf;
using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class GCClientProtoBufMsg<TBody> : GCClientBaseMsg<GCProtoBufHeader> where TBody : IExtensible, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="payloadReserve"></param>
        public GCClientProtoBufMsg(uint msgType, int payloadReserve = 0) : base(msgType, payloadReserve)
        {
            Body = new TBody();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => true;

        /// <summary>
        /// 
        /// </summary>
        public override ulong TargetJobID
        {
            get => Header.Proto.job_id_target;
            set => Header.Proto.job_id_target = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public override ulong SourceJobID
        {
            get => Header.Proto.job_id_source;
            set => Header.Proto.job_id_source = value;
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
