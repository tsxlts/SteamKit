using System.Linq.Expressions;
using System.Net;
using ProtoBuf;
using SteamKit.Client.Internal;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Proto;
using SteamKit.Client.Options;
using SteamKit.Exceptions;
using SteamKit.Factory;

namespace SteamKit.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class SlimClient : IDisposable
    {
        private readonly LogonOptions logonOptions;

        private readonly BaseClient baseClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SlimClient()
        {
            logonOptions = LogonOptions.Default;
            baseClient = new BaseClient(ProtocolTypes.WebSocket);
        }

        /// <summary>
        /// 设置登录参数
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SlimClient ConfigureOptions(Action<LogonOptions> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            action.Invoke(logonOptions);
            return this;
        }

        /// <summary>
        /// 设置Logger
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns></returns>
        public SlimClient WithLogger(ILogger logger)
        {
            baseClient.WithLogger(logger);

            return this;
        }

        /// <summary>
        /// 设置通讯协议
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public SlimClient WithProtocol(ProtocolTypes protocol)
        {
            baseClient.WithProtocol(protocol);
            return this;
        }

        /// <summary>
        /// 设置Steam服务工厂
        /// </summary>
        /// <param name="serverProvider"></param>
        /// <returns></returns>
        public SlimClient WithServerProvider(IServerProvider serverProvider)
        {
            baseClient.WithServerProvider(serverProvider);
            return this;
        }

        /// <summary>
        /// 设置Socket工厂
        /// </summary>
        /// <param name="socketProvider"></param>
        /// <returns></returns>
        public SlimClient WithSocketProvider(ISocketProvider socketProvider)
        {
            baseClient.WithSocketProvider(socketProvider);
            return this;
        }

        /// <summary>
        /// 设置客户端语言
        /// </summary>
        /// <param name="language">客户端语言</param>
        /// <returns></returns>
        public SlimClient WithClientLanguage(string language)
        {
            baseClient.WithClientLanguage(language);
            return this;
        }

        /// <summary>
        /// 设置区域
        /// </summary>
        /// <param name="cellId">cellId</param>
        /// <returns></returns>
        public SlimClient WithCellId(uint cellId)
        {
            baseClient.WithCellId(cellId);
            return this;
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public SlimClient RegistCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            baseClient.RegistCallback(msgType, callback);
            return this;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public virtual async Task<bool> ConnectAsync(CancellationToken cancellationToken = default)
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
        public virtual async Task<bool> ConnectAsync(EndPoint? endPoint, ProtocolTypes? protocol, CancellationToken cancellationToken = default)
        {
            await DisconnectAsync().ConfigureAwait(false);

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (CancellationTokenRegistration tokenRegistration = cancellationToken.Register(async () => { tcs.TrySetCanceled(); await DisconnectAsync().ConfigureAwait(false); }))
            {
                baseClient.WithConnected((sender, args) =>
                {
                    try
                    {
                        tcs.TrySetResult(true);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).WithDisconnected((sender, args) =>
                {
                    tcs.TrySetResult(false);
                });

                await InternalConnectAsync(endPoint, protocol, cancellationToken).ConfigureAwait(false);

                using (var timeoutToken = new CancellationTokenSource(60 * 1000))
                {
                    return await tcs.Task.WaitAsync(timeoutToken.Token).ConfigureAwait(false);
                }
            }
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
        /// 帐号密码授权登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="guardData"></param>
        /// <param name="platformType">登录平台</param>
        /// <param name="appType">App类型</param>
        /// <param name="qosLevel"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <exception cref="AuthException"></exception>
        public async Task<BeginCredentialsAuthResult> AuthTokenViaCredentialsAsync(string userName, string password, string? guardData, EAuthTokenPlatformType platformType, EAuthTokenAppType appType, int qosLevel = 2, CancellationToken cancellationToken = default)
        {
            string websiteId = ConvertWebsiteId(platformType);

            return await baseClient.AuthTokenViaCredentialsAsync(userName: userName, password: password, guardData: guardData, websiteId, platformType, appType, qosLevel, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 扫码授权登录
        /// </summary>
        /// <param name="platformType">登录平台</param>
        /// <param name="appType">App类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">CancellationToken</exception>
        public async Task<BeginQrAuthResult> AuthTokenViaQrAsync(EAuthTokenPlatformType platformType, EAuthTokenAppType appType, CancellationToken cancellationToken = default)
        {
            string websiteId = ConvertWebsiteId(platformType);

            return await baseClient.AuthTokenViaQrAsync(websiteId, platformType, appType, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgClientLogonResponse> LogonAsync(ulong steamId, string refreshToken, CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<CMsgClientLogonResponse> tcs = new TaskCompletionSource<CMsgClientLogonResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (CancellationTokenRegistration tokenRegistration = cancellationToken.Register(async () => { tcs.TrySetCanceled(); await DisconnectAsync().ConfigureAwait(false); }))
            {
                baseClient.WithLogon((sender, logonResponse) =>
                {
                    tcs.TrySetResult(logonResponse.Body);
                });

                await baseClient.LogonAsync(new LogonParameter
                {
                    SteamId = steamId,
                    AccessToken = refreshToken!,

                    ChatMode = logonOptions.ChatMode,
                    GamingDeviceType = logonOptions.GamingDeviceType,
                    OSType = logonOptions.OSType,
                    UIMode = logonOptions.UIMode,
                }, cancellationToken).ConfigureAwait(false);

                return await tcs.Task.ConfigureAwait(false);
            }
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
        /// 发送消息
        /// </summary>
        /// <param name="msg">Msg</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task SendAsync(IClientMsg msg, CancellationToken cancellationToken = default)
        {
            await baseClient.SendAsync(msg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            baseClient.Dispose();
        }

        /// <summary>
        /// SteamId
        /// </summary>
        public ulong? SteamId => baseClient.SteamId;

        /// <summary>
        /// 服务连接协议
        /// </summary>
        public ProtocolTypes? Protocol => baseClient.Protocol;

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger? Logger => baseClient.Logger;

        /// <summary>
        /// Connected
        /// </summary>
        public bool Connected => baseClient.IsConnected();

        private string ConvertWebsiteId(EAuthTokenPlatformType platformType)
        {
            string websiteId = "Mobile";
            switch (platformType)
            {
                case EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown:
                    websiteId = "Unknown";
                    break;

                case EAuthTokenPlatformType.k_EAuthTokenPlatformType_SteamClient:
                    websiteId = "Client";
                    break;

                case EAuthTokenPlatformType.k_EAuthTokenPlatformType_WebBrowser:
                    websiteId = "Website";
                    break;

                case EAuthTokenPlatformType.k_EAuthTokenPlatformType_MobileApp:
                    websiteId = "Mobile";
                    break;
            }

            return websiteId;
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
    }
}
