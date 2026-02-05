
namespace SteamKit.Client.Model.GC
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGCClientMsg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsProto();

        /// <summary>
        /// 
        /// </summary>
        public uint MsgType { get; }

        /// <summary>
        /// 获取此数据包消息的目标任务Id
        /// </summary>
        public ulong TargetJobID { get; set; }

        /// <summary>
        /// 获取此数据包消息的源任务Id
        /// </summary>
        public ulong SourceJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Deserialize(byte[] data);
    }
}
