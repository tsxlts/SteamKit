using SteamKit.Factory;

namespace SteamKit.Client.Internal
{
    internal class MessageAsyncCallbackHandler<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageAsyncCallbackHandler()
        {
            Callback = WithoutCallback;
        }

        public MessageAsyncCallbackHandler(AsyncEventHandler<T> callback) : this()
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
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            Callback = WithoutCallback;
            Callback += callback;
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
            return CallbackInvoker.CallbackInvokeAsync(Callback, sender, param, logger);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="param"></param>
        /// <param name="logger"></param>
        public void Invoke(object? sender, T param, ILogger? logger)
        {
            CallbackInvoker.CallbackInvoke(Callback, sender, param, logger);
        }

        private static Task WithoutCallback(object? sender, T e)
        {
            return Task.CompletedTask;
        }
    }
}
