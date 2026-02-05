using SteamKit.Factory;

namespace 解绑邮箱.Factory
{
    internal class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
        }

        public void LogInformation(string format, params object?[]? args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}：{format}", args);
            Console.ResetColor();
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogError(string? format, params object?[]? args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (!string.IsNullOrWhiteSpace(format))
            {
                Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}：{format}", args);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogWarning(string? format, params object?[]? args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!string.IsNullOrWhiteSpace(format))
            {
                Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}：{format}", args);
            }
            Console.ResetColor();
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
        /// Debug日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogException(Exception exception, string? format, params object?[]? args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (!string.IsNullOrWhiteSpace(format))
            {
                Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}：{format}", args);
            }
            if (exception != null)
            {
                Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}：{exception}");
            }
            Console.ResetColor();
        }
    }
}
