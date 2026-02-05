using SteamKit.Client.Internal.Header;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerExtendedMsg : ServerBaseMsg<ExtendedHeader>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eMsg"></param>
        /// <param name="data"></param>
        public ServerExtendedMsg(EMsg eMsg, byte[] data) : base(eMsg, data)
        {
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
    }
}
