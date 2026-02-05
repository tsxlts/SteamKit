using SteamKit.Client.Internal.Header;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    public abstract class ServerBaseMsg<THeader> : IServerMsg where THeader : IHeader, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eMsg"></param>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ServerBaseMsg(EMsg eMsg, byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            MsgType = eMsg;
            Data = data;

            Header = new THeader();

            using (MemoryStream ms = new MemoryStream(data))
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
        public EMsg MsgType { get; }

        /// <summary>
        /// 获取此数据包消息的目标任务Id
        /// </summary>
        public abstract ulong JobID { get; }

        /// <summary>
        /// 获取此数据包消息的源任务Id
        /// </summary>
        public abstract ulong SourceJobID { get; }

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
        /// <returns></returns>
        public byte[] GetBody()
        {
            return Data[(int)BodyOffset..];
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
