using System.Collections.Concurrent;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC.CS2;
using SteamKit.Factory;
using SteamKit.Internal;

namespace SteamKit.Game.CS2
{
    /// <summary>
    /// CsgoClientManagement
    /// </summary>
    public class CS2ClientManagement
    {
        /// <summary>
        /// 移除客户端事假
        /// </summary>
        /// <param name="arg"></param>
        public delegate void ClientRemovedEventHandler(CS2Client arg);

        private readonly uint gameVersion;
        private readonly uint gameBuildId;
        private readonly Waiter inspectWaiter;
        private readonly ClientConfiguration clientConfiguration;
        private readonly ConcurrentQueue<ClientInfo> csgoClients;
        private readonly ConcurrentDictionary<EMsg, AsyncEventHandler<MessageCallback>> messageCallbacks;

        private ILogger logger;
        private ClientRemovedEventHandler? clientRemoved;
        private ProtocolTypes protocol = ProtocolTypes.WebSocket;
        private uint cellId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">
        /// 游戏版本
        /// /game/csgo/steam.inf
        /// </param>
        /// <param name="buildId">
        /// 生成版本Id
        /// Steam目录/steamapps/appmanifest_*.acf
        /// </param>
        public CS2ClientManagement(uint version, uint buildId)
        {
            gameVersion = version;
            gameBuildId = buildId;
            inspectWaiter = new Waiter();
            clientConfiguration = ClientConfiguration.Default;
            csgoClients = new ConcurrentQueue<ClientInfo>();
            messageCallbacks = new ConcurrentDictionary<EMsg, AsyncEventHandler<MessageCallback>>();

            logger = new DefaultLogger();
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="authenticator">令牌验证器</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CS2Client?> AddClientAsync(string userName, string password, IAuthenticator authenticator, CancellationToken cancellationToken = default)
        {
            return await AddClientAsync(csgoClient => csgoClient.WithUser(userName, password, authenticator), cancellationToken);
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="refreshToken">登录授权Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CS2Client?> AddClientAsync(ulong steamId, string refreshToken, CancellationToken cancellationToken = default)
        {
            return await AddClientAsync(csgoClient => csgoClient.WithRefreshToken(steamId, refreshToken), cancellationToken);
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="steamId">SteamId</param>
        /// <param name="webLogonToken">Web登录授权Token</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CS2Client?> AddClientAsync(string userName, ulong steamId, string webLogonToken, CancellationToken cancellationToken = default)
        {
            return await AddClientAsync(csgoClient => csgoClient.WithWebToken(userName, steamId, webLogonToken), cancellationToken);
        }

        /// <summary>
        /// 检视
        /// </summary>
        /// <param name="inspectLink">检视链接</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CEconItemPreviewDataBlock?> InspectAsync(string inspectLink, CancellationToken cancellationToken = default)
        {
            Extensions.GetInspectParameter(inspectLink,
                param_s: out var param_s,
                param_m: out var param_m,
                param_a: out var param_a,
                param_d: out var param_d);

            return await InspectAsync(s: param_s, m: param_m, a: param_a, d: param_d, cancellationToken);
        }

        /// <summary>
        /// 检视
        /// </summary>
        /// <param name="s">
        /// 检视参数S
        /// <para>SteamId</para>
        /// <para>与参数M不可同时为空</para>
        /// </param>
        /// <param name="m">
        /// 检视参数M
        /// <para>市场商品Id</para>
        /// <para>与参数S不可同时为空</para>
        /// </param>
        /// <param name="a">
        /// 检视参数A
        /// <para>资产Id</para>
        /// </param>
        /// <param name="d">
        /// 检视参数D
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CEconItemPreviewDataBlock?> InspectAsync(ulong s, ulong m, ulong a, ulong d, CancellationToken cancellationToken = default)
        {
            ClientInfo? client;
            while (true)
            {
                if (!csgoClients.TryDequeue(out client))
                {
                    return null;
                }
                if (!client.Client!.GameLoaded)
                {
                    clientRemoved?.Invoke(client.Client);
                    continue;
                }
                break;
            }

            try
            {
                using (client.InspectWaitToken!)
                {
                    await inspectWaiter.WaitAsync(client.InspectWaitToken!.Token);

                    var result = await client.Client.InspectAsync(s: s, m: m, a: a, d: d, cancellationToken);
                    return result;
                }
            }
            finally
            {
                client.InspectWaitToken = new CancellationTokenSource(clientConfiguration.InspectInterval);
                csgoClients.Enqueue(client);
            }
        }

        /// <summary>
        /// 绑定移除客户端事件
        /// </summary>
        /// <param name="removed">Action</param>
        public CS2ClientManagement OnClientRemoved(ClientRemovedEventHandler removed)
        {
            clientRemoved = removed;
            return this;
        }

        /// <summary>
        /// 设置Logger
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns></returns>
        public CS2ClientManagement WithLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        /// <summary>
        /// 设置客户端配置信息
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns></returns>
        public CS2ClientManagement WithConfiguration(Action<ClientConfiguration> action)
        {
            action.Invoke(clientConfiguration);
            return this;
        }

        /// <summary>
        /// 设置通讯协议
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public CS2ClientManagement WithProtocol(ProtocolTypes protocol)
        {
            this.protocol = protocol;
            return this;
        }

        /// <summary>
        /// 设置区域
        /// </summary>
        /// <param name="cellId">cellId</param>
        /// <returns></returns>
        public CS2ClientManagement WithCellId(uint cellId)
        {
            this.cellId = cellId;
            return this;
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public CS2ClientManagement RegistCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            messageCallbacks.AddOrUpdate(msgType, key => callback, (key, value) =>
            {
                value += callback;
                return value;
            });

            return this;
        }

        /// <summary>
        /// 空闲的客户端个数
        /// </summary>
        public int IdleClientCount => csgoClients.Count(c => c.Client!.GameLoaded);

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="connectAction">客户端连接初始化</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        private async Task<CS2Client?> AddClientAsync(Action<CS2Client> connectAction, CancellationToken cancellationToken = default)
        {
            CS2Client csgoClient = new CS2Client(gameVersion, gameBuildId);
            csgoClient.WithProtocol(protocol);
            csgoClient.WithCellId(cellId);
            csgoClient.WithLogger(logger);
            csgoClient.Configure(options => options.KickPlayingSession = clientConfiguration.KickPlayingSession);

            connectAction.Invoke(csgoClient);

            foreach (var callback in messageCallbacks)
            {
                csgoClient.RegistCallback(callback.Key, callback.Value);
            }

            var result = await csgoClient.ConnectAsync(cancellationToken);
            if (!result.Result)
            {
                return null;
            }

            var loginGame = await csgoClient.LoginGameAsync(cancellationToken);
            if (!loginGame.Success)
            {
                return null;
            }

            csgoClients.Enqueue(new ClientInfo { Client = csgoClient, InspectWaitToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(10)) });
            return csgoClient;
        }

        /// <summary>
        /// 客户端配置
        /// </summary>
        public class ClientConfiguration
        {
            private ClientConfiguration()
            {

            }

            /// <summary>
            /// 是否踢掉正在进行的游戏会话
            /// </summary>
            internal bool KickPlayingSession { get; set; } = false;

            /// <summary>
            /// 检视间隔时间
            /// </summary>
            internal TimeSpan InspectInterval { get; set; } = TimeSpan.FromMilliseconds(1000);

            /// <summary>
            /// 设置是否踢掉正在进行有游戏会话
            /// <para></para>
            /// </summary>
            /// <param name="kickPlayingSession">
            /// 是否踢掉正在进行有游戏会话
            /// <para>true: 已登录的连接将被强制下线</para>
            /// <para>false: 如果已在其他地方登录游戏, 则无法登陆游戏</para>
            /// <para>默认为 false</para>
            /// </param>
            /// <returns></returns>
            public ClientConfiguration WithKickPlayingSession(bool kickPlayingSession)
            {
                KickPlayingSession = kickPlayingSession;
                return this;
            }

            /// <summary>
            /// 设置检视间隔时间
            /// </summary>
            /// <param name="interval">间隔时间</param>
            /// <returns></returns>
            public ClientConfiguration WithInspectInterval(TimeSpan interval)
            {
                InspectInterval = interval;

                return this;
            }

            /// <summary>
            /// 默认配置
            /// </summary>
            public static ClientConfiguration Default = new ClientConfiguration
            {
                KickPlayingSession = false,
                InspectInterval = TimeSpan.FromMilliseconds(1000)
            };
        }

        private class ClientInfo
        {
            public CS2Client? Client { get; set; }

            public CancellationTokenSource? InspectWaitToken { get; set; }
        }
    }
}
