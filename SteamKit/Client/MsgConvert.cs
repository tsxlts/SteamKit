using ProtoBuf;
using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Msg;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client
{
    /// <summary>
    /// 消息转换
    /// </summary>
    public class MsgConvert
    {
        /// <summary>
        /// 反序列化服务端消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IServerMsg? DeserializeServerMsg(byte[] data)
        {
            if (data.Length < sizeof(uint))
            {
                return null;
            }

            uint rawEMsg = BitConverter.ToUInt32(data, 0);
            EMsg eMsg = MsgUtil.GetMsg(rawEMsg);

            switch (eMsg)
            {
                case EMsg.ChannelEncryptRequest:
                case EMsg.ChannelEncryptResponse:
                case EMsg.ChannelEncryptResult:
                    return new ServerChannelMsg(eMsg, data);
            }

            try
            {
                if (MsgUtil.IsProtoBuf(rawEMsg))
                {
                    return new ServerProtoBufMsg(eMsg, data);
                }
                else
                {
                    return new ServerExtendedMsg(eMsg, data);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化服务端GC消息
        /// </summary>
        /// <param name="msgGC"></param>
        /// <returns></returns>
        public static IGCServerMsg DeserializeGCServerMsg(CMsgGCClient msgGC)
        {
            if (MsgUtil.IsProtoBuf(msgGC.msgtype))
            {
                return new GCServerProtoBufMsg(MsgUtil.GetGCMsg(msgGC.msgtype), msgGC.appid, msgGC.payload);
            }

            return new GCServerMsg(MsgUtil.GetGCMsg(msgGC.msgtype), msgGC.appid, msgGC.payload);
        }

        /// <summary>
        /// 反序列化服务端消息体
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ServerProtoBufMsg<TBody> Deserialize<TBody>(IServerMsg msg) where TBody : IExtensible, new()
        {
            return new ServerProtoBufMsg<TBody>(msg);
        }

        /// <summary>
        /// 反序列化服务端GC消息体
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static GCServerProtoBufMsg<TBody> Deserialize<TBody>(IGCServerMsg msg) where TBody : IExtensible, new()
        {
            return new GCServerProtoBufMsg<TBody>(msg);
        }
    }
}
