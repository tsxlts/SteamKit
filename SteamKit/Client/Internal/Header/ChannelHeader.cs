using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelHeader : IHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelHeader()
        {
            Msg = EMsg.Invalid;
            TargetJobID = ulong.MaxValue;
            SourceJobID = ulong.MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write((int)Msg);
            bw.Write(TargetJobID);
            bw.Write(SourceJobID);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            Msg = (EMsg)br.ReadInt32();
            TargetJobID = br.ReadUInt64();
            SourceJobID = br.ReadUInt64();
        }

        /// <summary>
        /// 
        /// </summary>
        public EMsg Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong TargetJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong SourceJobID { get; set; }
    }
}
