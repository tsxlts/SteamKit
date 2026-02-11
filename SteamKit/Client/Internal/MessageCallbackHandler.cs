using SteamKit.Factory;

namespace SteamKit.Client.Internal
{
    internal class MessageCallbackHandler<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageCallbackHandler()
        {
            Callback = WithoutCallback;
        }

        public MessageCallbackHandler(EventHandler<T> callback) : this()
        {
            Callback += callback;
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<T> Callback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public MessageCallbackHandler<T> SetCallback(EventHandler<T> callback)
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

        private static void WithoutCallback(object? sender, T e)
        {
        }
    }
}
