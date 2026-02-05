using SteamKit.Client.Model;

namespace SteamKit.Client.Internal
{
    internal class MsgChannelEncryptRequest : ISteamSerializable
    {
        public EMsg GetEMsg() { return EMsg.ChannelEncryptRequest; }

        public static readonly uint PROTOCOL_VERSION = 1;
        public uint ProtocolVersion { get; set; }

        public EUniverse Universe { get; set; }

        public MsgChannelEncryptRequest()
        {
            ProtocolVersion = MsgChannelEncryptRequest.PROTOCOL_VERSION;
            Universe = EUniverse.Invalid;
        }

        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write(ProtocolVersion);
            bw.Write((int)Universe);
        }

        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            ProtocolVersion = br.ReadUInt32();
            Universe = (EUniverse)br.ReadInt32();
        }
    }

    internal class MsgChannelEncryptResponse : ISteamSerializable
    {
        public uint ProtocolVersion { get; set; }
        public uint KeySize { get; set; }

        public MsgChannelEncryptResponse()
        {
            ProtocolVersion = MsgChannelEncryptRequest.PROTOCOL_VERSION;
            KeySize = 128;
        }

        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write(ProtocolVersion);
            bw.Write(KeySize);

        }

        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            ProtocolVersion = br.ReadUInt32();
            KeySize = br.ReadUInt32();
        }
    }

    internal class MsgChannelEncryptResult : ISteamSerializable
    {
        public EMsg GetEMsg() { return EMsg.ChannelEncryptResult; }

        public EResult Result { get; set; }

        public MsgChannelEncryptResult()
        {
            Result = EResult.Invalid;
        }

        public void Serialize(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write((int)Result);

        }

        public void Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            Result = (EResult)br.ReadInt32();
        }
    }

    internal class MsgClientLogon
    {
        public static readonly uint ObfuscationMask = 0xBAADF00D;
        public static readonly uint CurrentProtocol = 65581;
        public static readonly uint ProtocolVerMajorMask = 0xFFFF0000;
        public static readonly uint ProtocolVerMinorMask = 0xFFFF;
        public static readonly ushort ProtocolVerMinorMinGameServers = 4;
        public static readonly ushort ProtocolVerMinorMinForSupportingEMsgMulti = 12;
        public static readonly ushort ProtocolVerMinorMinForSupportingEMsgClientEncryptPct = 14;
        public static readonly ushort ProtocolVerMinorMinForExtendedMsgHdr = 17;
        public static readonly ushort ProtocolVerMinorMinForCellId = 18;
        public static readonly ushort ProtocolVerMinorMinForSessionIDLast = 19;
        public static readonly ushort ProtocolVerMinorMinForServerAvailablityMsgs = 24;
        public static readonly ushort ProtocolVerMinorMinClients = 25;
        public static readonly ushort ProtocolVerMinorMinForOSType = 26;
        public static readonly ushort ProtocolVerMinorMinForCegApplyPESig = 27;
        public static readonly ushort ProtocolVerMinorMinForMarketingMessages2 = 27;
        public static readonly ushort ProtocolVerMinorMinForAnyProtoBufMessages = 28;
        public static readonly ushort ProtocolVerMinorMinForProtoBufLoggedOffMessage = 28;
        public static readonly ushort ProtocolVerMinorMinForProtoBufMultiMessages = 28;
        public static readonly ushort ProtocolVerMinorMinForSendingProtocolToUFS = 30;
        public static readonly ushort ProtocolVerMinorMinForMachineAuth = 33;
        public static readonly ushort ProtocolVerMinorMinForSessionIDLastAnon = 36;
        public static readonly ushort ProtocolVerMinorMinForEnhancedAppList = 40;
        public static readonly ushort ProtocolVerMinorMinForSteamGuardNotificationUI = 41;
        public static readonly ushort ProtocolVerMinorMinForProtoBufServiceModuleCalls = 42;
        public static readonly ushort ProtocolVerMinorMinForGzipMultiMessages = 43;
        public static readonly ushort ProtocolVerMinorMinForNewVoiceCallAuthorize = 44;
        public static readonly ushort ProtocolVerMinorMinForClientInstanceIDs = 44;
    }
}