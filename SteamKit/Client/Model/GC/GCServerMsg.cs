using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class GCServerMsg<TBody> : GCServerMsg where TBody : ISteamSerializable, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gcMsg"></param>
        public GCServerMsg(IGCServerMsg gcMsg) : base(gcMsg.MsgType, gcMsg.AppId, gcMsg.GetData())
        {
            using MemoryStream ms = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            {
                Body = new TBody();
                Body.Deserialize(ms);
            }
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public TBody Body { get; }
    }
}
