using System.Security.Cryptography;
using SteamKit.Client.Internal.Server;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Internal;
using SteamKit.Model;
using static SteamKit.SteamEnum;

namespace SteamKit.Client
{
    /// <summary>
    /// SteamClientExtensions
    /// </summary>
    public static class SteamClientExtensions
    {
        /// <summary>
        /// 查询库存
        /// 仅可查询当前登录用户的库存
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="appId">AppId</param>
        /// <param name="contextId">ContextId</param>
        /// <param name="count">查询数量</param>
        /// <param name="startAssetId">开始查询的资产Id</param>
        /// <param name="tradableOnly">是否只查可以交易的资产</param>
        /// <param name="marketableOnly">是否只查可以出售的资产</param>
        /// <param name="assetIds">指定查询的资产</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CEcon_GetInventoryItemsWithDescriptions_Response>> QueryInventoryAsync(this SteamClient steamClient, uint appId, ulong contextId, int count = 0, ulong? startAssetId = null, bool tradableOnly = false, bool marketableOnly = false, IEnumerable<ulong>? assetIds = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            var filter = new CEcon_GetInventoryItemsWithDescriptions_Request.FilterOptions
            {
                tradable_only = tradableOnly,
                marketable_only = marketableOnly,
            };
            if (assetIds?.Any() ?? false)
            {
                filter.assetids.AddRange(assetIds.Select(c => c));
            }

            var task = steamClient.ServiceMethodCallAsync<IEcon, CEcon_GetInventoryItemsWithDescriptions_Response>(c => c.GetInventoryItemsWithDescriptions(new CEcon_GetInventoryItemsWithDescriptions_Request
            {
                steamid = steamClient.SteamId!.Value,
                appid = appId,
                contextid = contextId,
                count = count,
                start_assetid = startAssetId ?? 0,
                for_trade_offer_verification = false,
                get_descriptions = true,
                get_asset_properties = true,
                language = $"{language.GetApiCode()}",
                filters = filter
            }), cancellationToken: cancellationToken);

            var result = await task;
            return new ClientResult<CEcon_GetInventoryItemsWithDescriptions_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 查询资产ClassInfo
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="appId">AppId</param>
        /// <param name="classes">资产Class信息</param>
        /// <param name="language">语言类型</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CEcon_GetAssetClassInfo_Response>> QueryAssetClassInfoAsync(this SteamClient steamClient, uint appId, IEnumerable<ClassIdentifiersParameter> classes, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            var message = new CEcon_GetAssetClassInfo_Request
            {
                appid = appId,
                language = $"{language.GetWebApiCode()}",
            };
            message.classes.AddRange(classes.Select(c => new CEconItem_ClassIdentifiers
            {
                classid = c.ClassId,
                instanceid = c.InstanceId,
            }));
            var task = steamClient.ServiceMethodCallAsync<IEcon, CEcon_GetAssetClassInfo_Response>(c => c.GetAssetClassInfo(message), cancellationToken: cancellationToken);

            var result = await task;
            return new ClientResult<CEcon_GetAssetClassInfo_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 获取交易链接Token
        /// </summary>
        /// <param name="steamClient"></param>
        /// <param name="generateNewToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ClientResult<CEcon_GetTradeOfferAccessToken_Response>> QueryTradeOfferAccessTokenAsync(this SteamClient steamClient, bool generateNewToken = false, CancellationToken cancellationToken = default)
        {
            var message = new CEcon_GetTradeOfferAccessToken_Request
            {
                generate_new_token = generateNewToken,
            };
            var task = steamClient.ServiceMethodCallAsync<IEcon, CEcon_GetTradeOfferAccessToken_Response>(c => c.GetTradeOfferAccessToken(message), cancellationToken: cancellationToken);

            var result = await task;
            return new ClientResult<CEcon_GetTradeOfferAccessToken_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 查询待确认的登录授权信息
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<List<ulong>>> QueryAuthSessionsForAccountAsync(this SteamClient steamClient, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync((IAuthentication api) => api.GetAuthSessionsForAccount(new CAuthentication_GetAuthSessionsForAccount_Request
            {
            }), cancellationToken: cancellationToken).ConfigureAwait(false);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<List<ulong>>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = new List<ulong>()
                };
            }

            return new ClientResult<List<ulong>>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result!.client_ids
            };
        }

        /// <summary>
        /// 查询待确认登录授权Session信息
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CAuthentication_GetAuthSessionInfo_Response>> QueryAuthSessionInfoAsync(this SteamClient steamClient, ulong clientId, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync((IAuthentication api) => api.GetAuthSessionInfo(new CAuthentication_GetAuthSessionInfo_Request
            {
                client_id = clientId,
            }), cancellationToken: cancellationToken).ConfigureAwait(false);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<CAuthentication_GetAuthSessionInfo_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null
                };
            }

            return new ClientResult<CAuthentication_GetAuthSessionInfo_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result
            };
        }

        /// <summary>
        /// 手机令牌确认
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="version">Version</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="sharedSecret">登录共享秘钥</param>
        /// <param name="confirm">是否确认登录</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<EResult>> MobileConfirmationAsync(this SteamClient steamClient, int version, ulong clientId, string sharedSecret, bool confirm, CancellationToken cancellationToken = default)
        {
            var signature = GuardCodeGenerator.GenerateSignature(version, clientId, steamClient.SteamId!.Value, sharedSecret);
            var task = steamClient.ServiceMethodCallAsync((IAuthentication api) => api.UpdateAuthSessionWithMobileConfirmation(new CAuthentication_UpdateAuthSessionWithMobileConfirmation_Request
            {
                steamid = steamClient.SteamId!.Value,
                client_id = clientId,
                confirm = confirm,
                persistence = ESessionPersistence.k_ESessionPersistence_Persistent,
                version = version,
                signature = signature
            }), cancellationToken: cancellationToken).ConfigureAwait(false);

            var result = await task;

            return new ClientResult<EResult>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.EResult
            };
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="renewal">是否续期RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>> GenerateAppAccessTokenAsync(this SteamClient steamClient, bool renewal = true, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(steamClient.RefreshToken))
            {
                throw new InvalidOperationException("RefreshToken为空,请使用AuthToken登录客户端");
            }

            var task = steamClient.ServiceMethodCallAsync<IAuthentication, CAuthentication_AccessToken_GenerateForApp_Response>(c => c.GenerateAccessTokenForApp(new CAuthentication_AccessToken_GenerateForApp_Request
            {
                steamid = steamClient.SteamId!.Value,
                refresh_token = steamClient.RefreshToken,
                renewal_type = renewal ? ETokenRenewalType.k_ETokenRenewalType_Allow : ETokenRenewalType.k_ETokenRenewalType_None
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK || string.IsNullOrEmpty(result.Result?.access_token))
            {
                return new ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="renewal">是否续期RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>> GenerateAppAccessTokenAsync(this SteamClient steamClient, ulong steamId, string refreshToken, bool renewal = true, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync<IAuthentication, CAuthentication_AccessToken_GenerateForApp_Response>(c => c.GenerateAccessTokenForApp(new CAuthentication_AccessToken_GenerateForApp_Request
            {
                steamid = steamId,
                refresh_token = refreshToken,
                renewal_type = renewal ? ETokenRenewalType.k_ETokenRenewalType_Allow : ETokenRenewalType.k_ETokenRenewalType_None
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK || string.IsNullOrEmpty(result.Result?.access_token))
            {
                return new ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CAuthentication_AccessToken_GenerateForApp_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 查询隐私设置
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CPlayer_GetPrivacySettings_Response>> QueryPrivacySettingsAsync(this SteamClient steamClient, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync<IPlayer, CPlayer_GetPrivacySettings_Response>(c => c.GetPrivacySettings(new CPlayer_GetPrivacySettings_Request
            {
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<CPlayer_GetPrivacySettings_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CPlayer_GetPrivacySettings_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 查询自己拥有的游戏
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="appidsFilter">AppIds</param>
        /// <param name="language">Language</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CPlayer_GetOwnedGames_Response>> QueryOwnedGamesAsync(this SteamClient steamClient, IEnumerable<uint>? appidsFilter = null, Language language = Language.Schinese, CancellationToken cancellationToken = default)
        {
            var message = new CPlayer_GetOwnedGames_Request
            {
                steamid = steamClient.SteamId!.Value,
                include_appinfo = true,
                include_extended_appinfo = true,
                include_free_sub = true,
                include_played_free_games = true,
                language = $"{language.GetWebApiCode()}",
                skip_unvetted_apps = false,
                appids_filter = { }
            };
            if (appidsFilter?.Any() ?? false)
            {
                message.appids_filter.AddRange(appidsFilter);
            }
            var task = steamClient.ServiceMethodCallAsync<IPlayer, CPlayer_GetOwnedGames_Response>(c => c.GetOwnedGames(message), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<CPlayer_GetOwnedGames_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CPlayer_GetOwnedGames_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 发送好友消息
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="friendSteamId">好友SteamId</param>
        /// <param name="message">消息体</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CFriendMessages_SendMessage_Response>> SendFrientMessageAsync(this SteamClient steamClient, ulong friendSteamId, string message, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync((IFriendMessages c) => c.SendMessage(new CFriendMessages_SendMessage_Request
            {
                steamid = friendSteamId,
                message = message,

                chat_entry_type = (int)EChatEntryType.ChatMsg,
                contains_bbcode = true,
                client_message_id = $"{Extensions.GetSystemTimestamp()}{RandomNumberGenerator.GetInt32(0, 999999)}",
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<CFriendMessages_SendMessage_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CFriendMessages_SendMessage_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 获取最近好友消息
        /// </summary>
        /// <param name="steamClient">SteamClient</param>
        /// <param name="friendSteamId">好友SteamId</param>
        /// <param name="count">数量</param>
        /// <param name="lastTime">上一页最后一条记录时间</param>
        /// <param name="lastOrdinal">上一页最后一条记录序号</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<ClientResult<CFriendMessages_GetRecentMessages_Response>> QueryRecentFrientMessages(this SteamClient steamClient, ulong friendSteamId, uint count = 0, uint lastTime = 0, uint lastOrdinal = 0, CancellationToken cancellationToken = default)
        {
            var task = steamClient.ServiceMethodCallAsync((IFriendMessages c) => c.GetRecentMessages(new CFriendMessages_GetRecentMessages_Request
            {
                steamid1 = steamClient.SteamId!.Value,
                steamid2 = friendSteamId,
                count = count,
                time_last = lastTime,
                ordinal_last = lastOrdinal
            }), cancellationToken: cancellationToken);

            var result = await task;
            if (result.EResult != EResult.OK)
            {
                return new ClientResult<CFriendMessages_GetRecentMessages_Response>
                {
                    ErrorCode = result.EResult,
                    ErrorMessage = result.ErrorMessage,
                    Result = null,
                };
            }

            return new ClientResult<CFriendMessages_GetRecentMessages_Response>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }
    }
}
