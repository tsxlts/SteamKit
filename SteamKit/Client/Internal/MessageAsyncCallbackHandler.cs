using SteamKit.Factory;

namespace SteamKit.Client.Internal
{
    internal class MessageAsyncCallbackHandler<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageAsyncCallbackHandler() : this(WithoutCallback)
        {
        }

        public MessageAsyncCallbackHandler(AsyncEventHandler<T> callback)
        {
            Callback += callback;
        }

        /// <summary>
        /// 
        /// </summary>
        public event AsyncEventHandler<T> Callback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public MessageAsyncCallbackHandler<T> SetCallback(AsyncEventHandler<T> callback)
        {
            Callback = callback;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="param"></param>
        /// <param name="logger"></param>
        public Task InvokeAsync(object? sender, T param, ILogger? logger)
        {
            try
            {
                return Callback.Invoke(sender, param);
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);

                return Task.FromException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="param"></param>
        /// <param name="logger"></param>
        public void Invoke(object? sender, T param, ILogger? logger)
        {
            try
            {
                Callback.Invoke(sender, param).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);
            }
        }

        private static Task WithoutCallback(object? sender, T e)
        {
            return Task.CompletedTask;
        }
    }
}
