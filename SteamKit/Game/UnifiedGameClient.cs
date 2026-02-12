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
        public delegate Task<LoginGameResponse> LoadingGameHanlder(GameClient client, CancellationToken cancellationToken);

        private LoadingGameHanlder loadingGameHanlder;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId">游戏Id</param>
        public UnifiedGameClient(uint appId) : this(appId: appId, version: 0, buildId: 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId">游戏Id</param>
        /// <param name="version">游戏版本</param>
        /// <param name="buildId">生成版本Id</param>
        public UnifiedGameClient(uint appId, uint version, uint buildId) : base(appId: appId, version: version, buildId: buildId)
        {
            loadingGameHanlder = DefaultLoadingGameAsync;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        public UnifiedGameClient WithLoadingGame(LoadingGameHanlder @event)
        {
            loadingGameHanlder = @event;
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
            return loadingGameHanlder.Invoke(this, cancellationToken);
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
