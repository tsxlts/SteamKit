using System.Net;
using System.Text;
using SteamKit.Attributes;
using SteamKit.Model;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Enums;

namespace SteamKit.Internal
{
    /// <summary>
    /// Utils
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// 16进制字符串转字节
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            int hexLen = hex.Length;
            byte[] ret = new byte[hexLen / 2];
            for (int i = 0; i < hexLen; i += 2)
            {
                ret[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return ret;
        }

        /// <summary>
        /// 字节转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 写入C风格的字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteCString(this BinaryWriter writer, string value)
        {
            writer.Write(value.ToCharArray());
            writer.Write((byte)0x00);
        }

        /// <summary>
        /// 获取API语言代码
        /// schinese
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string GetApiCode(this Language language)
        {
            Type type = language.GetType();
            if (type.GetField(language.ToString())?.GetCustomAttributes(typeof(LanguageAttribute), inherit: true)?.FirstOrDefault() is LanguageAttribute languageAttribute)
            {
                return languageAttribute.ApiCode;
            }

            return $"{language}";
        }

        /// <summary>
        /// 获取WebAPI语言代码
        /// zh-CN
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string GetWebApiCode(this Language language)
        {
            Type type = language.GetType();
            if (type.GetField(language.ToString())?.GetCustomAttributes(typeof(LanguageAttribute), inherit: true)?.FirstOrDefault() is LanguageAttribute languageAttribute)
            {
                return languageAttribute.WebApiCode;
            }

            return $"{language}";
        }

        /// <summary>
        /// 设置默认Headers
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IDictionary<string, string> SetDefaultHeaders(Proxy proxy, IDictionary<string, string>? headers = null)
        {
            IDictionary<string, string> result = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
            {
                { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36" },
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", proxy.AcceptLanguage}
            };
            if (proxy.Headers?.Count > 0)
            {
                foreach (var item in proxy.Headers)
                {
                    if (result.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                        continue;
                    }

                    result.Add(item);
                }
            }
            if (headers?.Count > 0)
            {
                foreach (var item in headers)
                {
                    if (result.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                        continue;
                    }

                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置默认Cookies
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieCollection SetDefaultCookies(Proxy proxy, CookieCollection? cookies = null)
        {
            CookieCollection result = new CookieCollection();
            if (proxy.Cookies?.Count > 0)
            {
                foreach (Cookie item in proxy.Cookies)
                {
                    result.Add(item!);
                }
            }
            if (cookies?.Count > 0)
            {
                foreach (Cookie item in cookies)
                {
                    result.Add(item!);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置手机端Headers
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IDictionary<string, string> SetMobileHeaders(Proxy proxy, IDictionary<string, string>? headers = null)
        {
            IDictionary<string, string> result = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
            {
                { "User-Agent", "Vdaima (Linux; U; Android 9; SM-G9810 Build/QP1A.190711.020; Valve Steam App Version/3)" },
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", proxy.AcceptLanguage}
            };
            if (proxy.Headers?.Count > 0)
            {
                foreach (var item in proxy.Headers)
                {
                    if (result.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                        continue;
                    }

                    result.Add(item);
                }
            }
            if (headers?.Count > 0)
            {
                foreach (var item in headers)
                {
                    if (result.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                        continue;
                    }

                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置手机端Cookie
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieCollection SetMobileCookies(CookieCollection? cookies = null)
        {
            CookieCollection userCookies = new CookieCollection
            {
                new Cookie("mobileClientVersion", "777777 3.7.7"),
                new Cookie("mobileClient", "android"),
            };
            if (cookies?.Count > 0)
            {
                userCookies.Add(cookies);
            }

            return userCookies;
        }

        /// <summary>
        /// 非成功状态抛出异常
        /// </summary>
        /// <param name="response"></param>
        /// <param name="exception"></param>
        /// <exception cref="ApiException"></exception>
        public static IWebResponse<T> ThrowIfError<T>(IWebResponse<T> response, Func<HttpStatusCode, bool>? exception = null)
        {
            exception ??= (code) => (int)code >= 500 || code == HttpStatusCode.NotFound;

            if (exception.Invoke(response.HttpStatusCode))
            {
                throw new ApiException(response.HttpStatusCode, response.Message ?? $"HttpStatusCode:{response.HttpStatusCode}", response.Response);
            }
            return response;
        }
    }
}
