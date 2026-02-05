using System.Security.Cryptography;

namespace SteamKit.Client.Internal
{

    /// <summary>
    /// Handles encrypting and decrypting using the RSA public key encryption
    /// algorithm.
    /// </summary>
    internal class RSACrypto : IDisposable
    {
        RSA rsa;

        /// <summary>
        /// Initializes a new instance of the <see cref="SteamKit2.RSACrypto"/> class.
        /// </summary>
        /// <param name="key">The public key to encrypt with.</param>
        public RSACrypto(byte[] key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            AsnKeyParser keyParser = new AsnKeyParser(key);

            rsa = RSA.Create();
            rsa.ImportParameters(keyParser.ParseRSAPublicKey());
        }

        /// <summary>
        /// Encrypt the specified input.
        /// </summary>
        /// <returns>The encrypted input.</returns>
        /// <param name="input">The input to encrypt.</param>
        public byte[] Encrypt(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return rsa.Encrypt(input, RSAEncryptionPadding.OaepSHA1);
        }

        /// <summary>
        /// Disposes of this class.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)rsa).Dispose();
        }
    }
}
