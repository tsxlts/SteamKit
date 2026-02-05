using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    public class GCHeader : ISteamSerializable
    {
        public ushort HeaderVersion { get; set; }
        public ulong TargetJobID { get; set; }
        public ulong SourceJobID { get; set; }

        public GCHeader()
        {
            HeaderVersion = 1;
            TargetJobID = ulong.MaxValue;
            SourceJobID = ulong.MaxValue;
        }

        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);
            bw.Write(HeaderVersion);
            bw.Write(TargetJobID);
            bw.Write(SourceJobID);
        }

        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            HeaderVersion = br.ReadUInt16();
            TargetJobID = br.ReadUInt64();
            SourceJobID = br.ReadUInt64();
        }
    }
}
