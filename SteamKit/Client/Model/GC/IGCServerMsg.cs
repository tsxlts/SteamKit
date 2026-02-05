
namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 服务端GC消息
    /// </summary>
    public interface IGCServerMsg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsProto();

        /// <summary>
        /// 消息类型
        /// </summary>
        public uint MsgType { get; }

        /// <summary>
        /// 应用Id
        /// </summary>
        public uint AppId { get; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public ulong JobID { get; }

        /// <summary>
        /// 获取消息体
        /// </summary>
        /// <returns></returns>
        public byte[] GetData();
    }
}
