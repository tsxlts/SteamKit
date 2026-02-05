using System.Collections.Concurrent;
using SteamKit.Client.Internal.Model;

namespace SteamKit.Client.Internal
{
    internal class AutoReleaseAsyncJobCollection<TKey> where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, IAsyncJob> asyncJobs;
        private readonly Func<TKey, ulong> jobIdCreater;

        public AutoReleaseAsyncJobCollection(Func<TKey, ulong> jobIdCreater)
        {
            this.asyncJobs = new ConcurrentDictionary<TKey, IAsyncJob>();
            this.jobIdCreater = jobIdCreater;
        }

        public TaskReleaser<TValue> TryAdd<TValue>(TKey jobKey, CancellationToken cancellationToken)
        {
            var job = new AsyncJob<TValue>(jobIdCreater(jobKey), cancellationToken);
            if (!asyncJobs.TryAdd(jobKey, job))
            {
                job = null;
            }

            return new TaskReleaser<TValue>(asyncJobs, jobKey)
            {
                Job = job
            };
        }

        public TaskReleaser<TValue> AddOrUpdate<TValue>(TKey jobKey, CancellationToken cancellationToken)
        {
            var value = asyncJobs.AddOrUpdate(jobKey,
                key => new AsyncJob<TValue>(jobIdCreater(jobKey), cancellationToken),
                (key, value) => new AsyncJob<TValue>(jobIdCreater(jobKey), cancellationToken));

            var job = value as AsyncJob<TValue>;

            return new TaskReleaser<TValue>(asyncJobs, jobKey)
            {
                Job = job
            };
        }

        public bool SetResult<TValue>(TKey jobKey, TValue? result)
        {
            if (!asyncJobs.TryRemove(jobKey, out var value))
            {
                return false;
            }

            var job = value as AsyncJob<TValue>;

            return job?.SetResult(result) ?? false;
        }

        public bool SetException(TKey jobKey, Exception exception)
        {
            if (!asyncJobs.TryRemove(jobKey, out var value))
            {
                return false;
            }

            return value.SetException(exception);
        }

        public class TaskReleaser<TValue> : IDisposable
        {
            private readonly ConcurrentDictionary<TKey, IAsyncJob> asyncJobs;
            private readonly TKey jobKey;

            public TaskReleaser(ConcurrentDictionary<TKey, IAsyncJob> jobs, TKey key)
            {
                this.asyncJobs = jobs;
                this.jobKey = key;
            }

            public void Dispose()
            {
                asyncJobs.TryRemove(jobKey, out var job);
            }

            public AsyncJob<TValue>? Job { get; init; }
        }
    }
}
