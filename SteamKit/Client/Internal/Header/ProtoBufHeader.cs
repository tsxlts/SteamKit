using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    /// <summary>
    /// 
    /// </summary>
    public class ProtoBufHeader : IHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public EMsg Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int HeaderLength { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Client.Model.Proto.ProtoBufHeader Proto { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public ProtoBufHeader()
        {
            Msg = EMsg.Invalid;
            HeaderLength = 0;
            Proto = new Client.Model.Proto.ProtoBufHeader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (MemoryStream msProto = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(msProto, Proto);
                HeaderLength = (int)msProto.Length;

                BinaryWriter bw = new BinaryWriter(stream);
                bw.Write((int)MsgUtil.MakeMsg(Msg, true));
                bw.Write(HeaderLength);


                bw.Write(msProto.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            Msg = MsgUtil.GetMsg((uint)br.ReadInt32());
            HeaderLength = br.ReadInt32();
            using (MemoryStream msProto = new MemoryStream(br.ReadBytes(HeaderLength)))
            {
                Proto = ProtoBuf.Serializer.Deserialize<Client.Model.Proto.ProtoBufHeader>(msProto);
            }
        }
    }
}
