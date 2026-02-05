using System.Runtime.CompilerServices;

namespace SteamKit.Client.Internal.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsyncJob<T> : IAsyncJob
    {
        private object _locker = new object();
        private readonly TaskCompletionSource<T?> tcs;
        private readonly CancellationToken token;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        public AsyncJob(ulong jobId) : this(jobId, default)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="cancellationToken"></param>
        public AsyncJob(ulong jobId, CancellationToken cancellationToken)
        {
            tcs = new TaskCompletionSource<T?>(TaskCreationOptions.RunContinuationsAsynchronously);
            JobId = jobId;
            token = cancellationToken;

            token.Register(() =>
            {
                SetCancel();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong JobId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<T?> ToTask() => tcs.Task;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T?> WaitAsync(CancellationToken cancellationToken)
        {
            return tcs.Task.WaitAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Task<T?> WaitAsync(TimeSpan timeout)
        {
            return tcs.Task.WaitAsync(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskAwaiter<T?> GetAwaiter()
        {
            return tcs.Task.GetAwaiter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="continueOnCapturedContext"></param>
        /// <returns></returns>
        public ConfiguredTaskAwaitable<T?> ConfigureAwait(bool continueOnCapturedContext)
        {
            return tcs.Task.ConfigureAwait(continueOnCapturedContext);
        }

        public static implicit operator Task<T?>(AsyncJob<T?> asyncJob)
        {
            return asyncJob.ToTask();
        }

        public bool SetResult(T? result)
        {
            lock (_locker)
            {
                if (tcs.Task.IsCompleted)
                {
                    return false;
                }
                return tcs.TrySetResult(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool SetResult(object result)
        {
            if (result == null)
            {
                return SetResult(default(T));
            }

            if (result is T t)
            {
                return SetResult(t);
            }

            throw new ArgumentException($"无法将{result.GetType()}设置为{typeof(T)}", nameof(result));
        }

        public bool SetException(Exception exception)
        {
            lock (_locker)
            {
                if (tcs.Task.IsCompleted)
                {
                    return false;
                }
                return tcs.TrySetException(exception);
            }
        }

        public bool SetCancel()
        {
            lock (_locker)
            {
                if (tcs.Task.IsCompleted)
                {
                    return false;
                }
                return tcs.TrySetCanceled();
            }
        }
    }
}
