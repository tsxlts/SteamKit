
using System.Buffers;
using System.Security.Cryptography;

namespace SteamKit.Client.Internal
{
    internal class NetFilterEncryptionWithHMAC : INetFilterEncryption
    {
        const int InitializationVectorLength = 16;
        const int InitializationVectorRandomLength = 3;

        readonly Aes aes;
        readonly byte[] hmacSecret;

        public NetFilterEncryptionWithHMAC(byte[] sessionKey)
        {
            hmacSecret = new byte[16];
            Array.Copy(sessionKey, 0, hmacSecret, 0, hmacSecret.Length);

            aes = Aes.Create();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = sessionKey;
        }

        public byte[] ProcessIncoming(byte[] data)
        {
            try
            {
                return SymmetricDecryptHMACIV(data);
            }
            catch (CryptographicException ex)
            {
                throw new IOException("Unable to decrypt incoming packet", ex);
            }
        }

        public int ProcessOutgoing(Span<byte> data, byte[] output)
        {
            return SymmetricEncryptWithHMACIV(data, output);
        }

        byte[] SymmetricDecryptHMACIV(Span<byte> input)
        {
            Span<byte> iv = stackalloc byte[InitializationVectorLength];

            aes.DecryptEcb(input[..iv.Length], iv, PaddingMode.None);
            byte[] plainText = aes.DecryptCbc(input[iv.Length..], iv, PaddingMode.PKCS7);

            ValidateInitializationVector(plainText, iv);

            return plainText;
        }

        int SymmetricEncryptWithHMACIV(Span<byte> input, byte[] output)
        {
            Span<byte> iv = stackalloc byte[InitializationVectorLength];

            GenerateInitializationVector(input, iv);

            var outputSpan = output.AsSpan();

            var cryptedIvLength = aes.EncryptEcb(iv, outputSpan, PaddingMode.None);
            var cipherTextLength = aes.EncryptCbc(input, iv, outputSpan[cryptedIvLength..], PaddingMode.PKCS7);

            return cryptedIvLength + cipherTextLength;
        }

        void GenerateInitializationVector(Span<byte> plainText, Span<byte> iv)
        {
            var hashLength = InitializationVectorLength - InitializationVectorRandomLength;
            RandomNumberGenerator.Fill(iv[hashLength..]);

            var hmacBufferLength = plainText.Length + InitializationVectorRandomLength;
            var hmacBuffer = ArrayPool<byte>.Shared.Rent(hmacBufferLength);

            try
            {
                var hmacBufferSpan = hmacBuffer.AsSpan()[..hmacBufferLength];

                iv[^InitializationVectorRandomLength..].CopyTo(hmacBufferSpan[..InitializationVectorRandomLength]);
                plainText.CopyTo(hmacBufferSpan[InitializationVectorRandomLength..]);

                Span<byte> hashValue = stackalloc byte[HMACSHA1.HashSizeInBytes];

                HMACSHA1.HashData(hmacSecret, hmacBufferSpan, hashValue);

                hashValue[..hashLength].CopyTo(iv);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(hmacBuffer);
            }
        }

        void ValidateInitializationVector(Span<byte> plainText, Span<byte> iv)
        {
            var hashLength = InitializationVectorLength - InitializationVectorRandomLength;
            var hmacBufferLength = plainText.Length + InitializationVectorRandomLength;
            var hmacBuffer = ArrayPool<byte>.Shared.Rent(hmacBufferLength);

            try
            {
                var hmacBufferSpan = hmacBuffer.AsSpan()[..hmacBufferLength];

                iv[^InitializationVectorRandomLength..].CopyTo(hmacBufferSpan[..InitializationVectorRandomLength]);
                plainText.CopyTo(hmacBufferSpan[InitializationVectorRandomLength..]);

                Span<byte> hashValue = stackalloc byte[HMACSHA1.HashSizeInBytes];

                HMACSHA1.HashData(hmacSecret, hmacBufferSpan, hashValue);

                if (!hashValue[..hashLength].SequenceEqual(iv[..hashLength]))
                {
                    throw new CryptographicException("HMAC from server did not match computed HMAC.");
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(hmacBuffer);
            }
        }

        public int CalculateMaxEncryptedDataLength(int plaintextDataLength)
        {
            int blockSize = aes.BlockSize / 8;
            int cipherTextSize = (plaintextDataLength + blockSize) / blockSize * blockSize;
            return InitializationVectorLength + cipherTextSize;
        }
    }
}
