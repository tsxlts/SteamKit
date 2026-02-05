using SteamKit.Factory;

namespace SteamKit.Client.Internal
{
    internal static class CallbackInvoker
    {
        public static void CallbackInvoke<T>(Action<T>? callback, T param, ILogger? logger)
        {
            try
            {
                callback?.Invoke(param);
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);
            }
        }

        public static Task CallbackInvokeAsync<T>(Action<T>? callback, T param, ILogger? logger)
        {
            return Task.Run(() =>
            {
                try
                {
                    callback?.Invoke(param);
                }
                catch (Exception ex)
                {
                    logger?.LogException(ex, null);
                }
            });
        }

        public static void CallbackInvoke<T>(EventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            try
            {
                callback?.Invoke(sender, param);
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);
            }
        }

        public static Task CallbackInvokeAsync<T>(EventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            return Task.Run(() =>
            {
                try
                {
                    callback?.Invoke(sender, param);
                }
                catch (Exception ex)
                {
                    logger?.LogException(ex, null);
                }
            });
        }
    }
}
