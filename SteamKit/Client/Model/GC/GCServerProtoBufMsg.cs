using ProtoBuf;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 服务端GC消息
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class GCServerProtoBufMsg<TBody> : GCServerProtoBufMsg where TBody : IExtensible, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gcMsg"></param>
        public GCServerProtoBufMsg(IGCServerMsg gcMsg) : base(gcMsg.MsgType, gcMsg.AppId, gcMsg.GetData())
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
