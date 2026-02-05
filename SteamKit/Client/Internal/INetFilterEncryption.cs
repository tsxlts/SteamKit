
namespace SteamKit.Client.Internal
{
    interface INetFilterEncryption
    {
        byte[] ProcessIncoming(byte[] data);

        int ProcessOutgoing(Span<byte> data, byte[] output);

        int CalculateMaxEncryptedDataLength(int plaintextDataLength);
    }
}
