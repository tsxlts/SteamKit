using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ProtoBuf;
using SteamKit.Client.Hanlders;
using SteamKit.Client.Internal.Connection;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Internal.Msg;
using SteamKit.Client.Internal.Server;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.Proto;
using SteamKit.Exceptions;
using SteamKit.Factory;
using SteamKit.Internal.Provider;
using SteamKit.Types;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal
{
    internal class BaseClient : IDisposable
    {
        public readonly AsyncLock @lock = new AsyncLock();
        public const ulong DefaultSteamId = 76561197960265728;
        public const ulong AnonymousSteamId = 117093590311632896;
        public string clientLanguage = "schinese";
        public uint cellId = 0;

        private readonly ulong startTime;
        private readonly string machineName;

        private readonly int timeout = 60 * 1000;
        private readonly List<MessageHandler> handlers;
        private readonly ConcurrentDictionary<EMsg, MessageAsyncCallbackHandler<MessageCallback>> messageCallbacks;

        private readonly MsgCallbackManager msgCallbackManager;

        private readonly MessageCallbackHandler<ConnectedEventArgs> connectedCallback;
        private readonly MessageCallbackHandler<DisconnectedEventArgs> disConnectedCallback;
        private readonly MessageCallbackHandler<ServerProtoBufMsg<CMsgClientLogonResponse>> logonCallback;
        private readonly MessageCallbackHandler<ServerProtoBufMsg<CMsgClientLoggedOff>> loggedOffCallback;

        private readonly AsyncJobCollection<ServerResult> asyncJobs;
        private readonly ScheduledFunction heartBeatFunc;

        private ProtocolTypes protocolTypes;
        private IMachineInfoProvider machineInfoProvider;
        private IServerProvider serverProvider;
        private ISocketProvider socketProvider;

        private IConnection? Connection;
        private int status = (int)ClientStatus.Close;
        private ulong currentSequential = 0;
        private bool isLogged = false;

        private static MethodInfo serviceMethodCallAsync;

        static BaseClient()
        {
            Expression<Func<BaseClient, Task<JobResult<CMsgClientHeartBeat>>>> expression = c => c.ServiceMethodCallAsync<CMsgClientHeartBeat, CMsgClientHeartBeat>("", "", new CMsgClientHeartBeat(), 1, default);
            serviceMethodCallAsync = (expression.Body as MethodCallExpression)!.Method.GetGenericMethodDefinition();
        }

        public BaseClient() : this(ProtocolTypes.WebSocket)
        {

        }

        public BaseClient(ProtocolTypes protocol)
        {
            this.startTime = (ulong)Extensions.GetSystemTimestamp();

            machineName = $"{Environment.MachineName}@SteamKit";

            protocolTypes = protocol;
            machineInfoProvider = MachineInfoProvider.GetDefaultProvider();
            serverProvider = new DefaultServerProvider();
            socketProvider = new DefaultSocketProvider();

            handlers = new List<MessageHandler>();
            messageCallbacks = new ConcurrentDictionary<EMsg, MessageAsyncCallbackHandler<MessageCallback>>();

            msgCallbackManager = new MsgCallbackManager(this);

            connectedCallback = new MessageCallbackHandler<ConnectedEventArgs>();
            disConnectedCallback = new MessageCallbackHandler<DisconnectedEventArgs>();
            logonCallback = new MessageCallbackHandler<ServerProtoBufMsg<CMsgClientLogonResponse>>();
            loggedOffCallback = new MessageCallbackHandler<ServerProtoBufMsg<CMsgClientLoggedOff>>();

            asyncJobs = new AsyncJobCollection<ServerResult>();

            heartBeatFunc = new ScheduledFunction(() =>
            {
                Logger?.LogDebug($"ClientHeartBeat");
                Send(new ClientProtoBufMsg<CMsgClientHeartBeat>(EMsg.ClientHeartBeat));
            });
        }

        public BaseClient WithProtocol(ProtocolTypes protocol)
        {
            protocolTypes = protocol;
            return this;
        }

        public BaseClient WithServerProvider(IServerProvider serverProvider)
        {
            this.serverProvider = serverProvider;
            return this;
        }

        public BaseClient WithSocketProvider(ISocketProvider socketProvider)
        {
            this.socketProvider = socketProvider;
            return this;
        }

        public BaseClient WithMachineInfoProvider(IMachineInfoProvider machineProvider)
        {
            machineInfoProvider = machineProvider;
            return this;
        }

        public BaseClient WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }

        public BaseClient WithClientLanguage(string language)
        {
            clientLanguage = language;
            return this;
        }

        public BaseClient WithCellId(uint cellId)
        {
            this.cellId = cellId;
            return this;
        }

        public BaseClient WithConnected(EventHandler<ConnectedEventArgs> connected)
        {
            connectedCallback.SetCallback(connected);
            return this;
        }

        public BaseClient WithDisconnected(EventHandler<DisconnectedEventArgs> disconnected)
        {
            disConnectedCallback.SetCallback(disconnected);
            return this;
        }

        public BaseClient WithLogon(EventHandler<ServerProtoBufMsg<CMsgClientLogonResponse>> logon)
        {
            logonCallback.SetCallback(logon);
            return this;
        }

        public BaseClient WithLogoff(EventHandler<ServerProtoBufMsg<CMsgClientLoggedOff>> loggedOff)
        {
            loggedOffCallback.SetCallback(loggedOff);
            return this;
        }

        public BaseClient RegistCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            messageCallbacks.AddOrUpdate(msgType, key => new MessageAsyncCallbackHandler<MessageCallback>(callback), (key, value) =>
            {
                value.Callback += callback;
                return value;
            });
            return this;
        }

        public BaseClient RemoveCallback(EMsg msgType, AsyncEventHandler<MessageCallback> callback)
        {
            messageCallbacks.AddOrUpdate(msgType, key => new MessageAsyncCallbackHandler<MessageCallback>(), (key, value) =>
            {
                value.Callback -= callback;
                return value;
            });
            return this;
        }

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            using (await @lock.LockAsync(cancellationToken).ConfigureAwait(false))
            {
                IConnection connection = await CreateConnectionAsync(null, cancellationToken).ConfigureAwait(false);
                await ConnectAsync(connection, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task ConnectAsync(EndPoint endPoint, ProtocolTypes protocol, CancellationToken cancellationToken = default)
        {
            using (await @lock.LockAsync(cancellationToken).ConfigureAwait(false))
            {
                Model.Server? server = new Model.Server(endPoint, protocol);
                IConnection connection = await CreateConnectionAsync(server, cancellationToken).ConfigureAwait(false);
                await ConnectAsync(connection, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            using (await @lock.LockAsync(cancellationToken).ConfigureAwait(false))
            {
                await DisconnectAsync(DisconnectType.UserInitiated, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 登录客户端
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task LogonAsync(LogonParameter parameter, CancellationToken cancellationToken = default)
        {
            var logon = new ClientProtoBufMsg<CMsgClientLogon>(EMsg.ClientLogon);
            logon.Body = new CMsgClientLogon
            {
                protocol_version = MsgClientLogon.CurrentProtocol,
                client_package_version = 1771,
                qos_level = parameter.QosLevel,

                account_name = parameter.UserName,
                password = parameter.Password,
                two_factor_code = parameter.TwoFactorCode,
                auth_code = parameter.AuthCode,
                access_token = parameter.AccessToken,
                client_os_type = (uint)parameter.OSType,
                chat_mode = (uint)parameter.ChatMode,

                client_language = clientLanguage,
                cell_id = cellId,

                obfuscated_private_ip = SteamHelpers.GetMsgIPAddress(GetLocalIP()!).ObfuscatePrivateIP(),
                machine_name = $"{machineName}",
                machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],

                //sha_sentryfile = new byte[0],
                //eresult_sentryfile = (int)EResult.FileNotFound,

                supports_rate_limit_response = true,
                should_remember_password = true,

                is_chrome_os = false,
                is_steam_box_deprecated = false,
                is_steam_deck_deprecated = false,
                is_tesla_deprecated = false,
            };

            if (parameter.UIMode != EUIMode.Unknown)
            {
                logon.Body.ui_mode = (uint)parameter.UIMode;
            }
            if (parameter.GamingDeviceType != EGamingDeviceType.Unknown)
            {
                logon.Body.gaming_device_type = (uint)parameter.GamingDeviceType;
            }

            if (logon.Body.obfuscated_private_ip.ShouldSerializev4())
            {
                logon.Body.deprecated_obfustucated_private_ip = logon.Body.obfuscated_private_ip.v4;
            }

            var steamid = parameter.SteamId;
            if (steamid <= 0)
            {
                var steamId = new SteamId(parameter.AccountId, parameter.AccountInstance, Universe, EAccountType.Individual);
                steamid = steamId.ConvertToUInt64();
            }
            logon.Header.Proto.steamid = steamid;

            await SendAsync(logon, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 网页Token登录
        /// </summary>
        /// <param name="userName">登录用户名</param>
        /// <param name="steamId">登录用户SteamId</param>
        /// <param name="webLogonToken">网站登录Token</param>
        /// <returns></returns>
        public async Task WebTokenLogonAsync(string userName, ulong steamId, string webLogonToken)
        {
            var logon = new ClientProtoBufMsg<CMsgClientLogon>(EMsg.ClientLogon);
            logon.Body = new CMsgClientLogon
            {
                account_name = userName,
                protocol_version = MsgClientLogon.CurrentProtocol,
                client_os_type = unchecked((uint)EOSType.Web),
                web_logon_nonce = webLogonToken,
                //qos_level = 2,
                //ui_mode = 3,
                //chat_mode = 2,
            };
            logon.Header.Proto.steamid = steamId;

            await SendAsync(logon).ConfigureAwait(false);
        }

        /// <summary>
        /// 匿名登录客户端
        /// </summary>
        /// <returns></returns>
        public async Task LogonAnonymousAsync()
        {
            var logon = new ClientProtoBufMsg<CMsgClientLogon>(EMsg.ClientLogon);
            logon.Body = new CMsgClientLogon
            {
                protocol_version = MsgClientLogon.CurrentProtocol,
                client_os_type = (uint)SteamHelpers.GetOSType(),
                client_language = clientLanguage,
                cell_id = cellId,
                machine_name = $"{machineName}",
                machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],
                sha_sentryfile = new byte[0],
                eresult_sentryfile = (int)EResult.FileNotFound
            };

            logon.Header.Proto.client_sessionid = 0;

            var steamId = new SteamId(0, 0, Universe, EAccountType.AnonUser);
            logon.Header.Proto.steamid = steamId.ConvertToUInt64();

            await SendAsync(logon).ConfigureAwait(false);
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="token">登录身份令牌</param>
        /// <returns></returns>
        public async Task LogonGameAsync(int appId, string token)
        {
            var logon = new ClientProtoBufMsg<CMsgClientLogon>(EMsg.ClientLogonGameServer);
            logon.Body = new CMsgClientLogon
            {
                game_server_app_id = appId,
                game_server_token = token,

                machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],
                protocol_version = MsgClientLogon.CurrentProtocol,
                client_os_type = (uint)SteamHelpers.GetOSType(),
                obfuscated_private_ip = SteamHelpers.GetMsgIPAddress(GetLocalIP()!).ObfuscatePrivateIP()
            };
            var steamId = new SteamId(0, 0, Universe, EAccountType.GameServer);

            logon.Header.Proto.client_sessionid = 0;
            logon.Header.Proto.steamid = steamId.ConvertToUInt64();

            await SendAsync(logon).ConfigureAwait(false);
        }

        /// <summary>
        /// 匿名登录游戏
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <returns></returns>
        public async Task LogonGameAnonymousAsync(int appId)
        {
            var logon = new ClientProtoBufMsg<CMsgClientLogon>(EMsg.ClientLogon);
            logon.Body = new CMsgClientLogon
            {
                game_server_app_id = appId,

                machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],
                protocol_version = MsgClientLogon.CurrentProtocol,
                client_os_type = (uint)SteamHelpers.GetOSType(),
                obfuscated_private_ip = SteamHelpers.GetMsgIPAddress(GetLocalIP()!).ObfuscatePrivateIP()
            };
            var steamId = new SteamId(0, 0, Universe, EAccountType.AnonGameServer);

            logon.Header.Proto.client_sessionid = 0;
            logon.Header.Proto.steamid = steamId.ConvertToUInt64();

            await SendAsync(logon).ConfigureAwait(false);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task LogoffAsync()
        {
            var logoff = new ClientProtoBufMsg<CMsgClientLogOff>(EMsg.ClientLogOff);
            await SendAsync(logoff).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="guardData"></param>
        /// <param name="websiteId">
        /// Unknown
        /// Client
        /// Mobile
        /// Website
        /// Store
        /// Community
        /// Partner
        /// SteamStats
        /// </param>
        /// <param name="platformType"></param>
        /// <param name="appType"></param>
        /// <param name="qosLevel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AuthException"></exception>
        public async Task<BeginCredentialsAuthResult> AuthTokenViaCredentialsAsync(string userName, string password, string? guardData, string websiteId, EAuthTokenPlatformType platformType, EAuthTokenAppType appType, int qosLevel, CancellationToken cancellationToken = default)
        {
            var publicResult = await ServiceMethodCallAsync((IAuthentication api) => api.GetPasswordRSAPublicKey(new CAuthentication_GetPasswordRSAPublicKey_Request
            {
                account_name = userName,
            }), cancellationToken: cancellationToken).ConfigureAwait(false);
            var publicKey = publicResult.Result;
            if (string.IsNullOrWhiteSpace(publicKey?.publickey_exp) || string.IsNullOrWhiteSpace(publicKey?.publickey_mod))
            {
                throw new AuthException("获取登录秘钥失败")
                {
                    EResult = publicResult?.EResult ?? EResult.Fail,
                    Error = "获取登录秘钥失败"
                };
            }

            var rsaParameters = new RSAParameters
            {
                Modulus = SteamKit.Internal.Utils.HexStringToByteArray(publicKey.publickey_mod),
                Exponent = SteamKit.Internal.Utils.HexStringToByteArray(publicKey.publickey_exp),
            };
            using var rsa = RSA.Create();
            rsa.ImportParameters(rsaParameters);
            var encryptedPassword = rsa.Encrypt(Encoding.UTF8.GetBytes(password), RSAEncryptionPadding.Pkcs1);

            var authSessionViaCredentialsResult = await ServiceMethodCallAsync((IAuthentication api) => api.BeginAuthSessionViaCredentials(new CAuthentication_BeginAuthSessionViaCredentials_Request
            {
                account_name = userName,
                persistence = ESessionPersistence.k_ESessionPersistence_Persistent,
                website_id = websiteId,
                guard_data = guardData ?? "",
                qos_level = qosLevel,
                encrypted_password = Convert.ToBase64String(encryptedPassword),
                encryption_timestamp = publicKey.timestamp,
                platform_type = platformType,
                device_friendly_name = $"{machineName}",
                device_details = new CAuthentication_DeviceDetails
                {
                    device_friendly_name = $"{machineName}",
                    platform_type = platformType,
                    os_type = (int)SteamHelpers.GetOSType(),
                    gaming_device_type = 0,
                    client_count = 0,
                    machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],
                    app_type = appType
                }
            }), cancellationToken: cancellationToken).ConfigureAwait(false);
            var authSessionViaCredentialsResponse = authSessionViaCredentialsResult.Result;
            if (authSessionViaCredentialsResponse == null || authSessionViaCredentialsResponse.steamid == 0)
            {
                throw new AuthException($"{authSessionViaCredentialsResponse?.extended_error_message}[{authSessionViaCredentialsResult?.EResult}]")
                {
                    EResult = authSessionViaCredentialsResult?.EResult ?? EResult.Fail,
                    Error = authSessionViaCredentialsResponse?.extended_error_message
                };
            }
            var result = new BeginCredentialsAuthResult(this)
            {
                SteamId = authSessionViaCredentialsResponse.steamid,
                ClientId = authSessionViaCredentialsResponse.client_id,
                RequestId = authSessionViaCredentialsResponse.request_id,
                PollingInterval = TimeSpan.FromSeconds(authSessionViaCredentialsResponse.interval),
                WeakToken = authSessionViaCredentialsResponse.weak_token,
                AllowedConfirmations = authSessionViaCredentialsResponse.allowed_confirmations.OrderBy(c =>
                    {
                        switch (c.confirmation_type)
                        {
                            case EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceConfirmation:
                                return 1;
                            case EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode:
                                return 2;
                            case EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode:
                                return 3;
                            case EAuthSessionGuardType.k_EAuthSessionGuardType_EmailConfirmation:
                                return 4;
                            default:
                                return 99;
                        }
                    }).ToList() ?? new List<CAuthentication_AllowedConfirmation>()
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="websiteId">
        /// Unknown
        /// Client
        /// Mobile
        /// Website
        /// Store
        /// Community
        /// Partner
        /// SteamStats
        /// </param>
        /// <param name="platformType"></param>
        /// <param name="appType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<BeginQrAuthResult> AuthTokenViaQrAsync(string websiteId, EAuthTokenPlatformType platformType, EAuthTokenAppType appType, CancellationToken cancellationToken = default)
        {
            var authSessionViaQrResult = await ServiceMethodCallAsync((IAuthentication api) => api.BeginAuthSessionViaQR(new CAuthentication_BeginAuthSessionViaQR_Request
            {
                website_id = websiteId,
                device_friendly_name = $"{machineName}",
                platform_type = platformType,

                device_details = new CAuthentication_DeviceDetails
                {
                    device_friendly_name = $"{machineName}",
                    platform_type = platformType,
                    os_type = (int)SteamHelpers.GetOSType(),
                    gaming_device_type = 0,
                    client_count = 0,
                    machine_id = MachineInfoProvider.GetMachineId(machineInfoProvider) ?? new byte[0],
                    app_type = appType,
                }
            }), cancellationToken: cancellationToken).ConfigureAwait(false);


            if (authSessionViaQrResult?.EResult != EResult.OK || authSessionViaQrResult.Result == null)
            {
                throw new InvalidOperationException($"Failed to begin QR auth session[{authSessionViaQrResult?.EResult}]");
            }

            var response = authSessionViaQrResult.Result;
            var result = new BeginQrAuthResult(this)
            {
                RequestId = response.request_id,
                ClientId = response.client_id,
                ChallengeURL = response.challenge_url,
                AllowedConfirmations = response.allowed_confirmations,
                PollingInterval = TimeSpan.FromSeconds(response.interval)
            };
            return result;
        }

        public async Task SendAsync(IClientMsg msg, CancellationToken cancellationToken = default)
        {
            if (SessionId.HasValue)
            {
                msg.SessionID = SessionId.Value;
            }
            if (SteamId.HasValue)
            {
                msg.SteamID = SteamId.Value;
            }

            var serialized = msg.Serialize();
            await SendAsync(serialized, cancellationToken).ConfigureAwait(false);
        }

        public void Send(IClientMsg msg)
        {
            if (SessionId.HasValue)
            {
                msg.SessionID = SessionId.Value;
            }
            if (SteamId.HasValue)
            {
                msg.SteamID = SteamId.Value;
            }

            var serialized = msg.Serialize();
            Send(serialized);
        }

        /// <summary>
        /// 请使用 <see cref="SteamGameCoordinator.SendAsync"/>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="gcMsg"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task SendAsync(uint appId, IGCClientMsg gcMsg, CancellationToken cancellationToken = default)
        {
            var clientMsg = new ClientProtoBufMsg<CMsgGCClient>(EMsg.ClientToGC)
            {
                SteamID = SteamId ?? 0,
                SessionID = SessionId ?? 0,
                Body = new CMsgGCClient
                {
                    appid = appId,
                    msgtype = MsgUtil.MakeGCMsg(gcMsg.MsgType, gcMsg.IsProto()),
                    payload = gcMsg.Serialize(),
                    steamid = SteamId ?? 0,
                    ip = SteamHelpers.GetIPAddressAsUInt(GetLocalIP()!)
                }
            };

            clientMsg.Header.Proto.routing_appid = appId;
            await SendAsync(clientMsg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送异步消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="msgType"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<JobResult<TResponse>> SendAsync<TRequest, TResponse>(EMsg msgType, TRequest message, CancellationToken cancellationToken = default) where TRequest : IExtensible, new() where TResponse : IExtensible, new()
        {
            var jobId = GetAsyncJobId();
            var msg = new ClientProtoBufMsg<TRequest>(msgType)
            {
                SourceJobID = jobId,
                Body = message
            };

            var job = new AsyncJob<ServerResult>(jobId, cancellationToken);
            if (!asyncJobs.TryAdd(job))
            {
                job.SetException(new Exception("注册任务失败"));
            }
            await SendAsync(msg).ConfigureAwait(false);

            var result = await job.ConfigureAwait(false);
            return new JobResult<TResponse>
            {
                JobId = job.JobId,
                MsgType = result?.MsgType ?? EMsg.Invalid,
                EResult = result?.EResult ?? EResult.Fail,
                ErrorMessage = result?.ErrorMessage,
                Result = result != null ? result.GetResult<TResponse>() : default
            };
        }

        /// <summary>
        /// 调用服务端方法
        /// </summary>
        /// <typeparam name="TServer"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="targetServer"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<JobResult<TResponse>> ServiceMethodCallAsync<TServer, TResponse>(Expression<Func<TServer, TResponse>> targetServer, uint version = 1, CancellationToken cancellationToken = default) where TServer : class where TResponse : IExtensible, new()
        {
            if (targetServer.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException("Expression must be a method call.", "targetServer");
            }

            Type server = typeof(TServer);
            string serverName = server.Name;
            if (server.IsInterface && serverName.StartsWith("I", StringComparison.CurrentCultureIgnoreCase))
            {
                serverName = serverName.Substring(1);
            }

            var call = (MethodCallExpression)targetServer.Body;
            string methodName = call.Method.Name;

            if (call.Arguments.Count != 1)
            {
                throw new ArgumentException("The number of parameters for the method must be 1.");
            }
            var argument = call.Arguments.Single();
            object? message;
            switch (argument.NodeType)
            {
                case ExpressionType.MemberAccess:
                case ExpressionType.MemberInit:
                case ExpressionType.New:
                    var unary = Expression.Convert(argument, typeof(object));
                    var lambda = Expression.Lambda<Func<object>>(unary);
                    var getter = lambda.Compile();
                    message = getter();
                    break;

                case ExpressionType.Constant:
                    message = (argument as ConstantExpression)!.Value;
                    break;

                default:
                    throw new NotSupportedException("Unknown Expression type.");
            }

            MethodInfo callMethod = serviceMethodCallAsync.MakeGenericMethod(argument.Type, typeof(TResponse));
            var task = ((Task<JobResult<TResponse>>)callMethod.Invoke(this, new object?[] { serverName, methodName, message, version, cancellationToken })!).ConfigureAwait(false);
            return await task;
        }

        /// <summary>
        /// 调用服务端方法
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="targetServerName"></param>
        /// <param name="targetMethodName"></param>
        /// <param name="message"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<JobResult<TResponse>> ServiceMethodCallAsync<TRequest, TResponse>(string targetServerName, string targetMethodName, TRequest message, uint version = 1, CancellationToken cancellationToken = default) where TRequest : IExtensible, new() where TResponse : IExtensible, new()
        {
            var jobId = GetAsyncJobId();

            var eMsg = SteamId == null ? EMsg.ServiceMethodCallFromClientNonAuthed : EMsg.ServiceMethodCallFromClient;
            var msg = new ClientProtoBufMsg<TRequest>(eMsg)
            {
                SourceJobID = jobId,
                Body = message
            };
            msg.Header.Proto.target_job_name = $"{targetServerName}.{targetMethodName}#{version}";

            var job = new AsyncJob<ServerResult>(jobId, cancellationToken);
            if (!asyncJobs.TryAdd(job))
            {
                job.SetException(new Exception("注册任务失败"));
            }

            await SendAsync(msg, cancellationToken).ConfigureAwait(false);

            var result = await job.ConfigureAwait(false);
            return new JobResult<TResponse>
            {
                JobId = job.JobId,
                MsgType = result?.MsgType ?? EMsg.Invalid,
                EResult = result?.EResult ?? EResult.Fail,
                ErrorMessage = result?.ErrorMessage,
                Result = result != null ? result.GetResult<TResponse>() : default
            };
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
            var eMsg = SteamId == null ? EMsg.ServiceMethodCallFromClientNonAuthed : EMsg.ServiceMethodCallFromClient;
            var msg = new ClientProtoBufMsg<TRequest>(eMsg)
            {
                Body = message
            };
            msg.Header.Proto.target_job_name = $"{targetServerName}.{targetMethodName}#{version}";

            await SendAsync(msg, cancellationToken).ConfigureAwait(false);
        }

        public void AddHandler(MessageHandler handler)
        {
            ArgumentNullException.ThrowIfNull(handler);

            var type = handler.GetType();
            var handlerIndex = handlers.FindIndex(h => h.GetType() == type);
            if (handlerIndex > -1)
            {
                throw new InvalidOperationException(string.Format("A handler of type \"{0}\" is already registered.", type));
            }

            handlers.Add(handler);
        }

        public bool RemoveHandler<T>() where T : MessageHandler
        {
            Type type = typeof(T);
            handlers.RemoveAll(h => h.GetType() == type);
            return true;
        }

        public T GetHandler<T>() where T : MessageHandler
        {
            if (TryGetHandler<T>(out var handler))
            {
                return handler;
            }

            Type type = typeof(T);
            throw new InvalidOperationException(string.Format("A handler of type \"{0}\" is not registered.", type));
        }

        public bool TryGetHandler<T>([NotNullWhen(true)] out T? handler) where T : MessageHandler
        {
            Type type = typeof(T);
            handler = handlers.Find(h => h.GetType() == type) as T;

            return handler != null;
        }

        public IPAddress GetLocalIP()
        {
            return Connection?.GetLocalIP() ?? IPAddress.None;
        }

        public bool IsConnected()
        {
            if (Connection == null)
            {
                return false;
            }
            if (!Connection.IsConnected)
            {
                return false;
            }

            return status == (int)ClientStatus.Open;
        }

        public bool IsLogged()
        {
            return isLogged;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
            Disconnected(this, new DisconnectedEventArgs(DisconnectType.Dispose));
        }

        internal List<MessageHandler> GetMessageHandlers()
        {
            return handlers;
        }

        internal MessageAsyncCallbackHandler<MessageCallback>? GetMessageCallback(EMsg msg)
        {
            messageCallbacks.TryGetValue(msg, out var callback);
            return callback;
        }

        internal ulong GetAsyncJobId()
        {
            var sequential = Interlocked.Increment(ref currentSequential);
            var Id = new GlobalId
            {
                ProcessId = 0,
                BoxId = 0,
                Sequential = sequential,
                StartTime = startTime
            };
            return Id;
        }

        #region 私有方法

        private async Task<SteamServer?> GetServerAsync(ProtocolTypes protocol, CancellationToken cancellationToken = default)
        {
            Logger?.LogDebug("Waiting to obtain server address ...");

            if (protocolTypes.HasFlag(ProtocolTypes.WebSocket))
            {
                var result = await serverProvider.GetServerAsync(ProtocolTypes.WebSocket, cancellationToken);
                return result;
            }

            if (protocolTypes.HasFlag(ProtocolTypes.Tcp))
            {
                var result = await serverProvider.GetServerAsync(ProtocolTypes.Tcp, cancellationToken);
                return result;
            }

            if (protocolTypes.HasFlag(ProtocolTypes.Udp))
            {
                var result = await serverProvider.GetServerAsync(ProtocolTypes.Udp, cancellationToken);
                return result;
            }

            return null;
        }

        private async Task<IConnection> CreateConnectionAsync(Model.Server? server, CancellationToken cancellationToken)
        {
            SteamServer? connectService = server ?? await GetServerAsync(protocolTypes, cancellationToken);
            if (connectService == null)
            {
                throw new Exception("没有可用的服务器");
            }

            if (connectService.ProtocolTypes.HasFlag(ProtocolTypes.WebSocket))
            {
                var webSocket = socketProvider.GetWebSocket();
                return new WebSocketConnection(connectService.EndPoint, webSocket);
            }

            if (connectService.ProtocolTypes.HasFlag(ProtocolTypes.Tcp))
            {
                var socket = socketProvider.GetSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, timeout);
                return new EncryptedConnection(new Tcp2Connection(connectService.EndPoint, socket));
            }

            if (connectService.ProtocolTypes.HasFlag(ProtocolTypes.Udp))
            {
                var socket = socketProvider.GetSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp, timeout);
                return new EncryptedConnection(new UdpConnection(connectService.EndPoint, socket));
            }

            throw new ArgumentException($"未注册协议为{connectService.ProtocolTypes}的服务连接");
        }

        private Task ConnectAsync(IConnection connection, CancellationToken cancellationToken)
        {
            var connectionRelease = Interlocked.Exchange(ref Connection, connection);

            Connection.Connected += Connected;
            Connection.Disconnected += Disconnected;
            Connection.MsgReceived += NetMsgReceived;

            Logger?.LogDebug($"Server address : [{connection.Protocol}] {Connection.CurrentEndPoint}");
            Logger?.LogDebug("Waiting for connection ...");
            Logger?.LogInformation("Waiting for connection ...");

            try
            {
                return Connection.ConnectAsync(cancellationToken);
            }
            catch
            {
                serverProvider.DisableServer(Connection.CurrentEndPoint);
                throw;
            }
        }

        private Task DisconnectAsync(DisconnectType disconnectType, CancellationToken cancellationToken)
        {
            heartBeatFunc?.Stop();
            return Connection?.DisconnectAsync(disconnectType, cancellationToken) ?? Task.CompletedTask;
        }

        private void Connected(object? sender, ConnectedEventArgs e)
        {
            Logger?.LogDebug("Connection successful");
            Logger?.LogInformation("Connection successful");

            Interlocked.Exchange(ref status, (int)ClientStatus.Open);

            var request = new ClientProtoBufMsg<CMsgClientHello>(EMsg.ClientHello);
            request.Body.protocol_version = MsgClientLogon.CurrentProtocol;
            SendAsync(request.Serialize()).GetAwaiter().GetResult();

            connectedCallback.InvokeAsync(sender, e, Logger).ConfigureAwait(false);
        }

        private void Disconnected(object? sender, DisconnectedEventArgs e)
        {
            Logger?.LogDebug("Connection dropped: {0}", e.Type);
            Logger?.LogInformation("Connection dropped: {0}", e.Type);
            if (e.Exception != null)
            {
                Logger?.LogException(e.Exception, "Connection dropped: {0}", e.Type);
            }

            ClientStatus localConnected = (ClientStatus)Interlocked.Exchange(ref status, (int)ClientStatus.Close);

            heartBeatFunc.Stop();
            asyncJobs.CancelPendingJobs();

            isLogged = false;
            SessionId = null;
            SteamId = null;

            CellId = null;
            PublicIP = null;
            IPCountryCode = null;

            var connectionRelease = Interlocked.Exchange(ref Connection, null);
            if (connectionRelease == null)
            {
                return;
            }

            connectionRelease.MsgReceived -= NetMsgReceived;
            connectionRelease.Connected -= Connected;
            connectionRelease.Disconnected -= Disconnected;

            if (localConnected == ClientStatus.Close)
            {
                return;
            }

            disConnectedCallback.InvokeAsync(sender, e, Logger).ConfigureAwait(false);
        }

        private void NetMsgReceived(object? sender, MsgEventArgs e)
        {
            IServerMsg? packetMsg = MsgConvert.DeserializeServerMsg(e.Data);
            OnClientMsgReceived(packetMsg);
        }

        private bool OnClientMsgReceived(IServerMsg? packetMsg)
        {
            if (packetMsg == null)
            {
                DisconnectAsync(DisconnectType.ConnectionError, default);
                return false;
            }

            Logger?.LogDebug("Received message, jobId: {0}, msgType: {1}", packetMsg.JobID, packetMsg.MsgType);

            switch (packetMsg.MsgType)
            {
                case EMsg.Multi:
                    HandleMulti(packetMsg);
                    break;

                case EMsg.ClientLogOnResponse:
                    HandleLogonResponse(packetMsg);
                    break;

                case EMsg.ClientLoggedOff:
                    HandleLoggedOff(packetMsg);
                    break;

                case EMsg.ClientServerUnavailable:
                    HandleServerUnavailable(packetMsg);
                    break;

                case EMsg.ClientCMList:
                    HandleCMList(packetMsg);
                    break;

                case EMsg.ClientSessionToken:
                    HandleSessionToken(packetMsg);
                    break;

                case EMsg.ServiceMethod:
                    HandleServiceMethod(packetMsg);
                    break;

                case EMsg.ServiceMethodResponse:
                    HandleServiceMethodResponse(packetMsg);
                    break;

                case EMsg.ClientServiceCall:
                    break;

                case EMsg.JobHeartbeat:
                    HeartbeatJob(packetMsg.JobID);
                    break;

                case EMsg.DestJobFailed:
                    FailJob(packetMsg.JobID);
                    break;
            }

            if (packetMsg is ServerProtoBufMsg protoBufMsg)
            {
                asyncJobs.SetResult(packetMsg.JobID, new ServerResult(protoBufMsg));
            }

            msgCallbackManager.PostMsg(packetMsg);
            return true;

            if (messageCallbacks.TryGetValue(packetMsg.MsgType, out var callback))
            {
                callback.InvokeAsync(this, new MessageCallback
                {
                    PacketResult = packetMsg
                }, Logger).ConfigureAwait(false);
            }

            foreach (var handler in handlers)
            {
                try
                {
                    handler.HandleMsgAsync(packetMsg).ConfigureAwait(false);
                }
                catch (ProtoException ex)
                {
                    Logger?.LogException(ex, $"'{handler.GetType().Name}' handler failed to (de)serialize a protobuf: {ex}");
                }
                catch (Exception ex)
                {
                    Logger?.LogException(ex, $"Unhandled exception from '{handler.GetType().Name}' handler: {ex}");
                }
            }

            return true;
        }

        private void HandleMulti(IServerMsg packetMsg)
        {
            if (!packetMsg.IsProto())
            {
                return;
            }

            var msgMulti = new ServerProtoBufMsg<CMsgMulti>(packetMsg);
            byte[] payload = msgMulti.Body.message_body;
            if (msgMulti.Body.size_unzipped > 0)
            {
                try
                {
                    using var compressedStream = new MemoryStream(payload);
                    using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                    using var decompressedStream = new MemoryStream();
                    gzipStream.CopyTo(decompressedStream);
                    payload = decompressedStream.ToArray();
                }
                catch (Exception)
                {
                    return;
                }
            }

            using var ms = new MemoryStream(payload);
            using var br = new BinaryReader(ms);
            while (ms.Length - ms.Position != 0)
            {
                int subSize = br.ReadInt32();
                byte[] subData = br.ReadBytes(subSize);

                if (!OnClientMsgReceived(MsgConvert.DeserializeServerMsg(subData)))
                {
                    break;
                }
            }

        }

        private void HandleLogonResponse(IServerMsg packetMsg)
        {
            if (!packetMsg.IsProto())
            {
                return;
            }

            var logonResponse = new ServerProtoBufMsg<CMsgClientLogonResponse>(packetMsg);
            var eResult = (EResult)logonResponse.Body.eresult;
            var extendedResult = (EResult)logonResponse.Body.eresult_extended;

            switch (eResult)
            {
                case EResult.OK:
                    {
                        Logger?.LogDebug("Login successful: {0}", logonResponse.Header.Proto.steamid);
                        Logger?.LogInformation("Login successful: {0}", logonResponse.Header.Proto.steamid);

                        isLogged = true;
                        SessionId = logonResponse.Header.Proto.client_sessionid;
                        SteamId = logonResponse.Header.Proto.steamid;

                        CellId = logonResponse.Body.cell_id;
                        PublicIP = logonResponse.Body.public_ip.GetIPAddress();
                        IPCountryCode = logonResponse.Body.ip_country_code;

                        int hbDelay = logonResponse.Body.legacy_out_of_game_heartbeat_seconds;

                        heartBeatFunc.Stop();
                        heartBeatFunc.Delay = TimeSpan.FromSeconds(hbDelay);
                        heartBeatFunc.Start();
                    }
                    break;

                case EResult.TryAnotherCM:
                case EResult.ServiceUnavailable:
                    {
                        Logger?.LogDebug("Login failed: {0} / {1}", eResult, extendedResult);
                        Logger?.LogInformation("Login failed: {0} / {1}", eResult, extendedResult);

                        if (Connection?.CurrentEndPoint == null)
                        {
                            break;
                        }

                        serverProvider.DisableServer(Connection.CurrentEndPoint);
                    }
                    break;

                default:
                    {
                        Logger?.LogDebug("Login failed: {0} / {1}", eResult, extendedResult);
                        Logger?.LogError("Login failed: {0} / {1}", eResult, extendedResult);
                        Logger?.LogInformation("Login failed: {0} / {1}", eResult, extendedResult);
                    }
                    break;
            }

            logonCallback.InvokeAsync(this, logonResponse, Logger).ConfigureAwait(false);
        }

        private void HandleLoggedOff(IServerMsg packetMsg)
        {
            isLogged = false;
            SessionId = null;
            SteamId = null;

            CellId = null;
            PublicIP = null;
            IPCountryCode = null;

            heartBeatFunc.Stop();

            if (!packetMsg.IsProto())
            {
                return;
            }

            var logoffMsg = new ServerProtoBufMsg<CMsgClientLoggedOff>(packetMsg);
            var logoffResult = (EResult)logoffMsg.Body.eresult;

            Logger?.LogDebug("Client logged off: {0}", logoffResult);
            Logger?.LogInformation("Client logged off: {0}", logoffResult);

            if (logoffResult == EResult.TryAnotherCM || logoffResult == EResult.ServiceUnavailable)
            {
                serverProvider.DisableServer(Connection!.CurrentEndPoint);
            }

            loggedOffCallback.InvokeAsync(this, logoffMsg, Logger).ConfigureAwait(false);
        }

        private void HandleServerUnavailable(IServerMsg packetMsg)
        {
            var msgServerUnavailable = new ServerMsg<MsgClientServerUnavailable>(packetMsg);

            DisconnectAsync(DisconnectType.ConnectionError, default);
        }

        private void HandleCMList(IServerMsg packetMsg)
        {
            var cmMsg = new ServerProtoBufMsg<CMsgClientCMList>(packetMsg);
            var cmList = cmMsg.Body.cm_addresses
                .Zip(cmMsg.Body.cm_ports, (addr, port) => Model.Server.CreateSocketServer(new IPEndPoint(SteamHelpers.GetIPAddress(addr), (int)port)));

            var webSocketList = cmMsg.Body.cm_websocket_addresses.Select(addr => Model.Server.CreateWebSocketServer(addr));

            serverProvider.ResetServer(cmList.Concat(webSocketList));
        }

        private void HandleSessionToken(IServerMsg packetMsg)
        {
            var sessToken = new ServerProtoBufMsg<CMsgClientSessionToken>(packetMsg);

            SessionToken = sessToken.Body.token;
        }

        private void HandleServiceMethod(IServerMsg packetMsg)
        {
            var serverMsg = new ServerProtoBufMsg(packetMsg.MsgType, packetMsg.GetData());
            var jobName = serverMsg.Header.Proto.target_job_name;
            if (string.IsNullOrEmpty(jobName))
            {
                return;
            }

            Logger?.LogDebug("Handle ServiceMethod: {0}", jobName);
        }

        private void HandleServiceMethodResponse(IServerMsg packetMsg)
        {
            var serverMsg = new ServerProtoBufMsg(packetMsg.MsgType, packetMsg.GetData());
            var jobName = serverMsg.Header.Proto.target_job_name;
            if (string.IsNullOrEmpty(jobName))
            {
                return;
            }

            Logger?.LogDebug("Handle ServiceMethodResponse: {0}", jobName);
        }

        private void HeartbeatJob(ulong jobId)
        {
            asyncJobs.Heartbeat(jobId);
        }

        private void FailJob(ulong jobId)
        {
            asyncJobs.SetException(jobId, new JobFailedException(jobId));
        }

        private async Task SendAsync(byte[] buffer, CancellationToken cancellationToken = default)
        {
            if (Connection == null)
            {
                throw new InvalidOperationException("Connection is null.");
            }

            if (!Connection.IsConnected)
            {
                if (status == (int)ClientStatus.Open)
                {
                    Disconnected(this, new DisconnectedEventArgs(DisconnectType.ConnectionClosed));
                }

                throw new ConnectionException(Connection.CurrentEndPoint, Connection.Protocol, "连接已断开");
            }

            await Connection.SendAsync(buffer, cancellationToken).ConfigureAwait(false);
        }

        private void Send(byte[] buffer)
        {
            if (Connection == null)
            {
                throw new InvalidOperationException("Connection is null.");
            }

            if (!Connection.IsConnected)
            {
                if (status == (int)ClientStatus.Open)
                {
                    Disconnected(this, new DisconnectedEventArgs(DisconnectType.ConnectionClosed));
                }

                throw new ConnectionException(Connection.CurrentEndPoint, Connection.Protocol, "连接已断开");
            }

            Connection.Send(buffer);
        }
        #endregion

        #region 属性

        public int? SessionId { get; private set; }

        public ulong? SteamId { get; private set; }

        public ulong? SessionToken { get; private set; }

        public uint? CellId { get; private set; }

        public string? IPCountryCode { get; private set; }

        public IPAddress? PublicIP { get; private set; }

        public EndPoint? EndPoint => Connection?.CurrentEndPoint;

        public ProtocolTypes? Protocol => Connection?.Protocol;

        public EUniverse Universe => EUniverse.Public;

        public string MachineName => machineName;

        public ILogger? Logger { get; private set; } = new DefaultLogger();

        #endregion

        private enum ClientStatus
        {
            Close = 0,
            Open = 1
        }
    }
}