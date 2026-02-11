using SteamKit.Factory;

namespace SteamKit.Client.Internal
{
    internal static class CallbackInvoker
    {
        public static void CallbackInvoke<T>(EventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            var cb = callback;
            if (cb == null)
            {
                return;
            }

            try
            {
                cb(sender, param);
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);
            }
        }

        public static Task CallbackInvokeAsync<T>(EventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            var cb = callback;
            if (cb == null)
            {
                return Task.CompletedTask;
            }

            return Task.Run(() =>
            {
                try
                {
                    cb(sender, param);
                }
                catch (Exception ex)
                {
                    logger?.LogException(ex, null);
                }
            });
        }

        public static void CallbackInvoke<T>(AsyncEventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            var cb = callback;
            if (cb == null)
            {
                return;
            }

            try
            {
                cb(sender, param).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);
            }
        }

        public static Task CallbackInvokeAsync<T>(AsyncEventHandler<T>? callback, object? sender, T param, ILogger? logger)
        {
            var cb = callback;
            if (cb == null)
            {
                return Task.CompletedTask;
            }

            try
            {
                return cb(sender, param);
            }
            catch (Exception ex)
            {
                logger?.LogException(ex, null);

                return Task.FromException(ex);
            }
        }
    }
}
