using System.Collections.Concurrent;

namespace SteamKit.Internal
{
    internal class AsyncJobReleaser<TKey, TValue> : CollectionReleaser<ConcurrentDictionary<TKey, TValue>> where TKey : notnull
    {
        public AsyncJobReleaser(ConcurrentDictionary<TKey, TValue> collection, TKey key) : base(collection, c =>
        {
            c.TryRemove(key, out var _);
        })
        {
        }
    }
}
