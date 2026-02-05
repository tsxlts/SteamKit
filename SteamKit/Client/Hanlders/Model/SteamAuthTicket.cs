using System.IO.Hashing;

namespace SteamKit.Client.Hanlders
{
    public partial class SteamAuthTicketHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public class TicketInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public uint AppId { get; }

            /// <summary>
            /// 
            /// </summary>
            public byte[] Ticket { get; }

            /// <summary>
            /// 
            /// </summary>
            public uint TicketCRC { get; }

            internal TicketInfo(uint appId, byte[] ticket)
            {
                AppId = appId;
                Ticket = ticket;
                TicketCRC = Crc32.HashToUInt32(ticket);
            }
        }
    }
}
