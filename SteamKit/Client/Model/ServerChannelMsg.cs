using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class ServerChannelMsg<TBody> : ServerChannelMsg where TBody : ISteamSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public ServerChannelMsg(IServerMsg msg) : base(msg.MsgType, msg.GetData())
        {
            using MemoryStream ms = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            {
                Body = new TBody();
                Body.Deserialize(ms);

                Payload = new MemoryStream();
                int payloadLen = (int)(ms.Length - ms.Position);
                if (payloadLen > 0)
                {
                    ms.CopyTo(Payload, payloadLen);
                    Payload.Seek(0, SeekOrigin.Begin);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TBody Body { get; }

        /// <summary>
        /// 
        /// </summary>
        public MemoryStream Payload { get; }
    }
}
