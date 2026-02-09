using System.Linq.Expressions;
using System.Net;
using System.Text;
using ProtoBuf;
using SteamKit.Client.Hanlders;
using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.Proto;
using SteamKit.Client.Options;
using SteamKit.Exceptions;
using SteamKit.Factory;
using SteamKit.Model;
using static SteamKit.Enums;

namespace SteamKit.Client
{
    /// <summary>
    /// SteamClient
    /// </summary>
    public class SteamClient : IDisposable
    {
        private readonly ClientConfiguration configuration;
        private readonly LogonOptions logonOptions;
        private readonly BaseClient baseClient;
        private readonly MessageAsyncCallbackHandler<ServerProtoBufMsg<CMsgClientLogonResponse>> logonCallback;
        private readonly MessageAsyncCallbackHandler<ServerProtoBufMsg<CMsgClientLoggedOff>> logoffCallback;
        private readonly MessageAsyncCallbackHandler<DisconnectedEventArgs> disconnectedCallback;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SteamClient()
        {
            configuration = ClientConfiguration.Default;
            logonOptions = LogonOptions.Default;
            baseClient = new BaseClient(ProtocolTypes.WebSocket);

            logonCallback = new MessageAsyncCallbackHandler<ServerProtoBufMsg<CMsgClientLogonResponse>>();
            logoffCallback = new MessageAsyncCallbackHandler<ServerProtoBufMsg<CMsgClientLoggedOff>>();
            disconnectedCallback = new MessageAsyncCallbackHandler<DisconnectedEventArgs>();

            AddHandler(new SteamGameCoordinator());
            AddHandler(new SteamAuthTicketHandler());
            AddHandler(new ServiceMessageHandler());
            AddHandler(new SteamUserHandler());
            AddHandler(new AuthenticatorHandler());
            AddHandler(new MicroTxnHandler());
        }

        /// <summary>
        /// 匿名登录
        /// </summary>
        /// <returns></returns>
        public SteamClient WithAnonymous()
        {
            configuration.LoginType = LoginType.Anonymous;
            return this;
        }

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="drawQRCode">绘制二维码</param>
        /// <returns></returns>
        public SteamClient WithQrCode(Action<BeginQrAuthResult> drawQRCode)
        {
            configuration.LoginType = LoginType.QrCode;
            configuration.DrawQRCode = drawQRCode;
            return this;
        }

        /// <summary>
        /// 用户帐号密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="authenticator">令牌验证器</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public SteamClient WithUser(string userName, string password, IAuthenticator authenticator)
        {
            configuration.LoginType = LoginType.UserName;
            configuration.UserName = userName;
            configuration.Password = password;
            configuration.Authenticator = authenticator;

            return this;
        }

        /// <summary>
        /// RefreshToken登录
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="refreshToken">RefreshToken</param>
        /// <returns></returns>
        public SteamClient WithRefreshToken(ulong steamId, string refreshToken)
        {
            configuration.LoginType = LoginType.RefreshToken;
            configuration.SteamId = steamId;
            configuration.RefreshToken = refreshToken;

            return this;
        }

        /// <summary>
        /// 网页Token登录
        /// </summary>
        /// <param name="userName">登录用户名</param>
        /// <param name="steamId">登录用户SteamId</param>
        /// <param name="webLogonToken">网页登录Token</param>
        /// <returns></returns>
        public SteamClient WithWebToken(string userName, ulong steamId, string webLogonToken)
        {
            configuration.LoginType = LoginType.WebToken;
            configuration.UserName = userName;
            configuration.SteamId = steamId;
            configuration.WebLogonToken = webLogonToken;

            return this;
        }

        /// <summary>
        /// 游戏服务器登录
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="token">登录令牌</param>
        /// <returns></returns>
        public SteamClient WithGameServer(uint appId, string token)
        {
            configuration.LoginType = LoginType.GameServer;
            configuration.AppId = (int)appId;
            configuration.Password = token;

            return this;
        }

        /// <summary>
        /// 设置Logger
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns></returns>
        public SteamClient WithLogger(ILogger logger)
        {
            baseClient.WithLogger(logger);

            return this;
        }

        /// <summary>
        /// 设置通讯协议
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public SteamClient WithProtocol(ProtocolTypes protocol)
        {
            baseClient.WithProtocol(protocol);
            return this;
        }

        /// <summary>
        /// 设置Steam服务工厂
        /// </summary>
        /// <param name="serverProvider"></param>
        /// <returns></returns>
        public SteamClient WithServerProvider(IServerProvider serverProvider)
        {
            baseClient.WithServerProvider(serverProvider);
            return this;
        }

        /// <summary>
        /// 设置Socket工厂
        /// </summary>
        /// <param name="socketProvider"></param>
        /// <returns></returns>
        public SteamClient WithSocketProvider(ISocketProvider socketProvider)
        {
            baseClient.WithSocketProvider(socketProvider);
            return this;
        }

        /// <summary>
        /// 设置客户端语言
        /// </summary>
        /// <param name="language">客户端语言</param>
        /// <returns></returns>
        public SteamClient WithClientLanguage(string language)
        {
            baseClient.WithClientLanguage(language);
            return this;
        }

        /// <summary>
        /// 设置区域
        /// </summary>
        /// <param name="cellId">cellId</param>
        /// <returns></returns>
        public SteamClient WithCellId(uint cellId)
        {
            baseClient.WithCellId(cellId);
            return this;
        }

        /// <summary>
        /// 设置登录参数
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SteamClient ConfigureOptions(Action<LogonOptions> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            action.Invoke(logonOptions);
            return this;
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public SteamClient RegistCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            baseClient.RegistCallback(msgType, callback);
            return this;
        }

        /// <summary>
        /// 移除消息回调事件
        /// </summary>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public SteamClient RemoveCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            baseClient.RemoveCallback(msgType, callback);
            return this;
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <typeparam name="MsgType">MsgType</typeparam>
        /// <param name="appId">AppId</param>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public SteamClient RegistGCCallback<MsgType>(uint appId, MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            var gcHandler = GetHandler<SteamGameCoordinator>();
            gcHandler.RegistCallback(appId, msgType, callback);
            return this;
        }

        /// <summary>
        /// 移除消息回调事件
        /// </summary>
        /// <typeparam name="MsgType">MsgType</typeparam>
        /// <param name="appId">AppId</param>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public SteamClient RemoveGCCallback<MsgType>(uint appId, MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            var gcHandler = GetHandler<SteamGameCoordinator>();
            gcHandler.RemoveCallback(appId, msgType, callback);
            return this;
        }

        /// <summary>
        /// 注册登录消息回调事件
        /// </summary>
        /// <param name="logon"></param>
        /// <returns></returns>
        public SteamClient WithLogon(AsyncEventHandler<ServerProtoBufMsg<CMsgClientLogonResponse>> logon)
        {
            logonCallback.SetCallback(logon);
            return this;
        }

        /// <summary>
        /// 注册用户退出登录事件
        /// </summary>
        /// <param name="logoff"></param>
        /// <returns></returns>
        public SteamClient WithLogoff(AsyncEventHandler<ServerProtoBufMsg<CMsgClientLoggedOff>> logoff)
        {
            logoffCallback.SetCallback(logoff);
            return this;
        }

        /// <summary>
        /// 注册连接断开事件
        /// </summary>
        /// <param name="disconnected"></param>
        /// <returns></returns>
        public SteamClient WithDisconnected(AsyncEventHandler<DisconnectedEventArgs> disconnected)
        {
            disconnectedCallback.SetCallback(disconnected);
            return this;
        }

        /// <summary>
        /// 连接并登录服务器
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public virtual async Task<ClientResult<bool>> ConnectAsync(CancellationToken cancellationToken = default)
        {
            return await ConnectAsync(null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 连接并登录服务器
        /// </summary>
        /// <param name="endPoint">服务器节点</param>
        /// <param name="protocol">ProtocolType</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public virtual async Task<ClientResult<bool>> ConnectAsync(EndPoint? endPoint, ProtocolTypes? protocol, CancellationToken cancellationToken = default)
        {
            await DisconnectAsync().ConfigureAwait(false);

            TaskCompletionSource<ClientResult<bool>> tcs = new TaskCompletionSource<ClientResult<bool>>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (CancellationTokenRegistration tokenRegistration = cancellationToken.Register(async () => { tcs.TrySetCanceled(); await DisconnectAsync().ConfigureAwait(false); }))
            {
                baseClient.WithConnected((sender, args) =>
                {
                    try
                    {
                        Logger?.LogDebug("Waiting for login ...");
                        Logger?.LogInformation("Waiting for login ...");

                        Login(cancellationToken).GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).WithDisconnected((sender, args) =>
                {
                    disconnectedCallback.Invoke(sender, args, Logger);
                }).WithLogon((sender, logonResponse) =>
                {
                    var eResult = (EResult)logonResponse.Body.eresult;

                    tcs.TrySetResult(new ClientResult<bool>
                    {
                        ErrorCode = eResult,
                        Result = eResult == EResult.OK,
                        ErrorMessage = logonResponse.Header.Proto.error_message
                    });

                    logonCallback.Invoke(sender, logonResponse, Logger);
                }).WithLogoff((sender, loggedOffResponse) =>
                {
                    logoffCallback.Invoke(sender, loggedOffResponse, Logger);
                });

                await InternalConnectAsync(endPoint, protocol, cancellationToken).ConfigureAwait(false);

                try
                {
                    return await tcs.Task.ConfigureAwait(false);
                }
                catch (AuthException authException)
                {
                    Logger?.LogDebug("Login failed: {0} ({1})", authException.Error, authException.EResult);
                    Logger?.LogError("Login failed: {0} ({1})", authException.Error, authException.EResult);
                    Logger?.LogInformation("Login failed: {0} ({1})", authException.Error, authException.EResult);

                    return new ClientResult<bool>
                    {
                        ErrorCode = authException.EResult,
                        ErrorMessage = authException.Error,
                        Result = false
                    };
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public virtual async Task LogoffAsync()
        {
            await baseClient.LogoffAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public virtual async Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            await baseClient.DisconnectAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">Msg</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task SendAsync(IClientMsg msg, CancellationToken cancellationToken = default)
        {
            await baseClient.SendAsync(msg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送GC消息
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="gcMsg">gcMsg</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task SendAsync(uint appId, IGCClientMsg gcMsg, CancellationToken cancellationToken = default)
        {
            var gcHandler = GetHandler<SteamGameCoordinator>();
            await gcHandler.SendAsync(appId, gcMsg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(IClientMsg msg)
        {
            baseClient.Send(msg);
        }

        /// <summary>
        /// 发送GC消息
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="gcMsg">gcMsg</param>
        public void Send(uint appId, IGCClientMsg gcMsg)
        {
            var gcHandler = GetHandler<SteamGameCoordinator>();
            gcHandler.Send(appId, gcMsg);
        }

        /// <summary>
        /// 发送异步消息
        /// </summary>
        /// <typeparam name="TRequest">TRequest</typeparam>
        /// <typeparam name="TResponse">TResponse</typeparam>
        /// <param name="msgType">MsgType</param>
        /// <param name="message">Message</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<JobResult<TResponse>> SendAsync<TRequest, TResponse>(EMsg msgType, TRequest message, CancellationToken cancellationToken = default) where TRequest : IExtensible, new() where TResponse : IExtensible, new()
        {
            return await baseClient.SendAsync<TRequest, TResponse>(msgType, message, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 调用服务端方法
        /// </summary>
        /// <typeparam name="TServer">TServer</typeparam>
        /// <typeparam name="TResponse">TResponse</typeparam>
        /// <param name="targetServer">ServerMethod</param>
        /// <param name="version">Version</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<JobResult<TResponse>> ServiceMethodCallAsync<TServer, TResponse>(Expression<Func<TServer, TResponse>> targetServer, uint version = 1, CancellationToken cancellationToken = default) where TServer : class where TResponse : IExtensible, new()
        {
            return await baseClient.ServiceMethodCallAsync(targetServer, version, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 调用服务端方法
        /// </summary>
        /// <typeparam name="TRequest">TRequest</typeparam>
        /// <typeparam name="TResponse">TResponse</typeparam>
        /// <param name="targetServerName">ServerName</param>
        /// <param name="targetMethodName">MethodName</param>
        /// <param name="message">Message</param>
        /// <param name="version">Version</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<JobResult<TResponse>> ServiceMethodCallAsync<TRequest, TResponse>(string targetServerName, string targetMethodName, TRequest message, uint version = 1, CancellationToken cancellationToken = default) where TRequest : IExtensible, new() where TResponse : IExtensible, new()
        {
            return await baseClient.ServiceMethodCallAsync<TRequest, TResponse>(targetServerName, targetMethodName, message, version, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 调用服务端通知
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="targetServerName"></param>
        /// <param name="targetMethodName"></param>
        /// <param name="message"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ServiceNotificationCallAsync<TRequest>(string targetServerName, string targetMethodName, TRequest message, uint version = 1, CancellationToken cancellationToken = default) where TRequest : IExtensible, new()
        {
            await baseClient.ServiceNotificationCallAsync(targetServerName, targetMethodName, message, version, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 注册消息处理器
        /// </summary>
        /// <param name="handler">handler</param>
        public void AddHandler(MessageHandler handler)
        {
            handler.Setup(this);
            baseClient.AddHandler(handler);
        }

        /// <summary>
        /// 移除消息处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool RemoveHandler<T>() where T : MessageHandler
        {
            return baseClient.RemoveHandler<T>();
        }

        /// <summary>
        /// 获取消息处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetHandler<T>() where T : MessageHandler
        {
            return baseClient.GetHandler<T>();
        }

        /// <summary>
        /// 获取或者注册消息处理器
        /// <para>如果已注册则返回已注册的消息处理器</para>
        /// <para>如果未注册则注册消息处理器并返回该消息处理器</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addHanderFactory"></param>
        /// <returns></returns>
        public T GetOrAddHandler<T>(Func<SteamClient, T> addHanderFactory) where T : MessageHandler
        {
            if (baseClient.TryGetHandler<T>(out var handler))
            {
                return handler;
            }

            handler = addHanderFactory.Invoke(this);
            AddHandler(handler);

            return handler;
        }

        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected()
        {
            return baseClient.IsConnected() && baseClient.IsLogged();
        }

        /// <summary>
        /// 获取任务Id
        /// </summary>
        /// <returns></returns>
        public ulong GetJobId()
        {
            return baseClient.GetAsyncJobId();
        }

        /// <summary>
        /// SteamId
        /// </summary>
        public ulong? SteamId => baseClient.SteamId;

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string? RefreshToken { get; private set; }

        /// <summary>
        /// CellId
        /// </summary>
        public uint? CellId => baseClient.CellId;

        /// <summary>
        /// 用户Ip所在国家
        /// </summary>
        public string? IPCountryCode => baseClient.IPCountryCode;

        /// <summary>
        /// 用户所在Ip
        /// </summary>
        public IPAddress? ClientIP => baseClient.PublicIP;

        /// <summary>
        /// 服务节点
        /// </summary>
        public EndPoint? EndPoint => baseClient.EndPoint;

        /// <summary>
        /// 服务连接协议
        /// </summary>
        public ProtocolTypes? Protocol => baseClient?.Protocol;

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger? Logger => baseClient.Logger;

        /// <summary>
        /// MachineName
        /// </summary>
        public string MachineName => baseClient.MachineName;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            baseClient.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SteamClient#");
            if (!IsConnected())
            {
                stringBuilder.AppendLine("Anonymous");
                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine($"LoginType:{configuration.LoginType}");
            stringBuilder.AppendLine($"SteamId:{SteamId}");
            return stringBuilder.ToString();
        }

        private async Task InternalConnectAsync(EndPoint? endPoint, ProtocolTypes? protocol, CancellationToken cancellationToken = default)
        {
            if (endPoint != null && protocol != null)
            {
                await baseClient.ConnectAsync(endPoint, protocol.Value, cancellationToken).ConfigureAwait(false);
                return;
            }

            await baseClient.ConnectAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task Login(CancellationToken cancellationToken)
        {
            configuration.CheckUser();
            switch (configuration.LoginType)
            {
                case LoginType.Anonymous:
                    await baseClient.LogonAnonymousAsync().ConfigureAwait(false);
                    break;

                case LoginType.UserName:
                    await UserAuth(cancellationToken);
                    break;

                case LoginType.QrCode:
                    await QrCodeAuth(cancellationToken);
                    break;

                case LoginType.RefreshToken:
                    RefreshToken = configuration.RefreshToken;
                    await Logon(userName: null, steamId: configuration.SteamId, refreshToken: configuration.RefreshToken!,
                        qosLevel: configuration.QosLevel,
                        cancellationToken: cancellationToken).ConfigureAwait(false);
                    break;

                case LoginType.WebToken:
                    await baseClient.WebTokenLogonAsync(configuration.UserName!, configuration.SteamId, configuration.WebLogonToken!).ConfigureAwait(false);
                    break;

                case LoginType.GameServer:
                    await baseClient.LogonGameAsync(configuration.AppId, configuration.Password!).ConfigureAwait(false);
                    break;

                default:
                    throw new NotImplementedException($"未实现的登录方式: {configuration.LoginType}");
            }
        }

        private async Task UserAuth(CancellationToken cancellationToken)
        {
            var beginAuth = await baseClient.AuthTokenViaCredentialsAsync(userName: configuration.UserName!, password: configuration.Password!,
                guardData: null,
                websiteId: configuration.WebsiteId,
                platformType: configuration.Platform,
                appType: configuration.AppType,
                qosLevel: (int)configuration.QosLevel,
                cancellationToken: cancellationToken).ConfigureAwait(false);
            if (beginAuth == null || beginAuth.SteamId == 0)
            {
                throw new AuthException("Steam授权失败")
                {
                    EResult = EResult.Fail,
                    Error = "Steam授权失败"
                };
            }

            AuthTokenResult authTokenResult;
            GuardConfirmationResult confirmationResult;
            EResult confirmLoginResult;
            int maxRetry = 3;
            do
            {
                authTokenResult = await beginAuth.PollingAuthTokenAsync(cancellationToken);
                if (!string.IsNullOrWhiteSpace(authTokenResult.AccountName) && !string.IsNullOrWhiteSpace(authTokenResult.RefreshToken))
                {
                    break;
                }

                if (authTokenResult.EResult != EResult.OK)
                {
                    throw new AuthException("登录失败")
                    {
                        EResult = authTokenResult?.EResult ?? EResult.Fail,
                        Error = "登录失败"
                    };
                }

                confirmationResult = await configuration.Authenticator!.GuardConfirmationAsync(beginAuth.AllowedConfirmations.Select(c => c.confirmation_type), cancellationToken).ConfigureAwait(false);
                if (!confirmationResult.Confirmed)
                {
                    throw new OperationCanceledException("已取消登录");
                }

                maxRetry--;
                confirmLoginResult = await beginAuth.ConfirmLoginAsync(confirmationResult.ConfirmationType, confirmationResult.GuardCode, cancellationToken).ConfigureAwait(false);
                if (confirmLoginResult == EResult.OK)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    continue;
                }

                if (maxRetry <= 0)
                {
                    throw new AuthException("令牌验证失败")
                    {
                        EResult = confirmLoginResult,
                        Error = "令牌验证失败"
                    };
                }

                await beginAuth.WaitPollingAsync(cancellationToken).ConfigureAwait(false);
            } while (maxRetry > 0);

            if (string.IsNullOrWhiteSpace(authTokenResult.AccountName) || string.IsNullOrWhiteSpace(authTokenResult.RefreshToken))
            {
                throw new AuthException("登录失败")
                {
                    EResult = authTokenResult.EResult != EResult.OK ? authTokenResult.EResult : EResult.Invalid,
                    Error = "登录失败"
                };
            }

            //AccessToken = authTokenResult.AccessToken;
            RefreshToken = authTokenResult.RefreshToken;

            await Logon(userName: null, steamId: beginAuth.SteamId, refreshToken: authTokenResult.RefreshToken!,
                qosLevel: configuration.QosLevel,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        private async Task QrCodeAuth(CancellationToken cancellationToken)
        {
            var qrBeginAuth = await baseClient.AuthTokenViaQrAsync(websiteId: configuration.WebsiteId,
                platformType: configuration.Platform,
                appType: configuration.AppType,
                cancellationToken: cancellationToken).ConfigureAwait(false);
            if (qrBeginAuth == null || qrBeginAuth.ClientId == 0)
            {
                throw new AuthException("Steam授权失败")
                {
                    EResult = EResult.Fail,
                    Error = "Steam授权失败"
                };
            }
            configuration.DrawQRCode?.Invoke(qrBeginAuth);
            var authTokenResult = await qrBeginAuth.WithQrCode(() => configuration.DrawQRCode?.Invoke(qrBeginAuth)).WithUrlChanged(() => configuration.DrawQRCode?.Invoke(qrBeginAuth)).PollingAuthTokenAsync(cancellationToken).ConfigureAwait(false);

            //AccessToken = authTokenResult.AccessToken;
            RefreshToken = authTokenResult.RefreshToken;

            await Logon(userName: null, steamId: null, refreshToken: authTokenResult.RefreshToken!,
                qosLevel: configuration.QosLevel,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        private async Task Logon(string? userName, ulong? steamId, string refreshToken, uint qosLevel, CancellationToken cancellationToken = default)
        {
            await baseClient.LogonAsync(new LogonParameter
            {
                QosLevel = qosLevel,

                UserName = userName,
                SteamId = steamId ?? 0,
                AccessToken = refreshToken,

                ChatMode = logonOptions.ChatMode,
                GamingDeviceType = logonOptions.GamingDeviceType,
                OSType = logonOptions.OSType,
                UIMode = logonOptions.UIMode,
            }, cancellationToken).ConfigureAwait(false);
        }

        private class ClientConfiguration
        {
            public static ClientConfiguration Default => new ClientConfiguration
            {
                LoginType = LoginType.Anonymous,
                UserName = string.Empty,
                Password = string.Empty,
                WebLogonToken = string.Empty,
                Authenticator = new DefaultAuthenticator(),

                DrawQRCode = (c) => { },

                QosLevel = 2,
            };

            private ClientConfiguration()
            {
            }

            public void CheckUser()
            {
                if (LoginType == LoginType.Anonymous || LoginType == LoginType.QrCode)
                {
                    return;
                }

                if (LoginType == LoginType.RefreshToken)
                {
                    if (SteamId <= 0)
                    {
                        throw new ArgumentNullException(nameof(UserName), "登录SteamId不能为空");
                    }

                    if (string.IsNullOrWhiteSpace(RefreshToken))
                    {
                        throw new ArgumentNullException(nameof(RefreshToken), "RefreshToken不能为空");
                    }
                    return;
                }

                if (LoginType == LoginType.WebToken)
                {
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        throw new ArgumentNullException(nameof(UserName), "登录用户名不能为空");
                    }
                    if (SteamId <= 0)
                    {
                        throw new ArgumentNullException(nameof(SteamId), "登录SteamId不能为空");
                    }
                    if (string.IsNullOrWhiteSpace(WebLogonToken))
                    {
                        throw new ArgumentNullException(nameof(WebLogonToken), "网页登录Token不能为空");
                    }
                    return;
                }

                if (LoginType == LoginType.GameServer)
                {
                    if (AppId <= 0)
                    {
                        throw new ArgumentNullException(nameof(AppId), "AppId不能为空");
                    }
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        throw new ArgumentNullException(nameof(Password), "登录令牌不能为空");
                    }
                    return;
                }

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    throw new ArgumentNullException(nameof(UserName), "登录用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new ArgumentNullException(nameof(Password), "登录密码不能为空");
                }
                if (Authenticator == null)
                {
                    throw new ArgumentNullException(nameof(Authenticator), "AuthGuardFactory不能为空");
                }
            }

            public LoginType LoginType { get; set; } = LoginType.Anonymous;

            public EAuthTokenPlatformType Platform => EAuthTokenPlatformType.k_EAuthTokenPlatformType_SteamClient;

            public EAuthTokenAppType AppType => EAuthTokenAppType.k_EAuthTokenAppType_Unknown;

            public uint QosLevel { get; set; }

            public string? UserName { get; set; }

            public string? Password { get; set; }

            public ulong SteamId { get; set; }

            public string? WebLogonToken { get; set; }

            public string? RefreshToken { get; set; }

            public int AppId { get; set; }

            public IAuthenticator? Authenticator { get; set; }

            public Action<BeginQrAuthResult>? DrawQRCode { get; set; }

            /// <summary>
            /// Unknown
            /// Client
            /// Mobile
            /// Website
            /// Store
            /// Community
            /// Partner
            /// SteamStats
            /// </summary>
            public string WebsiteId
            {
                get
                {
                    switch (Platform)
                    {
                        case EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown:
                            return "Unknown";

                        case EAuthTokenPlatformType.k_EAuthTokenPlatformType_SteamClient:
                            return "Client";

                        case EAuthTokenPlatformType.k_EAuthTokenPlatformType_WebBrowser:
                            return "Website";

                        case EAuthTokenPlatformType.k_EAuthTokenPlatformType_MobileApp:
                            return "Mobile";
                    }

                    return "Mobile";
                }
            }
        }

        /// <summary>
        /// 登录方式
        /// </summary>
        private enum LoginType
        {
            /// <summary>
            /// 匿名登录
            /// </summary>
            Anonymous = 0,

            /// <summary>
            /// 用户帐号密码登录
            /// </summary>
            UserName = 101,

            /// <summary>
            /// 扫码授权登录
            /// </summary>
            QrCode = 102,

            /// <summary>
            /// RefreshToken登录
            /// </summary>
            RefreshToken = 202,

            /// <summary>
            /// 网页登录Token登录
            /// </summary>
            WebToken = 301,

            /// <summary>
            /// 游戏服务器登录
            /// </summary>
            GameServer = 901
        }
    }
}
