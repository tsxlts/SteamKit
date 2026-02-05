
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    /// <summary>
    /// 
    /// </summary>
    public class GCProtoBufHeader : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public uint Msg { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int HeaderLength { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Client.Model.Proto.GCProtoBufHeader Proto { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public GCProtoBufHeader()
        {
            Msg = 0;
            HeaderLength = 0;
            Proto = new Client.Model.Proto.GCProtoBufHeader();
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

                bw.Write(MsgUtil.MakeGCMsg(Msg, true));
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
            Msg = MsgUtil.GetGCMsg(br.ReadUInt32());
            HeaderLength = br.ReadInt32();
            using (MemoryStream msProto = new MemoryStream(br.ReadBytes(HeaderLength)))
            {
                Proto = ProtoBuf.Serializer.Deserialize<Client.Model.Proto.GCProtoBufHeader>(msProto);
            }
        }
    }
}
