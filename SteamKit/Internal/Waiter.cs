
namespace SteamKit.Internal
{
    internal class Waiter
    {
        public Waiter()
        {
        }

        public async Task WaitAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            using (var slim = new SemaphoreSlim(0, 1))
            {
                try
                {
                    await slim.WaitAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {

                }
            }
        }
    }
}
