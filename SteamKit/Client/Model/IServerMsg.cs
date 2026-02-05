namespace SteamKit.Client.Model
{
    /// <summary>
    /// 服务端消息
    /// </summary>
    public interface IServerMsg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsProto();

        /// <summary>
        /// 消息类型
        /// </summary>
        public EMsg MsgType { get; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public abstract ulong JobID { get; }

        /// <summary>
        /// 源任务ID
        /// </summary>
        public ulong SourceJobID { get; }

        /// <summary>
        /// 获取消息体
        /// </summary>
        /// <returns></returns>
        public byte[] GetData();
    }
}
