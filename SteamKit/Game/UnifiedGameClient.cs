using SteamKit.Client.Model;

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
            loadingGameEvent = DefaultLoadingGameAsync;
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

        private Task<LoginGameResponse> DefaultLoadingGameAsync(GameClient client, CancellationToken cancellationToken)
        {
            return Task.FromResult(new LoginGameResponse
            {
                Success = true,
                Error = null
            });
        }
    }
}
