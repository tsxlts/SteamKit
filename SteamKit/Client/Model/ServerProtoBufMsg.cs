using ProtoBuf;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 服务端消息
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class ServerProtoBufMsg<TBody> : ServerProtoBufMsg where TBody : IExtensible, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        public ServerProtoBufMsg(IServerMsg msg) : base(msg.MsgType, msg.GetData())
        {
            using MemoryStream ms = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            {
                Body = Serializer.Deserialize<TBody>(ms);
            }
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public TBody Body { get; }
    }
}
