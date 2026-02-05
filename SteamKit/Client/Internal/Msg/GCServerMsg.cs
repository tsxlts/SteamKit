
using SteamKit.Client.Internal.Header;

namespace SteamKit.Client.Internal.Msg
{
    public class GCServerMsg : GCServerBaseMsg<GCHeader>
    {
        public GCServerMsg(uint msgType, uint appId, byte[] data) : base(msgType, appId, data)
        {
        }

        public override bool IsProto() => false;

        public override ulong JobID => Header.TargetJobID;
    }
}
