using System.Collections.Concurrent;
using SteamKit.Client.Internal.Model;

namespace SteamKit.Client.Internal
{
    internal class AsyncJobCollection<T>
    {
        private readonly ConcurrentDictionary<ulong, AsyncJob<T>> asyncJobs;

        public AsyncJobCollection()
        {
            asyncJobs = new ConcurrentDictionary<ulong, AsyncJob<T>>();
        }

        public bool TryAdd(AsyncJob<T> job)
        {
            return asyncJobs.TryAdd(job.JobId, job);
        }

        public bool SetResult(ulong jobId, T result)
        {
            if (!asyncJobs.TryRemove(jobId, out var job))
            {
                return false;
            }

            return job.SetResult(result);
        }

        public bool SetException(ulong jobId, Exception exception)
        {
            if (!asyncJobs.TryRemove(jobId, out var job))
            {
                return false;
            }

            return job.SetException(exception);
        }

        public bool Heartbeat(ulong jobId)
        {
            return true;
        }

        public void CancelPendingJobs()
        {
            foreach (var job in asyncJobs.Values)
            {
                job.SetCancel();
            }
            asyncJobs.Clear();
        }
    }
}
