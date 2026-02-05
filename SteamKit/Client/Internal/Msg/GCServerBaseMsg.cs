
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    public abstract class GCServerBaseMsg<THeader> : IGCServerMsg where THeader : ISteamSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="appId"></param>
        /// <param name="data"></param>
        public GCServerBaseMsg(uint msgType, uint appId, byte[] data)
        {
            AppId = appId;
            MsgType = msgType;
            Data = data;
            Header = new THeader();
            using (MemoryStream ms = new MemoryStream(Data))
            {
                Header.Deserialize(ms);
                BodyOffset = ms.Position;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsProto();

        /// <summary>
        /// 
        /// </summary>
        public uint AppId { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint MsgType { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract ulong JobID { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return Data;
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal THeader Header;

        /// <summary>
        /// 
        /// </summary>
        protected internal long BodyOffset;

        /// <summary>
        /// 
        /// </summary>
        protected internal byte[] Data;
    }
}
