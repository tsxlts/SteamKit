using SteamKit.Client.Internal.Header;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerByteBufferMsg : ServerBaseMsg<ExtendedHeader>, IDisposable
    {
        private readonly MemoryStream memoryStream;
        private readonly BinaryReader reader;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        public ServerByteBufferMsg(IServerMsg msg) : base(msg.MsgType, msg.GetData())
        {
            memoryStream = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            reader = new BinaryReader(memoryStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => false;

        /// <summary>
        /// 获取此数据包消息的目标任务Id
        /// </summary>
        public override ulong JobID => Header.TargetJobID;

        /// <summary>
        /// 获取此数据包消息的源任务Id
        /// </summary>
        public override ulong SourceJobID => Header.SourceJobID;

        /// <summary>
        /// 
        /// </summary>
        public BinaryReader Reader => reader;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            reader.Dispose();
            memoryStream.Dispose();
        }
    }
}
