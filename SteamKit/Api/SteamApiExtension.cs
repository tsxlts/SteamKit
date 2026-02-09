using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Builder.ProxyBulider.Regexs;
using static SteamKit.Enums;
using static SteamKit.Internal.Utils;

namespace SteamKit.Api
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SteamApi
    {
        /// <summary>
        /// 获取注销登录参数
        /// </summary>
        /// <param name="website">当前所在站点</param>
        /// <param name="currentSessionId">当前登录用户SessionId</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<LogoutParametersResponse>> GetLogoutParametersAsync(Website website, string currentSessionId, CookieCollection userCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            string domain = website switch
            {
                var site when site.HasFlag(Website.Community) => proxy.SteamCommunity,
                var site when site.HasFlag(Website.Store) => proxy.SteamStore,
                var site when site.HasFlag(Website.Help) => proxy.SteamHelp,
                var site when site.HasFlag(Website.Checkout) => proxy.SteamCheckout,
                _ => proxy.SteamCommunity
            };

            Uri uri = new Uri($"{domain}/login/logout/");
            var @params = new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                { "sessionid",currentSessionId }
            });

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<LogoutParametersResponse> loginoutResponse = new WebResponse<string>(response).Convert(html =>
                {
                    if (string.IsNullOrWhiteSpace(html))
                    {
                        return null;
                    }

                    Regex regex = new Regex(@"var rgParameters = {([\s\S]+?)};");
                    var match = regex.Match(html);
                    if (!match.Success)
                    {
                        return null;
                    }

                    string json = $"{{{match.Groups[1].Value}}}";
                    var parameters = JsonConvert.DeserializeObject<JObject>(json)!;

                    regex = new Regex(@"rgParameters\.token = ""(.+?)"";");
                    match = regex.Match(html);
                    if (match.Success)
                    {
                        parameters["token"] = match.Groups[1].Value;
                    }

                    LogoutParametersResponse result = parameters.ToObject<LogoutParametersResponse>()!;
                    result.Parameters = parameters.ToObject<IDictionary<string, string>>()!;

                    regex = new Regex(@"TransferLogout\(\[(.+?)\],rgParameters\);");
                    match = regex.Match(html.Replace(" ", ""));
                    if (match.Success)
                    {
                        string array = $"[{match.Groups[1].Value}]";
                        List<string> url = JsonConvert.DeserializeObject<List<string>>(array)!;
                        result.Url = url;
                    }

                    return result;
                });
                return loginoutResponse;
            }
        }

        /// <summary>
        /// 获取用户Cookie
        /// </summary>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> GetCookiesAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<string>(response);
                return stringResponse;
            }
        }

        /// <summary>
        /// 获取用户SteamId
        /// </summary>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> GetSteamIdAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = SteamIdRegex.Match(html ?? "");
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }
                    return null;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 获取SessionId
        /// </summary>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> GetSessionIdAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = SessionIdRegex.Match(html ?? "");
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }

                    return null;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 获取用户个人资料
        /// </summary>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="loginCookies">当前登录用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<AccountProfilesResponse>> GetAccountProfilesAsync(string steamId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/edit/info");
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, autoRedirect: true, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, loginCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<AccountProfilesResponse> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = match = Regex.Match(html ?? "", @"data-profile-edit=""(?<profile>.+?)""");
                    if (!match.Success)
                    {
                        return null;
                    }

                    var json = HttpUtility.HtmlDecode(match.Groups["profile"].Value);
                    JToken profile = JsonConvert.DeserializeObject<JToken>(json)!;

                    var result = profile.ToObject<AccountProfilesResponse>();
                    return result;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 获取拥有的游戏库存
        /// </summary>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<List<AppInventoryContextsResponse>>> GetAppInventoryContextsAsync(string steamId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/inventory");
            CookieCollection cookies = new CookieCollection(loginCookies)
                .SetLanguage(Language.English)
                .SetWebTradeEligibility();

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, autoRedirect: true, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<List<AppInventoryContextsResponse>> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = Regex.Match(html ?? "", @"var(\s{1,})g_rgAppContextData(\s{0,})=(?<json>.+?);([\w\W]+)var(\s{1,})");
                    if (!match.Success)
                    {
                        return new List<AppInventoryContextsResponse>();
                    }

                    var token = JsonConvert.DeserializeObject<JToken>(match.Groups["json"].Value)!;
                    if (token is JArray)
                    {
                        return new List<AppInventoryContextsResponse>();
                    }
                    var obj = token.ToObject<JObject>()!;
                    var result = new List<AppInventoryContextsResponse>();

                    foreach (var item in obj)
                    {
                        var itemResult = item.Value!.ToObject<AppInventoryContextsResponse>()!;
                        itemResult.Contexts = new List<AppInventoryContextsResponse.AppContext>();

                        var rgContexts = item.Value.Value<JObject>("rgContexts")!;
                        foreach (var contextItem in rgContexts)
                        {
                            var context = contextItem.Value!.ToObject<AppInventoryContextsResponse.AppContext>()!;
                            itemResult.Contexts.Add(context);
                        }

                        result.Add(itemResult);
                    }

                    return result;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 检测交易权限
        /// 检测被检测用户的交易权限
        /// </summary>
        /// <param name="userCookies">被检测用户登录Cookie</param>
        /// <param name="checkerTradeLink">检测方的交易链接</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<List<TradePermissionsResponse>>> GetTradePermissionsAsync(CookieCollection userCookies, string checkerTradeLink, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri(checkerTradeLink.Replace(DefaultSteamCommunity, proxy.SteamCommunity));
            CookieCollection cookies = new CookieCollection(userCookies)
                .SetWebTradeEligibility();

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<List<TradePermissionsResponse>> steamResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = Regex.Match(html ?? "", @"var(\s{1,})g_rgAppContextData(\s{0,})=(?<json>.+?);([\w\W]+)var(\s{1,})");
                    if (!match.Success)
                    {
                        return new List<TradePermissionsResponse>();
                    }

                    var token = JsonConvert.DeserializeObject<JToken>(match.Groups["json"].Value)!;
                    if (token is JArray)
                    {
                        return new List<TradePermissionsResponse>();
                    }
                    var obj = token.ToObject<JObject>()!;
                    var result = new List<TradePermissionsResponse>();

                    foreach (var item in obj)
                    {
                        var itemResult = item.Value!.ToObject<TradePermissionsResponse>()!;
                        result.Add(itemResult);
                    }

                    return result;
                });
                return steamResponse;
            }
        }

        /// <summary>
        /// 获取ApiKey
        /// </summary>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> GetApiKeyAsync(CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/dev/apikey");
            CookieCollection cookies = new CookieCollection
            {
                loginCookies ,
                new Cookie(Extensions.SteamLanguageCookeName, "english")
            };
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = ApiKeyRegex.Match(html ?? "");
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }

                    return null;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 获取交易链接
        /// </summary>
        /// <param name="steamId">当前登录用户SteamId</param>
        /// <param name="loginCookies">登录Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<string>> GetTradeLinkAsync(string steamId, CookieCollection loginCookies, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/profiles/{steamId}/tradeoffers/privacy/");
            CookieCollection cookies = new CookieCollection(loginCookies)
                .SetLanguage(Language.English)
                .SetWebTradeEligibility();

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.HtmlFormate, autoRedirect: true, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, cookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<string> stringResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Match match = TradeLinkRegex.Match(html ?? "");

                    if (match.Success)
                    {
                        var tradeLink = match.Groups["tradeLink"].Value;
                        return $"{DefaultSteamCommunity}/{tradeLink.TrimStart('/')}";
                    }
                    return null;
                });
                return stringResponse;
            }
        }

        /// <summary>
        /// 查询物品信息
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">商品名称</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<QueryGoodsInfoResponse>> GetGoodsInfoAsync(string appId, string hashName, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/listings/{appId}/{Uri.EscapeDataString(hashName)}?l=english");

            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept-Language", "en-US,en;q=0.9"}
            };
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy, headers), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<QueryGoodsInfoResponse> marketResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Regex regex = new Regex(@"Market_LoadOrderSpread\((.+?)\)");
                    var match = regex.Match(html!);
                    if (!match.Success)
                    {
                        return null;
                    }

                    string Id = match.Groups[1].Value.Trim();
                    QueryGoodsInfoResponse result = new QueryGoodsInfoResponse
                    {
                        Id = Id,
                        AppId = appId,
                        HashName = hashName,
                        ContextId = null,
                        ClassId = null,
                        DefaultInspectUrl = null
                    };

                    regex = new Regex("var g_rgListingInfo =(?<listingInfo>.+?);");
                    match = regex.Match(html!);
                    if (!match.Success)
                    {
                        return result;
                    }

                    string jsonListing = HttpUtility.HtmlDecode(match.Groups["listingInfo"].Value);
                    JToken jTokenListing = JsonConvert.DeserializeObject<JToken>(jsonListing)!;
                    if (!(jTokenListing is JObject objListing))
                    {
                        return result;
                    }
                    if (objListing.Count < 1)
                    {
                        return result;
                    }

                    ulong? classId = null;
                    regex = new Regex(@"var g_rgAssets =(.+?),""classid"":""(?<classId>\d+)"",");
                    match = regex.Match(html!);
                    if (match.Success)
                    {
                        classId = ulong.Parse(match.Groups["classId"].Value);
                    }

                    string listingId;
                    JObject asset;
                    JArray actions;
                    string? assetId = null;
                    string? contextId = null;
                    string? inspectUrl = null;

                    foreach (var item in objListing)
                    {
                        listingId = item.Key;

                        asset = item.Value!.Value<JObject>("asset")!;
                        assetId = asset.Value<string>("id")!;
                        contextId = asset.Value<string>("contextid")!;
                        actions = asset.Value<JArray>("market_actions") ?? new JArray();
                        foreach (var action in actions)
                        {
                            string? actionName = action.Value<string>("name");
                            if (!(actionName?.Contains("在游戏中检视", StringComparison.InvariantCultureIgnoreCase) ?? false))
                            {
                                if (!(actionName?.Contains("Inspect in Game", StringComparison.InvariantCultureIgnoreCase) ?? false))
                                {
                                    continue;
                                }
                            }

                            inspectUrl = action.Value<string>("link")!;
                            inspectUrl = Regex.Replace(inspectUrl, @"(%listingid%)", listingId, RegexOptions.IgnoreCase);
                            inspectUrl = Regex.Replace(inspectUrl, @"(%assetid%)", assetId, RegexOptions.IgnoreCase);
                            break;
                        }

                        break;
                    }

                    result = new QueryGoodsInfoResponse
                    {
                        Id = Id,
                        AppId = appId,
                        ContextId = contextId,
                        ClassId = classId,
                        HashName = hashName,
                        DefaultInspectUrl = inspectUrl,
                    };

                    return result;
                });
                return marketResponse;
            }
        }

        /// <summary>
        /// 查询历史价格曲线图
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="hashName">商品名称</param>
        /// <param name="userCookies">用户Cookie</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<IWebResponse<List<PriceGraphResponse>>> GetPriceGraphAnonymousAsync(string appId, string hashName, CookieCollection? userCookies = null, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamCommunity}/market/listings/{appId}/{Uri.EscapeDataString(hashName)}");

            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Get, null, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy, userCookies), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                IWebResponse<List<PriceGraphResponse>> graphResponse = new WebResponse<string>(response).Convert(html =>
                {
                    Regex regex = new Regex("var line1=(.+?);");
                    var match = regex.Match(html!);
                    if (!match.Success)
                    {
                        return new List<PriceGraphResponse>();
                    }

                    string json = HttpUtility.HtmlDecode(match.Groups[1].Value);
                    JArray array = JsonConvert.DeserializeObject<JArray>(json)!;

                    var currencyRegex = new Regex("var strFormatPrefix = \"(.+?)\";");
                    var currencyMatch = currencyRegex.Match(html!);
                    string currency = currencyMatch.Success ? currencyMatch.Groups[1].Value : "$";
                    if (currency.StartsWith("\\u"))
                    {
                        byte[] bytes = new byte[2];
                        bytes[1] = byte.Parse(int.Parse(currency.Substring(2, 2), NumberStyles.HexNumber).ToString());
                        bytes[0] = byte.Parse(int.Parse(currency.Substring(4, 2), NumberStyles.HexNumber).ToString());
                        currency = Encoding.Unicode.GetString(bytes);
                    }

                    List<PriceGraphResponse> result = new List<PriceGraphResponse>();
                    PriceGraphResponse data;
                    JToken[] dataArray;
                    foreach (var item in array)
                    {
                        dataArray = item.ToObject<JToken[]>()!;

                        //May 04 2022 01: +0
                        DateTime.TryParseExact(dataArray[0].Value<string>(), "MMM d yyyy HH: z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time);
                        data = new PriceGraphResponse
                        {
                            Time = time.ToLocalTime(),
                            Price = dataArray[1].Value<decimal>(),
                            Count = dataArray[2].Value<int>(),
                            Currency = currency.Trim()
                        };
                        result.Add(data);
                    }

                    return result;
                });
                return graphResponse;
            }
        }

    }
}
