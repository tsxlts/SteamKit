using System.Text;
using SteamKit.Internal;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MsgClientServerUnavailable : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public MsgClientServerUnavailable()
        {
            JobidSent = 0;
            EMsgSent = 0;
            EServerTypeUnavailable = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EMsg GetEMsg() { return EMsg.ClientServerUnavailable; }

        /// <summary>
        /// 
        /// </summary>
        public ulong JobidSent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint EMsgSent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EServerType EServerTypeUnavailable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(JobidSent);
                bw.Write(EMsgSent);
                bw.Write((int)EServerTypeUnavailable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
            {
                JobidSent = br.ReadUInt64();
                EMsgSent = br.ReadUInt32();
                EServerTypeUnavailable = (EServerType)br.ReadInt32();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MsgClientVACBanStatus : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public MsgClientVACBanStatus()
        {
            NumBans = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EMsg GetEMsg() { return EMsg.ClientVACBanStatus; }

        /// <summary>
        /// 
        /// </summary>
        public uint NumBans { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<uint> BannedApps { get; } = new List<uint>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(NumBans);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
            {
                NumBans = br.ReadUInt32();

                for (int i = 0; i < NumBans; i++)
                {
                    BannedApps.Add(br.ReadUInt32());
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MsgClientMarketingMessageUpdate2 : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public MsgClientMarketingMessageUpdate2()
        {
            MarketingMessageUpdateTime = 0;
            Count = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EMsg GetEMsg() { return EMsg.ClientMarketingMessageUpdate2; }

        /// <summary>
        /// 
        /// </summary>
        public uint MarketingMessageUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Message> Messages { get; } = new List<Message>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);

            bw.Write(MarketingMessageUpdateTime);
            bw.Write(Count);

            foreach (Message message in Messages)
            {
                using MemoryStream messageStream = new MemoryStream();
                message.Serialize(messageStream);
                bw.Write((int)messageStream.Length + 4);
                bw.Write(messageStream.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using BinaryReader br = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);

            MarketingMessageUpdateTime = br.ReadUInt32();
            Count = br.ReadUInt32();

            for (int index = 0; index < Count; ++index)
            {
                int dataLen = br.ReadInt32() - 4;
                byte[] messageData = br.ReadBytes(dataLen);

                using MemoryStream messageStream = new MemoryStream(messageData);
                var message = new Message();
                message.Deserialize(messageStream);
                Messages.Add(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class Message : ISteamSerializable
        {
            /// <summary>
            /// 
            /// </summary>
            public Message()
            {
                ID = 0;
                URL = "";
                Flags = EMarketingMessageFlags.None;
            }

            /// <summary>
            /// 
            /// </summary>
            public ulong ID { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string URL { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public EMarketingMessageFlags Flags { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="stream"></param>
            public void Serialize(Stream stream)
            {
                using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);

                bw.Write(ID);
                bw.BaseStream.WriteNullTermString(URL, Encoding.UTF8);
                bw.Write((uint)Flags);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="stream"></param>
            public void Deserialize(Stream stream)
            {
                using BinaryReader br = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);

                ID = br.ReadUInt64();
                URL = br.BaseStream.ReadNullTermString(Encoding.UTF8);
                Flags = (EMarketingMessageFlags)br.ReadUInt32();
            }
        }
    }
}
