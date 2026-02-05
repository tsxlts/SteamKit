using System.Diagnostics.CodeAnalysis;
using SteamKit.Client.Internal;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public class SteamUserHandler : MessageHandler
    {
        private readonly IDictionary<EMsg, Func<IServerMsg, Task>> msgHandler;

        private readonly MessageAsyncCallbackHandler<CMsgClientSessionToken> clientSessionTokenCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientPlayingSessionState> clientPlayingSessionStateCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientAccountInfo> clientAccountInfoCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientEmailAddrInfo> clientEmailAddrInfoCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientWalletInfoUpdate> clientWalletInfoUpdateCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientUserNotifications> clientUserNotificationCallback;
        private readonly MessageAsyncCallbackHandler<CMsgClientVanityURLChangedNotification> clientVanityURLChangedNotificationCallback;
        private readonly MessageAsyncCallbackHandler<MsgClientMarketingMessageUpdate2> clientMarketingMessageUpdateCallback;

        private readonly object playingSessionLock;
        private readonly List<PlayingSession> playingSessions;

        /// <summary>
        /// 
        /// </summary>
        internal SteamUserHandler() : base()
        {
            msgHandler = new Dictionary<EMsg, Func<IServerMsg, Task>>
            {
                { EMsg.ClientSessionToken, HandleClientSessionToken },
                { EMsg.ClientPlayingSessionState, HandleClientPlayingSessionState },
                { EMsg.ClientAccountInfo, HandleClientAccountInfo },
                { EMsg.ClientEmailAddrInfo, HandleClientEmailAddrInfo },
                { EMsg.ClientWalletInfoUpdate, HandleClientWalletInfoUpdate },
                { EMsg.ClientRequestWebAPIAuthenticateUserNonceResponse, HandleClientRequestWebAPIAuthenticateUserNonceResponse },
                { EMsg.ClientVanityURLChangedNotification, HandleClientVanityURLChangedNotification },
                { EMsg.ClientMarketingMessageUpdate2, HandleClientMarketingMessageUpdate2 },
                { EMsg.ClientUserNotifications, HandleClientUserNotifications }
            };

            clientSessionTokenCallback = new MessageAsyncCallbackHandler<CMsgClientSessionToken>();
            clientPlayingSessionStateCallback = new MessageAsyncCallbackHandler<CMsgClientPlayingSessionState>();
            clientAccountInfoCallback = new MessageAsyncCallbackHandler<CMsgClientAccountInfo>();
            clientWalletInfoUpdateCallback = new MessageAsyncCallbackHandler<CMsgClientWalletInfoUpdate>();
            clientUserNotificationCallback = new MessageAsyncCallbackHandler<CMsgClientUserNotifications>();
            clientEmailAddrInfoCallback = new MessageAsyncCallbackHandler<CMsgClientEmailAddrInfo>();
            clientVanityURLChangedNotificationCallback = new MessageAsyncCallbackHandler<CMsgClientVanityURLChangedNotification>();
            clientMarketingMessageUpdateCallback = new MessageAsyncCallbackHandler<MsgClientMarketingMessageUpdate2>();

            playingSessionLock = new object();
            playingSessions = new List<PlayingSession>();
        }

        /// <summary>
        /// 注册用户会话Token回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientSessionTokenCallback(AsyncEventHandler<CMsgClientSessionToken> callback)
        {
            this.clientSessionTokenCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册进行中的游戏会话状态回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistPlayingSessionStateCallback(AsyncEventHandler<CMsgClientPlayingSessionState> callback)
        {
            this.clientPlayingSessionStateCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册用户帐号信息回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientAccountInfoCallback(AsyncEventHandler<CMsgClientAccountInfo> callback)
        {
            this.clientAccountInfoCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册用户邮箱地址回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientEmailAddrInfoCallback(AsyncEventHandler<CMsgClientEmailAddrInfo> callback)
        {
            this.clientEmailAddrInfoCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册钱包信息变化回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientWalletInfoUpdateCallback(AsyncEventHandler<CMsgClientWalletInfoUpdate> callback)
        {
            this.clientWalletInfoUpdateCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册客户端用户通知事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientUserNotificationsCallback(AsyncEventHandler<CMsgClientUserNotifications> callback)
        {
            this.clientUserNotificationCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册用户自定义个人链接变化回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientVanityURLChangedCallback(AsyncEventHandler<CMsgClientVanityURLChangedNotification> callback)
        {
            this.clientVanityURLChangedNotificationCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册营销信息变化回调事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public SteamUserHandler RegistClientMarketingMessageUpdateCallback(AsyncEventHandler<MsgClientMarketingMessageUpdate2> callback)
        {
            this.clientMarketingMessageUpdateCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 是否正在游戏中
        /// </summary>
        /// <param name="session">正在进行中游戏</param>
        /// <returns></returns>
        public bool HasPlayingSession([NotNullWhen(true)] out PlayingSession? session)
        {
            lock (playingSessionLock)
            {
                if (!playingSessions.Any())
                {
                    session = null;
                    return false;
                }

                session = playingSessions.First();
                return true;
            }
        }

        /// <summary>
        /// 是否正在游戏中
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <returns></returns>
        public bool HasPlayingSession(uint appId)
        {
            lock (playingSessionLock)
            {
                return playingSessions.Any(c => c.AppId == appId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetMsg"></param>
        protected internal override Task HandleMsgAsync(IServerMsg packetMsg)
        {
            if (msgHandler.TryGetValue(packetMsg.MsgType, out var handlerFunc))
            {
                return handlerFunc(packetMsg);
            }

            return Task.CompletedTask;
        }

        private Task HandleClientPlayingSessionState(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientPlayingSessionState>(packetMsg);
            var arg = msg.Body;
            lock (playingSessionLock)
            {
                if (arg.playing_app == 0)
                {
                    playingSessions.Clear();
                }
                else
                {
                    playingSessions.RemoveAll(c => c.AppId == arg.playing_app);
                    playingSessions.Add(new PlayingSession { AppId = arg.playing_app, PlayingBlocked = arg.playing_blocked });
                }
            }

            return this.clientPlayingSessionStateCallback.InvokeAsync(this, arg, Client?.Logger);
        }

        private Task HandleClientSessionToken(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientSessionToken>(packetMsg);
            return this.clientSessionTokenCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientAccountInfo(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientAccountInfo>(packetMsg);
            return this.clientAccountInfoCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientEmailAddrInfo(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientEmailAddrInfo>(packetMsg);
            return this.clientEmailAddrInfoCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientWalletInfoUpdate(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientWalletInfoUpdate>(packetMsg);
            return this.clientWalletInfoUpdateCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientRequestWebAPIAuthenticateUserNonceResponse(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientRequestWebAPIAuthenticateUserNonceResponse>(packetMsg);
            return Task.CompletedTask;
        }

        private Task HandleClientVanityURLChangedNotification(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientVanityURLChangedNotification>(packetMsg);
            return this.clientVanityURLChangedNotificationCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientMarketingMessageUpdate2(IServerMsg packetMsg)
        {
            var msg = new ServerMsg<MsgClientMarketingMessageUpdate2>(packetMsg);
            return this.clientMarketingMessageUpdateCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        private Task HandleClientUserNotifications(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgClientUserNotifications>(packetMsg);
            return this.clientUserNotificationCallback.InvokeAsync(this, msg.Body, Client?.Logger);
        }

        /// <summary>
        /// 
        /// </summary>
        public class PlayingSession
        {
            /// <summary>
            /// AppId
            /// </summary>
            public uint AppId { get; init; }

            /// <summary>
            /// 是否阻塞的
            /// </summary>
            public bool PlayingBlocked { get; init; }
        }
    }
}
