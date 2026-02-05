namespace SteamKit.Factory
{
    internal class DefaultLogger : ILogger
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogInformation(string format, params object?[]? args)
        {

        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogError(string? format, params object?[]? args)
        {

        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogWarning(string? format, params object?[]? args)
        {

        }

        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogDebug(string format, params object?[]? args)
        {

        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogException(Exception exception, string? format, params object?[]? args)
        {

        }
    }
}
