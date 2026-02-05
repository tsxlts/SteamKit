using SteamKit.Factory;

namespace SteamKit
{
    /// <summary>
    /// LoggerBuilder
    /// </summary>
    public class LoggerBuilder
    {
        private static ILogger internalLogger = new DefaultLogger();

        /// <summary>
        /// 设置日志处理器
        /// </summary>
        /// <param name="logger">Logger</param>
        public static void WithLogger(ILogger logger)
        {
            internalLogger = logger;
        }

        internal static ILogger GetLogger()
        {
            return internalLogger;
        }
    }
}
