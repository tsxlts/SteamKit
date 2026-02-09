
using System.Net;
using SteamKit.Api;
using SteamKit.Model;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Enums;
using static SteamKit.Internal.Utils;
using static SteamKit.Model.QueryInventoryHistoryResponse;

namespace SteamKit.WebClient
{
    /// <summary>
    /// Steam社区客户端
    /// </summary>
    public class SteamCommunityClient : SteamWebClient
    {
        private string? account;

        /// <summary>
        /// 
        /// </summary>
        public SteamCommunityClient() : base()
        {
        }

        /// <summary>
        /// 获取登录帐号名
        /// </summary>
        /// <returns></returns>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <exception cref="NotLoginException"></exception>
        public async ValueTask<string?> GetAccountNameAsync(CancellationToken cancellationToken = default)
        {
            CheckLogon();

            if (!string.IsNullOrWhiteSpace(account))
            {
                return account;
            }

            var checkToken = await SteamApi.GetClientWebTokenAsync(WebCookie, cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(checkToken.Body?.AccountName))
            {
                account = checkToken.Body?.AccountName;
            }
            return account;
        }

        /// <summary>
        /// 用户
        /// </summary>
        /// <exception cref="NotLoginException"></exception>
        public UserContext User
        {
            get
            {
                CheckLogon();
                return new UserContext(this);
            }
        }

        /// <summary>
        /// 库存
        /// </summary>
        /// <exception cref="NotLoginException"></exception>
        public InventoryContext Inventory
        {
            get
            {
                CheckLogon();
                return new InventoryContext(this);
            }
        }

        /// <summary>
        /// 交易报价
        /// </summary>
        /// <exception cref="NotLoginException"></exception>
        public TradeOfferContext TradeOffer
        {
            get
            {
                CheckLogon();
                return new TradeOfferContext(this);
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <exception cref="NotLoginException"></exception>
        public ConfirmationContext Confirmation
        {
            get
            {
                CheckLogon();
                return new ConfirmationContext(this);
            }
        }

        /// <summary>
        /// 市场
        /// </summary>
        public MarketContext Market
        {
            get
            {
                return new MarketContext(this);
            }
        }

        /// <summary>
        /// 站点
        /// </summary>
        protected override Website Website => Website.Community;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task FinalizeLoginAsync()
        {
            await base.FinalizeLoginAsync();

            Proxy proxy = GetProxy();
            string marketCheckUrl = $"{proxy.SteamCommunity}/market/eligibilitycheck/?" +
                $"goto={Uri.EscapeDataString($"{proxy.SteamCommunity}")}/market";
            var marketCheckResult = await SteamApi.GetAsync(marketCheckUrl, null, WebCookie, proxy.WebProxy).ConfigureAwait(false);
            WebCookie.Add(marketCheckResult.Cookies);

            WebCookie.SetLanguage(Language);
        }

        /// <summary>
        /// 完成注销
        /// </summary>
        /// <returns></returns>
        protected override Task FinalizeLogoutAsync()
        {
            var task = base.FinalizeLogoutAsync();

            account = null;
            return task;
        }

        /// <summary>
        /// 验证AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<(bool Success, CookieCollection Cookies)> VerifyAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            var cookies = new CookieCollection
            {
                new Cookie(Extensions.SteamSessionidCookieName,CreateSessionId()),
                new Cookie(Extensions.SteamAccessTokenCookeName, accessToken)
            };
            var checkToken = await SteamApi.GetClientWebTokenAsync(cookies, cancellationToken).ConfigureAwait(false);
            if (checkToken.Body == null || !checkToken.Body.LoggedIn)
            {
                return (false, new CookieCollection());
            }
            account = checkToken.Body.AccountName;
            cookies.Add(checkToken.Cookies);
            return (true, cookies);
        }

        /// <summary>
        /// 用户
        /// 需要登录
        /// </summary>
        public class UserContext
        {
            internal UserContext(SteamCommunityClient steamWebClient)
            {
                SteamWebClient = steamWebClient;
            }

            /// <summary>
            /// 获取Apikey
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<string?> GetApiKeyAsync(CancellationToken cancellationToken = default)
            {
                var apikeyResponse = await SteamApi.GetApiKeyAsync(SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return apikeyResponse.Body;
            }

            /// <summary>
            /// 获取交易链接
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<string?> GetTradeLinkAsync(CancellationToken cancellationToken = default)
            {
                var apikeyResponse = await SteamApi.GetTradeLinkAsync(SteamWebClient.SteamId!, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return apikeyResponse.Body;
            }

            /// <summary>
            /// 创建交易链接
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<string?> CreateTradeLinkAsync(CancellationToken cancellationToken = default)
            {
                var apikeyResponse = await SteamApi.CreateTradeLinkAsync(SteamWebClient.SessionId!, SteamWebClient.SteamId!, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return apikeyResponse.Body;
            }

            /// <summary>
            /// 查询好友
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<QueryOwnedFriendsResponse?> QueryFriendsAsync(CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var friendsResponse = await SteamApi.QueryFriendsAsync(accessToken, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(friendsResponse).Body;
            }

            /// <summary>
            /// 查询用户信息
            /// </summary>
            /// <param name="steamIds">SteamId</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<PlayerSummariesResponse?> QueryPlayerSummariesAsync(IEnumerable<string> steamIds, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var playerResponse = await SteamApi.QueryPlayerSummariesAsync(null, accessToken, steamIds, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(playerResponse).Body;
            }

            /// <summary>
            /// 查询钱包余额
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<QueryWalletDetailsResponse?> QueryWalletDetailsAsync(CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var playerResponse = await SteamApi.QueryWalletDetailsAsync(accessToken, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(playerResponse).Body;
            }

            /// <summary>
            /// 获取用户个人资料
            /// </summary>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<AccountProfilesResponse?> GetAccountProfilesAsync(CancellationToken cancellationToken = default)
            {
                var playerResponse = await SteamApi.GetAccountProfilesAsync(SteamWebClient.SteamId!, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(playerResponse).Body;
            }

            /// <summary>
            /// 编辑用户个人资料
            /// </summary>
            /// <param name="profiles">个人资料</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<EditAccountProfilesResponse?> EditAccountProfilesAsync(EditAccountProfilesParameter profiles, CancellationToken cancellationToken = default)
            {
                var playerResponse = await SteamApi.EditAccountProfilesAsync(SteamWebClient.SessionId!, SteamWebClient.SteamId!, profiles, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(playerResponse).Body;
            }

            /// <summary>
            /// SteamWebClient
            /// </summary>
            public SteamCommunityClient SteamWebClient { get; }
        }

        /// <summary>
        /// 报价
        /// 需要登录
        /// </summary>
        public class TradeOfferContext
        {
            internal TradeOfferContext(SteamCommunityClient steamWebClient)
            {
                SteamWebClient = steamWebClient;
            }

            /// <summary>
            /// 发送报价
            /// </summary>
            /// <param name="receiverSteamId">报价接收方Steam用户Id</param>
            /// <param name="receiverPartner">
            /// 报价接收方Partner
            /// 来自报价接收方的交易连接
            /// </param>
            /// <param name="receiverToken">
            /// 报价接收方Token</param>
            /// 来自报价接收方的交易连接
            /// <param name="currentAssets">报价发起方交换的资产</param>
            /// <param name="receiverAssets">报价接收方交换的资产</param>
            /// <param name="postscript">交易附言</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<SendOfferResponse?> SendOfferAsync(string receiverSteamId, string receiverPartner, string receiverToken, IEnumerable<SendOfferAsset>? currentAssets, IEnumerable<SendOfferAsset>? receiverAssets, string? postscript, CancellationToken cancellationToken = default)
            {
                var offerResponse = await SteamApi.SendOfferAsync(SteamWebClient.SessionId!, receiverSteamId, receiverPartner, receiverToken, currentAssets, receiverAssets, postscript, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 查询报价
            /// </summary>
            /// <param name="offerId">报价Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <param name="getDescriptions">是否获取资产描述信息</param>
            /// <returns></returns>
            public async Task<OfferResponse?> QueryOfferAsync(string offerId, bool getDescriptions = true, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var offerResponse = await SteamApi.QueryOfferAsync(accessToken, offerId, getDescriptions, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 查询报价
            /// </summary>
            /// <param name="sentOffer">查询发送的报价</param>
            /// <param name="receivedOffer">查询接收的报价</param>
            /// <param name="onlyActive">只查询活跃的报价</param>
            /// <param name="getDescriptions">是否获取资产描述信息</param>
            /// <param name="cursor">开始索引</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<OffersResponse?> QueryOffersAsync(bool sentOffer = true, bool receivedOffer = true, bool onlyActive = true, bool getDescriptions = true, long cursor = 0, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var offerResponse = await SteamApi.QueryOffersAsync(accessToken, sentOffer: sentOffer, receivedOffer: receivedOffer, onlyActive: onlyActive, getDescriptions: getDescriptions, cursor, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 接受报价
            /// </summary>
            /// <param name="offerId">报价Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<AcceptOfferResponse?> AcceptOfferAsync(string offerId, CancellationToken cancellationToken = default)
            {
                var offerResponse = await SteamApi.AcceptOfferAsync(SteamWebClient.SessionId!, offerId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 拒绝报价
            /// </summary>
            /// <param name="offerId">报价Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<DeclineOfferResponse?> DeclineOfferAsync(string offerId, CancellationToken cancellationToken = default)
            {
                var offerResponse = await SteamApi.DeclineOfferAsync(SteamWebClient.SessionId!, offerId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 取消报价
            /// </summary>
            /// <param name="offerId">报价Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<CancelOfferResponse?> CancelOfferAsync(string offerId, CancellationToken cancellationToken = default)
            {
                var offerResponse = await SteamApi.CancelOfferAsync(SteamWebClient.SessionId!, offerId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(offerResponse).Body;
            }

            /// <summary>
            /// 查询交易历史
            /// </summary>
            /// <param name="count">数量</param>
            /// <param name="afterTime">上一次返回的最后一条记录时间</param>
            /// <param name="afterTradeId">上一次返回的最后一条记录交易Id</param>
            /// <param name="includeFailed">是否包含失败的交易</param>
            /// <param name="getDescriptions">是否获取资产描述信息</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<TradeHistoryResponse?> QueryTradeHistoryAsync(int count = 10, long? afterTime = null, string? afterTradeId = null, bool includeFailed = false, bool getDescriptions = true, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var tradeResponse = await SteamApi.QueryTradeHistoryAsync(accessToken, count, afterTime, afterTradeId, includeFailed, getDescriptions, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(tradeResponse).Body;
            }

            /// <summary>
            /// 查询交易状态
            /// </summary>
            /// <param name="tradeId">交易Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<TradeStatusResponse?> QueryTradeStatusAsync(string tradeId, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var tradeResponse = await SteamApi.QueryTradeStatusAsync(accessToken, tradeId, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(tradeResponse).Body;
            }

            /// <summary>
            /// SteamWebClient
            /// </summary>
            public SteamCommunityClient SteamWebClient { get; }
        }

        /// <summary>
        /// 确认
        /// 需要登录
        /// </summary>
        public class ConfirmationContext
        {
            internal ConfirmationContext(SteamCommunityClient steamWebClient)
            {
                SteamWebClient = steamWebClient;
            }

            /// <summary>
            /// 查询待确认信息
            /// </summary>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<QueryConfirmationsResponse?> QueryConfirmationsAsync(string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationsResponse = await SteamApi.QueryConfirmationsAsync(SteamWebClient.SteamId!, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationsResponse).Body;
            }

            /// <summary>
            /// 查询待确认信息详情
            /// </summary>
            /// <param name="confirmationId">待确认信息Id</param>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns>HTML</returns>
            public async Task<string?> ConfirmationDetailAsync(string confirmationId, string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationResponse = await SteamApi.ConfirmationDetailAsync(SteamWebClient.SteamId!, confirmationId, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationResponse).Body;
            }

            /// <summary>
            /// 接受确认信息
            /// </summary>
            /// <param name="confirmations">待确认信息</param>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<bool> AllowConfirmationAsync(IEnumerable<ConfirmationBase> confirmations, string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationsResponse = await SteamApi.AllowMultiConfirmationAsync(SteamWebClient.SteamId!, confirmations, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationsResponse).Body;
            }

            /// <summary>
            /// 取消确认信息
            /// </summary>
            /// <param name="confirmations">待确认信息</param>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<bool> CancelConfirmationAsync(IEnumerable<ConfirmationBase> confirmations, string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationsResponse = await SteamApi.CancelMultiConfirmationAsync(SteamWebClient.SteamId!, confirmations, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationsResponse).Body;
            }

            /// <summary>
            /// 接受确认信息
            /// </summary>
            /// <param name="confirmation">待确认信息</param>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<bool> AllowConfirmationAsync(ConfirmationBase confirmation, string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationsResponse = await SteamApi.AllowConfirmationAsync(SteamWebClient.SteamId!, confirmation.Id, confirmation.Key, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationsResponse).Body;
            }

            /// <summary>
            /// 取消确认信息
            /// </summary>
            /// <param name="confirmation">待确认信息</param>
            /// <param name="deviceId">设备Id</param>
            /// <param name="identitySecret">身份秘钥</param>
            /// <param name="timestamp">当前时间戳</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<bool> CancelConfirmationAsync(ConfirmationBase confirmation, string deviceId, string identitySecret, ulong timestamp, CancellationToken cancellationToken = default)
            {
                var onfirmationsResponse = await SteamApi.CancelConfirmationAsync(SteamWebClient.SteamId!, confirmation.Id, confirmation.Key, deviceId, identitySecret, timestamp, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(onfirmationsResponse).Body;
            }

            /// <summary>
            /// SteamWebClient
            /// </summary>
            public SteamCommunityClient SteamWebClient { get; }
        }

        /// <summary>
        /// 库存
        /// </summary>
        public class InventoryContext
        {
            internal InventoryContext(SteamCommunityClient steamWebClient)
            {
                SteamWebClient = steamWebClient;
            }

            /// <summary>
            /// 查询用户库存
            /// </summary>
            /// <param name="steamId">待查询用户SteamId</param>
            /// <param name="appId">游戏Id</param>
            /// <param name="contextId">contextId</param>
            /// <param name="count">查询数量</param>
            /// <param name="lastMaxAssetId">上一次查询最大Id</param>
            /// <param name="language">语言类型</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<InventoryResponse?> QueryInventoryAsync(string steamId, string appId, string contextId, int count = 0, ulong? lastMaxAssetId = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
            {
                var inventoryResponse = await SteamApi.QueryInventoryAsync(steamId, appId, contextId, count, lastMaxAssetId, language, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(inventoryResponse).Body;
            }

            /// <summary>
            /// 查询自己的库存
            /// </summary>
            /// <param name="appId">AppId</param>
            /// <param name="contextId">ContextId</param>
            /// <param name="trading">
            /// 是否仅用于交易的库存
            /// <para>传入true：仅能查询 可交易的</para>
            /// <para>传入false：可查询 可交易的 和 不可交易的</para>
            /// </param>
            /// <param name="language">语言类型</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<SelfInventoryResponse?> QueryInventoryAsync(string appId, string contextId, bool trading = false, Language language = Language.Schinese, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var inventoryResponse = await SteamApi.QueryInventoryAsync(SteamWebClient.SteamId, appId, contextId, trading, language, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(inventoryResponse).Body;
            }

            /// <summary>
            /// 查询对方的库存
            /// </summary>
            /// <param name="partnerSteamId">对方的SteamId</param>
            /// <param name="partnerToken">对方的交易链接Token</param>
            /// <param name="appId">游戏Id</param>
            /// <param name="contextId">contextId</param>
            /// <param name="language">语言类型</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<SelfInventoryResponse?> QueryPartnerInventoryAsync(string partnerSteamId, string? partnerToken, string appId, string contextId, Language language = Language.Schinese, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var inventoryResponse = await SteamApi.QueryPartnerInventoryAsync(SteamWebClient.SessionId, partnerSteamId, partnerToken, appId, contextId, language, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(inventoryResponse).Body;
            }

            /// <summary>
            /// 查询库存历史记录
            /// </summary>
            /// <param name="appId">游戏Id</param>
            /// <param name="cursor">索引游标</param>
            /// <param name="language">语言类型</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<QueryInventoryHistoryResponse?> QueryInventoryHistoryAsync(IEnumerable<string>? appId, InventoryHistoryCursor? cursor, Language language = Language.Schinese, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var inventoryResponse = await SteamApi.QueryInventoryHistoryAsync(SteamWebClient.SteamId, SteamWebClient.SessionId, appId, cursor, language, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(inventoryResponse).Body;
            }

            /// <summary>
            /// 查询资产信息
            /// </summary>
            /// <param name="appId">AppId</param>
            /// <param name="classIds">
            /// ClassId和InstanceId
            /// </param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<AssetClassInfoResponse?> QueryAssetClassInfoAsync(string appId, IEnumerable<QueryAssetClassInfoParameter> classIds, CancellationToken cancellationToken = default)
            {
                string accessToken = SteamWebClient.WebApiToken!;
                var classResponse = await SteamApi.QueryAssetClassInfoAsync(null, accessToken, appId, classIds, SteamWebClient.Language, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(classResponse).Body;
            }

            /// <summary>
            /// SteamWebClient
            /// </summary>
            public SteamCommunityClient SteamWebClient { get; }
        }

        /// <summary>
        /// 市场
        /// </summary>
        public class MarketContext
        {
            internal MarketContext(SteamCommunityClient steamWebClient)
            {
                SteamWebClient = steamWebClient;
            }

            /// <summary>
            /// 出售资产
            /// </summary>
            /// <param name="appId">AppId</param>
            /// <param name="contextId">ContextId</param>
            /// <param name="assetId">资产Id</param>
            /// <param name="price">
            /// 出售价格
            /// 实际收款金额
            /// 单位：分
            /// </param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<SellAssetResponse?> SellAssetAsync(string appId, string contextId, ulong assetId, int price, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var sellResponse = await SteamApi.SellAssetAsync(SteamWebClient.SessionId, appId, contextId, assetId, price, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(sellResponse, code => code == HttpStatusCode.NotFound).Body;
            }

            /// <summary>
            /// 下架商品
            /// </summary>
            /// <param name="listingId">商品Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<RemoveListingResponse?> RemoveListingAsync(string listingId, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var inventoryResponse = await SteamApi.RemoveListingAsync(SteamWebClient.SessionId, listingId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(inventoryResponse, code => code == HttpStatusCode.NotFound).Body;
            }

            /// <summary>
            /// 查询自己的在售商品和订购单
            /// </summary>
            /// <param name="start">分页开始索引</param>
            /// <param name="count">分页大小</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<QuetyListingsResponse?> QuetyListingsAsync(int start, int count, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var listingsResult = await SteamApi.QuetyListingsAsync(start, count, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(listingsResult).Body;
            }

            /// <summary>
            /// 查询自己的市场订单历史记录
            /// </summary>
            /// <param name="start"></param>
            /// <param name="count"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<QuetyMarketHistoryResponse?> QueryListingsHistoryAsync(int start, int count, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var listingsResult = await SteamApi.QueryListingsHistoryAsync(start, count, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(listingsResult).Body;
            }

            /// <summary>
            /// 查询市场价格
            /// </summary>
            /// <param name="appId"></param>
            /// <param name="hashName"></param>
            /// <param name="currency"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<MarketPriceResponse?> QueryMarketPriceAsync(string appId, string hashName, Currency currency = Currency.CNY, CancellationToken cancellationToken = default)
            {
                var market = await SteamApi.QueryMarketPriceAsync(appId, hashName, currency, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(market).Body;
            }

            /// <summary>
            /// 查询市场商品
            /// </summary>
            /// <param name="appId">游戏Id</param>
            /// <param name="count">查询数量</param>
            /// <param name="start">查询起始索引</param>
            /// <param name="query">查询关键词</param>
            /// <param name="categories">分类信息</param>
            /// <param name="sortColumn">排序列</param>
            /// <param name="sortType">排序方式</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<MarketResponse?> QueryMarketAsync(string appId, int count, int start, string? query = null, IEnumerable<AssetFilter>? categories = null, string sortColumn = "default", string sortType = "asc", CancellationToken cancellationToken = default)
            {
                var market = await SteamApi.QueryMarketAsync(appId, count, start, query, categories, sortColumn, sortType, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(market).Body;
            }

            /// <summary>
            /// 查询市场商品列表
            /// </summary>
            /// <param name="appId">游戏Id</param>
            /// <param name="hashName"></param>
            /// <param name="start">分页开始索引</param>
            /// <param name="count">分页大小</param>
            /// <param name="currency">货币类型</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <returns></returns>
            public async Task<MarketListingsResponse?> QueryMarketListingsAsync(string appId, string hashName, int start, int count, Currency currency = Currency.CNY, CancellationToken cancellationToken = default)
            {
                var market = await SteamApi.QueryMarketListingsAsync(appId, hashName, start, count, currency, SteamWebClient.Language, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(market).Body;
            }

            /// <summary>
            /// 购买商品
            /// </summary>
            /// <param name="listingId">商品Id</param>
            /// <param name="price">
            /// 商品单价
            /// 不包含手续费
            /// 单位：分
            /// </param>
            /// <param name="fee">
            /// 商品手续费
            /// 单位：分
            /// </param>
            /// <param name="currency">货币类型</param>
            /// <param name="quantity">购买数量</param>
            /// <param name="confirmationId">令牌确认Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<BuyAssetResponse?> BuyAssetAsync(string listingId, int price, int fee, Currency currency, int quantity, string? confirmationId, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var sellResponse = await SteamApi.BuyAssetAsync(SteamWebClient.SessionId, listingId, price, fee, currency, quantity, confirmationId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(sellResponse, code => code == HttpStatusCode.NotFound).Body;
            }

            /// <summary>
            /// 创建订购单
            /// </summary>
            /// <param name="appId">游戏Id</param>
            /// <param name="hashName">HashName</param>
            /// <param name="price">
            /// 订购价格
            /// 单位：分
            /// </param>
            /// <param name="tradefeeTax">
            /// 交易手续费
            /// 单位：分
            /// </param>
            /// <param name="currency">货币类型</param>
            /// <param name="quantity">购买数量</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<CreateBuyOrderResponse?> CreateBuyOrderAsync(string appId, string hashName, int price, int tradefeeTax, Currency currency, int quantity, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var sellResponse = await SteamApi.CreateBuyOrderAsync(SteamWebClient.SessionId, appId, hashName, price, tradefeeTax, currency, quantity, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(sellResponse, code => code == HttpStatusCode.NotFound).Body;
            }

            /// <summary>
            /// 取消订购单响应
            /// </summary>
            /// <param name="buyOrderId">订购单Id</param>
            /// <param name="cancellationToken">CancellationToken</param>
            /// <exception cref="NotLoginException"></exception>
            /// <returns></returns>
            public async Task<CancelBuyOrderResponse?> CancelBuyOrderAsync(string buyOrderId, CancellationToken cancellationToken = default)
            {
                SteamWebClient.CheckLogon();
                var sellResponse = await SteamApi.CancelBuyOrderAsync(SteamWebClient.SessionId, buyOrderId, SteamWebClient.WebCookie, cancellationToken).ConfigureAwait(false);
                return ThrowIfError(sellResponse, code => code == HttpStatusCode.NotFound).Body;
            }

            /// <summary>
            /// SteamWebClient
            /// </summary>
            public SteamCommunityClient SteamWebClient { get; }
        }
    }
}
