
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using ProtoBuf;
using SteamKit.Client.Model.Proto;
using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Internal.Utils;
using static SteamKit.SteamBulider;
using static SteamKit.SteamEnum;

namespace SteamKit
{
    /// <summary>
    /// SteamWebApi
    /// </summary>
    public static class SteamWebApi
    {
        /// <summary>
        /// 查询库存
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="steamId">待查询用户SteamId</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="contextId">contextId</param>
        /// <param name="count">查询数量</param>
        /// <param name="startAssetId">上一次查询最大Id</param>
        /// <param name="tradableOnly">是否仅查询可交易的</param>
        /// <param name="marketableOnly">是否仅查询可出售的</param>
        /// <param name="assetIds">需要查询的资产Id</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CEcon_GetInventoryItemsWithDescriptions_Response>> QueryInventoryAsync(string accessToken, ulong steamId, uint appId, ulong contextId, int count = 0, ulong? startAssetId = null, bool tradableOnly = false, bool marketableOnly = false, IEnumerable<ulong>? assetIds = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var filter = new CEcon_GetInventoryItemsWithDescriptions_Request.FilterOptions
            {
                tradable_only = tradableOnly,
                marketable_only = marketableOnly,
            };
            if (assetIds?.Any() ?? false)
            {
                filter.assetids.AddRange(assetIds.Select(c => c));
            }
            var body = new CEcon_GetInventoryItemsWithDescriptions_Request
            {
                steamid = steamId,
                appid = appId,
                contextid = contextId,
                count = count,
                start_assetid = startAssetId ?? 0,
                for_trade_offer_verification = false,
                get_descriptions = true,
                get_asset_properties = true,
                language = $"{language.GetApiCode()}",
                filters = filter
            };

            return await SendAsync<CEcon_GetInventoryItemsWithDescriptions_Request, CEcon_GetInventoryItemsWithDescriptions_Response>(proxy, null, accessToken, "IEconService", "GetInventoryItemsWithDescriptions", HttpMethod.Get, body, version: 1, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询资产描述信息
        /// </summary>
        /// <param name="accessToken">
        /// 用户登录Token
        /// </param>
        /// <param name="appId">游戏Id</param>
        /// <param name="classes">资产属性</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<CEcon_GetAssetClassInfo_Response>> QueryAssetClassInfoAsync(string accessToken, uint appId, IEnumerable<ClassIdentifiersParameter> classes, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();

            var body = new CEcon_GetAssetClassInfo_Request
            {
                appid = appId,
                language = $"{language.GetWebApiCode()}",
            };
            body.classes.AddRange(classes.Select(c => new CEconItem_ClassIdentifiers
            {
                classid = c.ClassId,
                instanceid = c.InstanceId,
            }));

            return await SendAsync<CEcon_GetAssetClassInfo_Request, CEcon_GetAssetClassInfo_Response>(proxy, null, accessToken, "IEconService", "GetAssetClassInfo", HttpMethod.Get, body, version: 1, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TRequest">请求数据类型</typeparam>
        /// <typeparam name="TResponse">响应数据类型</typeparam>
        /// <param name="proxy">Proxy</param>
        /// <param name="apiKey">ApiKey</param>
        /// <param name="accessToken">AccessToken</param>
        /// <param name="serverName">服务名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="arg">请求参数</param>
        /// <param name="version">接口版本</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<TResponse>> SendAsync<TRequest, TResponse>(Proxy proxy, string? apiKey, string? accessToken, string serverName, string methodName, HttpMethod method, TRequest arg, int version = 1, CancellationToken cancellationToken = default) where TRequest : class, new() where TResponse : class, new()
        {
            string body;
            using (var inputPayload = new MemoryStream())
            {
                Serializer.Serialize(inputPayload, arg);
                body = Convert.ToBase64String(inputPayload.ToArray());
            }
            IDictionary<string, object> args = new Dictionary<string, object>
            {
                {"format","protobuf_raw" },
                {"input_protobuf_encoded",body }
            };

            return await SendAsync<TResponse>(proxy, apiKey, accessToken, serverName, methodName, method, args, version, cancellationToken);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TResponse">响应数据类型</typeparam>
        /// <param name="proxy">Proxy</param>
        /// <param name="apiKey">ApiKey</param>
        /// <param name="accessToken">AccessToken</param>
        /// <param name="serverName">服务名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="method">HttpMethod</param>
        /// <param name="args">请求参数</param>
        /// <param name="version">接口版本</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<TResponse>> SendAsync<TResponse>(Proxy proxy, string? apiKey, string? accessToken, string serverName, string methodName, HttpMethod method, IDictionary<string, object> args, int version = 1, CancellationToken cancellationToken = default)
        {
            StringBuilder builder = new StringBuilder($"{proxy.SteamApi}/{serverName}/{methodName}/v{version}");

            StringBuilder @params = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                @params.Append($"&key={Uri.EscapeDataString(apiKey ?? "")}");
            }
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                @params.Append($"&access_token={Uri.EscapeDataString(accessToken ?? "")}");
            }
            foreach (var (key, value) in args ?? new Dictionary<string, object>())
            {
                @params.Append($"&{Uri.EscapeDataString(key)}=");
                switch (value)
                {
                    case null:
                        break;

                    case byte[] byteArrayValue:
                        @params.Append(HttpUtility.UrlEncode(byteArrayValue));
                        break;

                    default:
                        if (value.ToString() is { } valueString)
                        {
                            @params.Append(Uri.EscapeDataString(valueString));
                        }
                        break;
                }
            }
            string body = @params.ToString().TrimStart('&');

            HttpContent? content = new StringContent(body, new MediaTypeHeaderValue("application/x-www-form-urlencoded"));
            if (method == HttpMethod.Get)
            {
                builder.Append($"/?{body}");
                content = null;
            }

            Uri uri = new Uri(builder.ToString());
            using (var response = await InternalHttpClient.SendAsync(uri, method, content, InternalHttpClient.ResultType.Stream, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<TResponse> apiResponse = SteamApiResponse<TResponse>.ProtobufResponse(response);
                return apiResponse;
            }
        }
    }
}
