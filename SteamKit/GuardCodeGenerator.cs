
using System.Security.Cryptography;
using System.Text;

namespace SteamKit
{
    /// <summary>
    /// GuardCodeGenerator
    /// </summary>
    public static class GuardCodeGenerator
    {
        private static byte[] steamGuardCodeTranslations = new byte[] { 50, 51, 52, 53, 54, 55, 56, 57, 66, 67, 68, 70, 71, 72, 74, 75, 77, 78, 80, 81, 82, 84, 86, 87, 88, 89 };

        /// <summary>
        /// 获取登录令牌确认码
        /// </summary>
        /// <param name="timestamp">秒时间戳</param>
        /// <param name="sharedSecret">Steam共享秘钥</param>
        /// <returns></returns>
        public static string GenerateAuthCode(ulong timestamp, string sharedSecret)
        {
            byte[] sharedSecretArray = Convert.FromBase64String(sharedSecret);
            byte[] timeArray = new byte[8];

            timestamp /= 30L;

            for (int i = 8; i > 0; i--)
            {
                timeArray[i - 1] = (byte)timestamp;
                timestamp >>= 8;
            }

            byte[] hashedData = HmacSHA1Encode(sharedSecretArray, timeArray);
            byte[] codeArray = new byte[5];
            byte b = (byte)(hashedData[19] & 0xF);
            int codePoint = (hashedData[b] & 0x7F) << 24 | (hashedData[b + 1] & 0xFF) << 16 | (hashedData[b + 2] & 0xFF) << 8 | (hashedData[b + 3] & 0xFF);

            for (int i = 0; i < 5; ++i)
            {
                codeArray[i] = steamGuardCodeTranslations[codePoint % steamGuardCodeTranslations.Length];
                codePoint /= steamGuardCodeTranslations.Length;
            }
            return Encoding.UTF8.GetString(codeArray);
        }

        /// <summary>
        /// 获取身份确认授权码
        /// </summary>
        /// <param name="timestamp">秒时间戳</param>
        /// <param name="identitySecret">Steam身份秘钥</param>
        /// <param name="tag">操作标识</param>
        /// <returns></returns>
        public static string GenerateConfirmationCode(ulong timestamp, string identitySecret, string tag)
        {
            byte[] identitySecretArray = Convert.FromBase64String(identitySecret);
            int tagArrayLength = 8;
            if (!string.IsNullOrWhiteSpace(tag))
            {
                if (tag.Length > 32)
                {
                    tagArrayLength = 8 + 32;
                }
                else
                {
                    tagArrayLength = 8 + tag.Length;
                }
            }

            byte[] array = new byte[tagArrayLength];
            for (int i = 8; i > 0; i--)
            {
                array[i - 1] = (byte)timestamp;
                timestamp >>= 8;
            }

            if (!string.IsNullOrWhiteSpace(tag))
            {
                Array.Copy(Encoding.UTF8.GetBytes(tag), 0, array, 8, tagArrayLength - 8);
            }

            byte[] hashedData = HmacSHA1Encode(identitySecretArray, array);
            string hash = Convert.ToBase64String(hashedData, Base64FormattingOptions.None);
            return hash;
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="version">Version</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="sharedSecret">Steam共享秘钥</param>
        /// <returns></returns>
        public static byte[] GenerateSignature(int version, ulong clientId, ulong steamId, string sharedSecret)
        {
            byte[] sharedSecretArray = Convert.FromBase64String(sharedSecret);
            using (var hmac = new HMACSHA256(sharedSecretArray))
            {
                var signatureBytes = new List<byte>();
                signatureBytes.AddRange(BitConverter.GetBytes((short)version));
                signatureBytes.AddRange(BitConverter.GetBytes(clientId));
                signatureBytes.AddRange(BitConverter.GetBytes(steamId));

                var signature = hmac.ComputeHash(signatureBytes.ToArray());
                return signature;
            }
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="value">待签名字符串</param>
        /// <param name="secret">秘钥</param>
        /// <returns>Base64</returns>
        public static string GenerateSignature(string value, string secret)
        {
            byte[] secretArray = Convert.FromBase64String(secret);
            byte[] valueArray = Encoding.UTF8.GetBytes(value);

            var signature = GenerateSignature(valueArray, secretArray);
            return Convert.ToBase64String(signature);
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="value">待签名字符串</param>
        /// <param name="secret">秘钥</param>
        /// <returns>Base64</returns>
        public static byte[] GenerateSignature(byte[] value, byte[] secret)
        {
            using (var hmac = new HMACSHA256(secret))
            {
                var signatureBytes = new List<byte>();
                signatureBytes.AddRange(value);

                var signature = hmac.ComputeHash(signatureBytes.ToArray());
                return signature;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] HmacSHA1Encode(byte[] key, byte[] value)
        {
            HMACSHA1 hmacGenerator = new HMACSHA1();
            hmacGenerator.Key = key;
            byte[] hashedData = hmacGenerator.ComputeHash(value);
            return hashedData;
        }
    }
}
