using System.Text;
using SteamKit.Client;
using SteamKit.Client.Hanlders;
using SteamKit.Client.Internal;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Cloud;
using SteamKit.Client.Model.Proto;
using SteamKit.Client.Server;
using SteamKit.Internal;
using SteamKit.Types;

namespace SteamKit.Game
{
    /// <summary>
    /// GameClient
    /// </summary>
    public abstract class GameClient : SteamClient
    {
        /// <summary>
        /// 开始登录游戏事件
        /// 登陆游戏前执行
        /// </summary>
        /// <param name="arg"></param>
        public delegate Task<LoginGameResponse> LoginGameExecutingEventHandler(GameClient arg);

        private readonly uint appId;
        private readonly uint version;
        private readonly uint buildId;
        private readonly EOSType osType;
        private readonly GameClientOptions options;
        private readonly ulong startTime;
        private event LoginGameExecutingEventHandler loginGameExecuting;

        private bool gameLoaded = false;
        private ulong currentSequential = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="version"></param>
        /// <param name="buildId"></param>
        public GameClient(uint appId, uint version, uint buildId)
        {
            this.appId = appId;
            this.version = version;
            this.loginGameExecuting = (arg) => Task.FromResult(new LoginGameResponse { Success = true, Error = null });
            this.buildId = buildId;
            this.osType = SteamHelpers.GetOSType();

            this.options = new GameClientOptions();

            this.startTime = (ulong)Extensions.GetSystemTimestamp();

            WithDisconnected((sends, args) =>
            {
                gameLoaded = false;

                return Task.CompletedTask;
            }).WithLogoff((sends, args) =>
            {
                gameLoaded = false;

                return Task.CompletedTask;
            }).RegistCallback(EMsg.ClientPlayingSessionState, (sender, response) =>
            {
                var msg = new ServerProtoBufMsg<CMsgClientPlayingSessionState>(response.PacketResult!);

                /*
                hasPlayingSession = msg.Body.playing_blocked && msg.Body.playing_app == AppId;
                if (hasPlayingSession)
                {
                    Logger?.LogDebug($"Already logged into the game elsewhere");
                    Logger?.LogInformation($"Already logged into the game elsewhere");
                }
                */

                if (msg.Body.playing_app == 0 && GameLoaded)
                {
                    gameLoaded = false;
                }

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 配置游戏选项
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public GameClient Configure(Action<GameClientOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            configure.Invoke(options);
            return this;
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<LoginGameResponse> LoginGameAsync(CancellationToken cancellationToken = default)
        {
            if (!this.IsConnected())
            {
                throw new InvalidOperationException("Connection is null.");
            }

            Logger?.LogDebug("Preparing login game...");
            Logger?.LogInformation("Preparing login game...");

            var beginLoginResult = await BeginLoginGameInternalAsync(cancellationToken).ConfigureAwait(false);
            if (!beginLoginResult.Success)
            {
                Logger?.LogDebug($"Login game canceled: {beginLoginResult.Error}");
                Logger?.LogInformation($"Login game canceled: {beginLoginResult.Error}");

                return beginLoginResult;
            }

            Logger?.LogDebug("Waiting for login game...");
            Logger?.LogInformation("Waiting for login game...");

            var loginResult = await LoginGameInternalAsync(cancellationToken).ConfigureAwait(false);
            if (!loginResult.Success)
            {
                Logger?.LogDebug($"Login game failed: {loginResult.Error}");
                Logger?.LogInformation($"Login game failed: {loginResult.Error}");

                return loginResult;
            }

            Logger?.LogDebug("Waiting for game loading...");
            Logger?.LogInformation("Waiting for game loading...");

            var loadResult = await LoadingGameInternalAsync(cancellationToken).ConfigureAwait(false);
            if (!loadResult.Success)
            {
                Logger?.LogDebug($"Game loading failed: {loadResult.Error}");
                Logger?.LogInformation($"Game loading failed: {loadResult.Error}");

                return loadResult;
            }

            Logger?.LogDebug("Game loaded successfully");
            Logger?.LogInformation("Game loaded successfully");

            gameLoaded = true;

            return loadResult;
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public virtual async Task LogoutGameAsync(CancellationToken cancellationToken = default)
        {
            var playGame = new ClientProtoBufMsg<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayedWithDataBlob);
            playGame.Body.client_os_type = (uint)OSType;
            playGame.Body.recent_reauthentication = false;
            playGame.Body.cloud_gaming_platform = 0;
            await SendAsync(playGame, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 踢掉其他地方已登录游戏的会话
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task KickPlayingSessionAsync(CancellationToken cancellationToken = default)
        {
            var clientMsg = new ClientProtoBufMsg<CMsgClientKickPlayingSession>(EMsg.ClientKickPlayingSession)
            {
                Body = new CMsgClientKickPlayingSession
                {
                    only_stop_game = false,
                }
            };
            await SendAsync(clientMsg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 申请免费游戏许可证
        /// 注册免费游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<ClientResult<CMsgClientRequestFreeLicenseResponse>> RequestFreeLicenseAsync(CancellationToken cancellationToken = default)
        {
            var result = await SendAsync<CMsgClientRequestFreeLicense, CMsgClientRequestFreeLicenseResponse>(EMsg.ClientRequestFreeLicense, new CMsgClientRequestFreeLicense
            {
                appids = { this.AppId }
            }, cancellationToken: cancellationToken);

            return new ClientResult<CMsgClientRequestFreeLicenseResponse>
            {
                ErrorCode = result.EResult,
                ErrorMessage = result.ErrorMessage,
                Result = result.Result,
            };
        }

        /// <summary>
        /// 注册开始登录游戏事件
        /// 登陆游戏前执行
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public GameClient OnLoginGameExecuting(LoginGameExecutingEventHandler handler)
        {
            this.loginGameExecuting = handler;
            return this;
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <typeparam name="MsgType"></typeparam>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public GameClient RegistGCCallback<MsgType>(MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            RegistGCCallback(AppId, msgType, callback);
            return this;
        }

        /// <summary>
        /// 移除消息回调事件
        /// </summary>
        /// <typeparam name="MsgType"></typeparam>
        /// <param name="msgType">MsgType</param>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public GameClient RemoveGCCallback<MsgType>(MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            RemoveGCCallback(AppId, msgType, callback);
            return this;
        }

        /// <summary>
        /// 等待游戏初始化完成
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public virtual async Task WaitInitAsync(CancellationToken cancellationToken = default)
        {
            var delay = 10;
            while (!gameLoaded)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(delay, cancellationToken);
                delay = Math.Min(delay * 2, 500);
            }
        }

        /// <summary>
        /// 开始登录游戏
        /// 登录游戏前检查
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        protected virtual async Task<LoginGameResponse> BeginLoginGameInternalAsync(CancellationToken cancellationToken)
        {
            var loginGame = await loginGameExecuting.Invoke(this).ConfigureAwait(false);
            if (!loginGame.Success)
            {
                return loginGame;
            }

            var userHandler = GetHandler<SteamUserHandler>();

            var message = new CCloud_AppLaunchIntent_Request
            {
                appid = AppId,
                client_id = GetClientId(),
                device_type = 1,
                ignore_pending_operations = false,
                machine_name = this.MachineName,
                os_type = (int)this.OSType,
            };

            var appLaunchIntentResponse = await ServiceMethodCallAsync((ICloud server) => server.SignalAppLaunchIntent(message), cancellationToken: cancellationToken).ConfigureAwait(false);
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (appLaunchIntentResponse.EResult == EResult.TooManyPending)
                {
                    message.ignore_pending_operations = true;
                    appLaunchIntentResponse = await ServiceMethodCallAsync((ICloud server) => server.SignalAppLaunchIntent(message), cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                if (!userHandler.HasPlayingSession(out var playingSession))
                {
                    break;
                }

                if (!options.KickPlayingSession)
                {
                    return new LoginGameResponse
                    {
                        Success = false,
                        Error = $"Already logged into the game elsewhere, AppId: {playingSession.AppId}",
                    };
                }

                await KickPlayingSessionAsync(cancellationToken).ConfigureAwait(false);
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                break;

            } while (true);

            return new LoginGameResponse
            {
                Success = true,
                Error = null,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ulong GetClientId()
        {
            var sequential = Interlocked.Increment(ref currentSequential);
            var Id = new GlobalId
            {
                ProcessId = 0,
                BoxId = 0,
                Sequential = sequential,
                StartTime = startTime,
            };
            return Id;
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        protected virtual async Task<LoginGameResponse> LoginGameInternalAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<LoginGameResponse>();
            AsyncEventHandler<MessageCallback> handler = (sender, response) =>
            {
                var msg = new ServerProtoBufMsg<CMsgClientPlayingSessionState>(response.PacketResult!);
                if (msg.Body.playing_app == this.AppId && !msg.Body.playing_blocked)
                {
                    tcs.TrySetResult(new LoginGameResponse
                    {
                        Success = true,
                        Error = null
                    });
                }

                return Task.CompletedTask;
            };

            try
            {
                RegistCallback(EMsg.ClientPlayingSessionState, handler);

                /*
                var playGame = new ClientProtoBufMsg<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
                playGame.Body.recent_reauthentication = Reauthentication;
                playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
                {
                    game_id = AppId,
                });
                await SendAsync(playGame);
                */

                int pid = Environment.ProcessId;
                int ppid = Math.Max(ProcessingHelper.GetParentProcessId(pid), 0);

                Logger?.LogDebug($"ProcessId: {pid}");
                Logger?.LogInformation($"ProcessId: {pid}");
                Logger?.LogDebug($"ParentProcessId: {ppid}");
                Logger?.LogInformation($"ParentProcessId: {ppid}");

                uint process_id = (uint)pid;
                uint process_id_parent = (uint)ppid; //Steam进程Id
                uint launch_source = this.LaunchSource;
                uint game_build_id = this.BuildId;
                uint owner_id = (uint)(this.SteamId!.Value - Extensions.DefaultSteamId);

                var playGame = new ClientProtoBufMsg<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayedWithDataBlob);
                playGame.Body.client_os_type = (uint)this.OSType;
                playGame.Body.cloud_gaming_platform = 0;
                playGame.Body.recent_reauthentication = false;
                playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
                {
                    game_id = AppId,
                    process_id = process_id,
                    launch_source = launch_source,
                    game_build_id = game_build_id,
                    owner_id = owner_id,

                    is_secure = false,
                    steam_id_gs = 0,
                    game_port = 0,
                    game_extra_info = "",
                    game_flags = 0,
                    game_ip_address = null,
                    game_os_platform = -1,
                    beta_name = "",
                    dlc_context = 0,
                    launch_option_type = 0,
                    streaming_provider_id = 0,

                    process_id_list ={
                    new CMsgClientGamesPlayed.ProcessInfo
                    {
                        process_id = process_id,
                        process_id_parent = process_id_parent,
                        parent_is_steam = true,
                    }
                }
                });
                await SendAsync(playGame).ConfigureAwait(false);

                var result = await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
                return result ?? new LoginGameResponse
                {
                    Success = false,
                    Error = "Unknown",
                };
            }
            finally
            {
                RemoveCallback(EMsg.ClientPlayingSessionState, handler);
            }
        }

        /// <summary>
        /// 加载游戏
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        protected abstract Task<LoginGameResponse> LoadingGameInternalAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 游戏已退出
        /// </summary>
        protected void GameExited()
        {
            this.gameLoaded = false;

            ExitGame();
        }

        /// <summary>
        /// 游戏已退出
        /// </summary>
        protected virtual void ExitGame()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder value = new StringBuilder(base.ToString());
            value.AppendLine($"{AppId}#GameVersion:{Version}#Build:{BuildId}");
            return value.ToString();
        }

        /// <summary>
        /// 游戏选项
        /// </summary>
        public GameClientOptions Options => options;

        /// <summary>
        /// 是否已连接游戏
        /// </summary>
        /// <returns></returns>
        public bool GameLoaded => gameLoaded;

        /// <summary>
        /// AppId
        /// </summary>
        public uint AppId => appId;

        /// <summary>
        /// 版本
        /// </summary>
        public uint Version => version;

        /// <summary>
        /// 生成版本Id
        /// </summary>
        public uint BuildId => buildId;

        /// <summary>
        /// 操作系统
        /// </summary>
        public EOSType OSType => osType;

        /// <summary>
        /// 
        /// </summary>
        public uint LaunchSource => 100;
    }
}
