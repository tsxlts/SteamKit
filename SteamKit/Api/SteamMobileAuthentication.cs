using SteamKit.Internal;
using SteamKit.Model;
using SteamKit.Model.Internal;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Internal.Utils;

namespace SteamKit.Api
{
    /// <summary>
    /// Steam手机授权
    /// </summary>
    public static class SteamMobileAuthentication
    {
        /// <summary>
        /// 获取WGToken
        /// </summary>
        /// <param name="accessToken">用户登录Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ISteamApiResponse<GetWGTokenResponse>> GetWGTokenAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            Proxy proxy = GetProxy();
            Uri uri = new Uri($"{proxy.SteamApi}/IMobileAuthService/GetWGToken/v1/?" +
               $"access_token={Uri.EscapeDataString(accessToken ?? "")}");

            var @params = new FormUrlEncodedContent(new Dictionary<string, string>
            {
            });
            using (var response = await InternalHttpClient.SendAsync(uri, HttpMethod.Post, @params, InternalHttpClient.ResultType.JsonFormate, headers: SetDefaultHeaders(proxy), cookies: SetDefaultCookies(proxy), proxy: proxy.WebProxy, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                ISteamApiResponse<GetWGTokenResponse> loginResponse = SteamApiResponse<ApiResponse<GetWGTokenResponse>>.JsonResponse(response).Convert(c => c?.Response);
                return loginResponse;
            }
        }
    }
}
