using SteamKit.Client.Internal.Header;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 服务端消息
    /// </summary>
    public class ServerProtoBufMsg : ServerBaseMsg<ProtoBufHeader>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eMsg"></param>
        /// <param name="data"></param>
        public ServerProtoBufMsg(EMsg eMsg, byte[] data) : base(eMsg, data)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => true;

        /// <summary>
        /// 
        /// </summary>
        public override ulong JobID => Header.Proto.jobid_target;

        /// <summary>
        /// 
        /// </summary>
        public override ulong SourceJobID => Header.Proto.jobid_source;
    }
}
