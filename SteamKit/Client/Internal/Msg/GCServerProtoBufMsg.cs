using SteamKit.Client.Internal.Header;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    public class GCServerProtoBufMsg : GCServerBaseMsg<GCProtoBufHeader>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emsg"></param>
        /// <param name="appId"></param>
        /// <param name="data"></param>
        public GCServerProtoBufMsg(uint emsg, uint appId, byte[] data) : base(emsg, appId, data)
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
        public override ulong JobID => Header.Proto.job_id_target;
    }
}
