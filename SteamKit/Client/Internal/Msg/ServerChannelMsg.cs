using SteamKit.Client.Internal.Header;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerChannelMsg : ServerBaseMsg<ChannelHeader>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eMsg"></param>
        /// <param name="data"></param>
        public ServerChannelMsg(EMsg eMsg, byte[] data) : base(eMsg, data)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsProto() => false;

        /// <summary>
        /// 
        /// </summary>
        public override ulong JobID => Header.TargetJobID;

        /// <summary>
        /// 
        /// </summary>
        public override ulong SourceJobID => Header.SourceJobID;
    }
}
