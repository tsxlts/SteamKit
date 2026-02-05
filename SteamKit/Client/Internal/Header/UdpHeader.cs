using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Header
{
    internal class UdpHeader : ISteamSerializable
    {
        public static readonly uint MAGIC = 0x31305356;
        // Static size: 4
        public uint Magic { get; set; }
        // Static size: 2
        public ushort PayloadSize { get; set; }
        // Static size: 1
        public EUdpPacketType PacketType { get; set; }
        // Static size: 1
        public byte Flags { get; set; }
        // Static size: 4
        public uint SourceConnID { get; set; }
        // Static size: 4
        public uint DestConnID { get; set; }
        // Static size: 4
        public uint SeqThis { get; set; }
        // Static size: 4
        public uint SeqAck { get; set; }
        // Static size: 4
        public uint PacketsInMsg { get; set; }
        // Static size: 4
        public uint MsgStartSeq { get; set; }
        // Static size: 4
        public uint MsgSize { get; set; }

        public UdpHeader()
        {
            Magic = UdpHeader.MAGIC;
            PayloadSize = 0;
            PacketType = EUdpPacketType.Invalid;
            Flags = 0;
            SourceConnID = 512;
            DestConnID = 0;
            SeqThis = 0;
            SeqAck = 0;
            PacketsInMsg = 0;
            MsgStartSeq = 0;
            MsgSize = 0;
        }

        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write(Magic);
            bw.Write(PayloadSize);
            bw.Write((byte)PacketType);
            bw.Write(Flags);
            bw.Write(SourceConnID);
            bw.Write(DestConnID);
            bw.Write(SeqThis);
            bw.Write(SeqAck);
            bw.Write(PacketsInMsg);
            bw.Write(MsgStartSeq);
            bw.Write(MsgSize);

        }

        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            Magic = br.ReadUInt32();
            PayloadSize = br.ReadUInt16();
            PacketType = (EUdpPacketType)br.ReadByte();
            Flags = br.ReadByte();
            SourceConnID = br.ReadUInt32();
            DestConnID = br.ReadUInt32();
            SeqThis = br.ReadUInt32();
            SeqAck = br.ReadUInt32();
            PacketsInMsg = br.ReadUInt32();
            MsgStartSeq = br.ReadUInt32();
            MsgSize = br.ReadUInt32();
        }
    }

    public enum EUdpPacketType : byte
    {
        Invalid = 0,
        ChallengeReq = 1,
        Challenge = 2,
        Connect = 3,
        Accept = 4,
        Disconnect = 5,
        Data = 6,
        Datagram = 7,
        Max = 8,
    }
}
