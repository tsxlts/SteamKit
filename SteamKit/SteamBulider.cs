using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SteamKit
{
    /// <summary>
    /// SteamBulider
    /// </summary>
    public class SteamBulider
    {
        /// <summary>
        /// 代理工厂
        /// </summary>
        /// <param name="stack">调用方法的StackFrame信息</param>
        /// <param name="methodName">调用方法的方法名</param>
        /// <returns></returns>
        public delegate Proxy ProxyFactory(StackFrame stack, string methodName);

        /// <summary>
        /// 默认SteamCommunity域名
        /// </summary>
        public const string DefaultSteamCommunity = "https://steamcommunity.com";

        /// <summary>
        /// 默认SteamApi域名
        /// </summary>
        public const string DefaultSteamApi = "https://api.steampowered.com";

        /// <summary>
        /// 默认SteamStore域名
        /// </summary>
        public const string DefaultSteamStore = "https://store.steampowered.com";

        /// <summary>
        /// 默认SteamHelp域名
        /// </summary>
        public const string DefaultSteamHelp = "https://help.steampowered.com";

        /// <summary>
        /// 默认SteamCheckout域名
        /// </summary>
        public const string DefaultSteamCheckout = "https://checkout.steampowered.com";

        /// <summary>
        /// 默认SteamLogin域名
        /// </summary>
        public const string DefaultSteamLogin = "https://login.steampowered.com";

        /// <summary>
        /// 默认AcceptLanguage
        /// </summary>
        public const string DefaultAcceptLanguage = "zh-CN,zh;q=0.9";

        private static ProxyFactory proxyFactory = (s, m) => Proxy.Instance;

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="proxy">
        /// 代理工厂
        /// <para>
        /// arg0：调用方法的StackFrame信息
        /// </para>
        /// <para>
        /// arg1：调用方法的方法名
        /// </para>
        /// </param>
        public static void WithProxy(ProxyFactory proxy)
        {
            proxyFactory = proxy;
        }

        internal static Proxy GetProxy([CallerMemberName] string? methodName = null)
        {
            var stackFrame = new StackFrame(1, true);
            var proxy = proxyFactory.Invoke(stackFrame, methodName!);
            return proxy ?? Proxy.Instance;
        }

        /// <summary>
        /// 代理
        /// </summary>
        public class Proxy
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            private Proxy()
            {

            }

            /// <summary>
            /// 设置代理
            /// </summary>
            /// <param name="proxy">IWebProxy</param>
            /// <returns></returns>
            public Proxy WithProxy(IWebProxy proxy)
            {
                WebProxy = proxy;
                return this;
            }

            /// <summary>
            /// 设置SteamCommunity
            /// </summary>
            /// <param name="steamCommunity">SteamCommunity</param>
            /// <returns></returns>
            public Proxy WithSteamCommunity(string steamCommunity)
            {
                if (string.IsNullOrWhiteSpace(steamCommunity))
                {
                    throw new ArgumentNullException(nameof(steamCommunity));
                }

                SteamCommunity = steamCommunity;
                return this;
            }

            /// <summary>
            /// 设置SteamApi
            /// </summary>
            /// <param name="steamApi">SteamApi</param>
            /// <returns></returns>
            public Proxy WithSteamApi(string steamApi)
            {
                if (string.IsNullOrWhiteSpace(steamApi))
                {
                    throw new ArgumentNullException(nameof(steamApi));
                }

                SteamApi = steamApi;
                return this;
            }

            /// <summary>
            /// 设置SteamStore
            /// </summary>
            /// <param name="steamStore">SteamStore</param>
            /// <returns></returns>
            public Proxy WithSteamStore(string steamStore)
            {
                if (string.IsNullOrWhiteSpace(steamStore))
                {
                    throw new ArgumentNullException(nameof(steamStore));
                }

                SteamStore = steamStore;
                return this;
            }

            /// <summary>
            /// 设置SteamHelp
            /// </summary>
            /// <param name="steamHelp">SteamHelp</param>
            /// <returns></returns>
            public Proxy WithSteamHelp(string steamHelp)
            {
                if (string.IsNullOrWhiteSpace(steamHelp))
                {
                    throw new ArgumentNullException(nameof(steamHelp));
                }

                SteamHelp = steamHelp;
                return this;
            }

            /// <summary>
            /// 设置SteamHelp
            /// </summary>
            /// <param name="steamCheckout">SteamCheckout</param>
            /// <returns></returns>
            public Proxy WithSteamCheckout(string steamCheckout)
            {
                if (string.IsNullOrWhiteSpace(steamCheckout))
                {
                    throw new ArgumentNullException(nameof(steamCheckout));
                }

                SteamCheckout = steamCheckout;
                return this;
            }

            /// <summary>
            /// 设置SteamLogin
            /// </summary>
            /// <param name="steamLogin">SteamLogin</param>
            /// <returns></returns>
            public Proxy WithSteamLogin(string steamLogin)
            {
                if (string.IsNullOrWhiteSpace(steamLogin))
                {
                    throw new ArgumentNullException(nameof(steamLogin));
                }

                SteamLogin = steamLogin;
                return this;
            }

            /// <summary>
            /// 设置AcceptLanguage
            /// </summary>
            /// <param name="acceptLanguage">AcceptLanguage</param>
            public Proxy WithAcceptLanguage(string acceptLanguage)
            {
                if (string.IsNullOrWhiteSpace(acceptLanguage))
                {
                    throw new ArgumentNullException(nameof(acceptLanguage));
                }

                AcceptLanguage = acceptLanguage;
                return this;
            }

            /// <summary>
            /// 设置请求头
            /// </summary>
            /// <param name="headers">headers</param>
            /// <returns></returns>
            public Proxy WithHeaders(IDictionary<string, string> headers)
            {
                if (headers == null)
                {
                    throw new ArgumentNullException(nameof(headers));
                }

                Headers.Clear();
                foreach (var item in headers)
                {
                    Headers.Add(item);
                }
                return this;
            }

            /// <summary>
            /// 设置请求头
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            /// <returns></returns>
            public Proxy SetHeaders(string key, string? value)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (value == null)
                {
                    Headers.Remove(key, out var _);
                }
                else
                {
                    Headers[key] = value;
                }
                return this;
            }

            /// <summary>
            /// 设置Cookie
            /// </summary>
            /// <param name="cookies">Cookies</param>
            /// <returns></returns>
            public Proxy WithCookies(CookieCollection cookies)
            {
                Cookies.Clear();
                Cookies.Add(cookies);
                return this;
            }

            /// <summary>
            /// 实例
            /// </summary>
            public static Proxy Instance => new Proxy();

            internal IWebProxy? WebProxy = null;
            internal string SteamCommunity = DefaultSteamCommunity;
            internal string SteamApi = DefaultSteamApi;
            internal string SteamStore = DefaultSteamStore;
            internal string SteamHelp = DefaultSteamHelp;
            internal string SteamCheckout = DefaultSteamCheckout;
            internal string SteamLogin = DefaultSteamLogin;
            internal string AcceptLanguage = DefaultAcceptLanguage;
            internal readonly IDictionary<string, string> Headers = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            internal readonly CookieCollection Cookies = new CookieCollection();
        }

        internal static class Regexs
        {
            public static Regex SteamIdRegex;
            public static Regex SessionIdRegex;
            public static Regex ApiKeyRegex;
            public static Regex TradeLinkRegex;

            static Regexs()
            {
                //g_steamID = "*********";
                SteamIdRegex = new Regex("g_steamID = \"(.+?)\"");
                //g_sessionID = "*********";
                SessionIdRegex = new Regex("g_sessionID = \"(.+?)\"");
                //<p>Key: *********</p>
                ApiKeyRegex = new Regex("<.+?>Key: (.+?)</.+?>");
                //<input size="45" type="text" class="trade_offer_access_url" id="trade_offer_access_url" value="https://steamcommunity.com/tradeoffer/new/?partner=****&token=****" readonly>
                TradeLinkRegex = new Regex("<.+? id=\"trade_offer_access_url\".*? value=\"(?<domain>.*?(?<tradeLink>/tradeoffer/new/\\?partner=.+?&token=.+?))\".*?>");
            }
        }
    }
}
