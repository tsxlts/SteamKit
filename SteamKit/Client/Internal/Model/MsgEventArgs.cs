

using System.Net;

namespace SteamKit.Client.Internal.Model
{
    internal class MsgEventArgs
    {
        public byte[] Data { get; }
        public EndPoint EndPoint { get; }

        public MsgEventArgs(byte[] data, EndPoint endPoint)
        {
            Data = data;
            EndPoint = endPoint;
        }

        public MsgEventArgs WithData(byte[] data) => new MsgEventArgs(data, EndPoint);
    }
}
