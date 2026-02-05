using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class UnifiedGameClient : GameClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public delegate Task<LoginGameResponse> LoadingGameEvent(GameClient client, CancellationToken cancellationToken);

        private readonly LoginGameJob loginGameJob;

        private LoadingGameEvent loadingGameEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        public UnifiedGameClient(uint appId) : this(appId: appId, version: 0, buildId: 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="version"></param>
        /// <param name="buildId"></param>
        public UnifiedGameClient(uint appId, uint version, uint buildId) : base(appId: appId, version: version, buildId: buildId)
        {
            loginGameJob = new LoginGameJob();

            loadingGameEvent = DefaultLoadingGameAsync;

            RegistCallback(EMsg.ClientPlayingSessionState, (sender, response) =>
            {
                var msg = new ServerProtoBufMsg<CMsgClientPlayingSessionState>(response.PacketResult!);
                if (msg.Body.playing_app == this.AppId && !msg.Body.playing_blocked)
                {
                    loginGameJob.Task?.SetResult(new LoginGameResponse
                    {
                        Success = true,
                        Error = null
                    });
                }

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCUnifiedMsg.ClientWelcome, (sender, response) =>
            {
                var msg = new Client.Internal.Msg.GCServerProtoBufMsg(response.PacketResult!.MsgType, response.PacketResult.AppId, response.PacketResult.GetData());
                loginGameJob.Task?.SetResult(new LoginGameResponse
                {
                    Success = true,
                    Error = null
                });

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        public UnifiedGameClient WithLoadingGame(LoadingGameEvent @event)
        {
            loadingGameEvent = @event;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task<LoginGameResponse> LoadingGameInternalAsync(CancellationToken cancellationToken)
        {
            return loadingGameEvent.Invoke(this, cancellationToken);
        }

        private async Task<LoginGameResponse> DefaultLoadingGameAsync(GameClient client, CancellationToken cancellationToken)
        {
            using (await loginGameJob.Lock.LockAsync(cancellationToken))
            {
                loginGameJob.Task = new AsyncJob<LoginGameResponse>(AppId, cancellationToken);

                TimerCallback timerCallback = (obj) =>
                {
                    try
                    {
                        if (!this.IsConnected())
                        {
                            loginGameJob.Task?.SetResult(new LoginGameResponse
                            {
                                Success = false,
                                Error = $"Game loading failed, Connection dropped"
                            });
                            return;
                        }

                        var clientMsg = new GCClientProtoBufMsg<UnifiedClientHello>((uint)EGCUnifiedMsg.ClientHello);
                        clientMsg.Body.version = Version;
                        Send(AppId, clientMsg);
                    }
                    catch
                    {

                    }
                };
                using (var timer = new Timer(timerCallback, this, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(1000)))
                {
                    var result = await loginGameJob.Task.ConfigureAwait(false);
                    return result ?? new LoginGameResponse
                    {
                        Success = false,
                        Error = "Unknown",
                    };
                }
            }
        }
    }
}
