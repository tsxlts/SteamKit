using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtendedHeader : IHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public ExtendedHeader()
        {
            Msg = EMsg.Invalid;
            HeaderSize = 36;
            HeaderVersion = 2;
            TargetJobID = ulong.MaxValue;
            SourceJobID = ulong.MaxValue;
            HeaderCanary = 239;
            SteamID = 0;
            SessionID = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public EMsg Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte HeaderSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ushort HeaderVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong TargetJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong SourceJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte HeaderCanary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong SteamID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SessionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);
            bw.Write((int)Msg);
            bw.Write(HeaderSize);
            bw.Write(HeaderVersion);
            bw.Write(TargetJobID);
            bw.Write(SourceJobID);
            bw.Write(HeaderCanary);
            bw.Write(SteamID);
            bw.Write(SessionID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            Msg = (EMsg)br.ReadInt32();
            HeaderSize = br.ReadByte();
            HeaderVersion = br.ReadUInt16();
            TargetJobID = br.ReadUInt64();
            SourceJobID = br.ReadUInt64();
            HeaderCanary = br.ReadByte();
            SteamID = br.ReadUInt64();
            SessionID = br.ReadInt32();
        }
    }
}
