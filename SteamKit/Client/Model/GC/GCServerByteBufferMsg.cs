using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    public class GCServerByteBufferMsg : GCServerMsg, IDisposable
    {
        private readonly MemoryStream memoryStream;
        private readonly BinaryReader reader;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gcMsg"></param>
        public GCServerByteBufferMsg(IGCServerMsg gcMsg) : base(gcMsg.MsgType, gcMsg.AppId, gcMsg.GetData())
        {
            memoryStream = new MemoryStream(Data, (int)BodyOffset, Data.Length - (int)BodyOffset);
            reader = new BinaryReader(memoryStream);
        }

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
