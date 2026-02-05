using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class ServerMsg<TBody> : ServerExtendedMsg where TBody : ISteamSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public ServerMsg(IServerMsg msg) : base(msg.MsgType, msg.GetData())
        {
            using MemoryStream ms = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            {
                Body = new TBody();
                Body.Deserialize(ms);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TBody Body { get; }
    }
}
