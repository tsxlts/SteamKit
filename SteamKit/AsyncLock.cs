namespace SteamKit
{
    /// <summary>
    /// 异步锁
    /// </summary>
    public class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore;

        /// <summary>
        /// 构造函数
        /// 最多同时允许一个线程获取锁
        /// </summary>
        public AsyncLock() : this(1, 1)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialCount">可以同时获取锁的初始请求数</param>
        /// <param name="maxCount">可以同时获取锁的最大请求数</param>
        public AsyncLock(int initialCount, int maxCount)
        {
            _semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <returns></returns>
        public IDisposable Lock()
        {
            _semaphore.Wait();
            return new Releaser(_semaphore) { Entered = true };
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public IDisposable Lock(CancellationToken cancellationToken = default)
        {
            _semaphore.Wait(cancellationToken);
            return new Releaser(_semaphore) { Entered = true };
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public Releaser Lock(TimeSpan timeout)
        {
            long totalMilliseconds = (long)timeout.TotalMilliseconds;
            if (totalMilliseconds < -1 || totalMilliseconds > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), timeout, $"超时时间不能小于-1并且不能超过{int.MaxValue}");
            }

            return Lock((int)timeout.TotalMilliseconds);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时时间</param>
        /// <returns></returns>
        public Releaser Lock(int millisecondsTimeout)
        {
            var entered = _semaphore.Wait(millisecondsTimeout);
            return new Releaser(_semaphore) { Entered = entered };
        }

        /// <summary>
        /// 异步等待获取锁
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<IDisposable> LockAsync(CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken);
            return new Releaser(_semaphore) { Entered = true };
        }

        /// <summary>
        /// 异步等待获取锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<Releaser> LockAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            long totalMilliseconds = (long)timeout.TotalMilliseconds;
            if (totalMilliseconds < -1 || totalMilliseconds > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), timeout, $"超时时间不能小于-1并且不能超过{int.MaxValue}");
            }

            return await LockAsync((int)timeout.TotalMilliseconds, cancellationToken);
        }

        /// <summary>
        /// 异步等待获取锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<Releaser> LockAsync(int millisecondsTimeout, CancellationToken cancellationToken = default)
        {
            var entered = await _semaphore.WaitAsync(millisecondsTimeout, cancellationToken);
            return new Releaser(_semaphore) { Entered = entered };
        }

        /// <summary>
        /// 锁释放器
        /// </summary>
        public class Releaser : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            internal Releaser(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            /// <summary>
            /// 释放锁
            /// </summary>
            public void Dispose()
            {
                if (!Entered)
                {
                    return;
                }

                _semaphore.Release();
            }

            /// <summary>
            /// 是否成功获取锁
            /// </summary>
            public bool Entered { get; internal set; } = false;
        }
    }
}
