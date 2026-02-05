using ProtoBuf;

namespace SteamKit.Game
{
    [ProtoContract()]
    internal partial class UnifiedClientHello : IExtensible
    {
        private IExtension __pbn__extensionData;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [ProtoMember(1)]
        public uint version { get; set; }
    }

    internal enum EGCUnifiedMsg
    {
        ClientWelcome = 4004,
        ClientHello = 4006,
    }
}
