namespace SteamKit.Internal
{
    internal static class TaskExtension
    {
        public static async Task WaitAsync(this TaskCompletionSource tcs, CancellationToken cancellationToken)
        {
            await tcs.Task.WaitAsync(cancellationToken);
        }

        public static async Task<T> WaitAsync<T>(this TaskCompletionSource<T> tcs, CancellationToken cancellationToken)
        {
            return await tcs.Task.WaitAsync(cancellationToken);
        }
    }
}
