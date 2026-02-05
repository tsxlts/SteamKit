using System;
using HttpMonitor.Injector.Model;
using HttpMonitor.Monitors;

namespace HttpMonitor.Internal
{
    [Serializable]
    public class HttpMonitor : MarshalByRefObject, IHttpMonitor
    {
        public void ReportHttpRequest(string method, string url, string source)
        {
            Console.WriteLine($"[REQUEST] {DateTime.Now:HH:mm:ss.fff} {method} {url}");
            Console.WriteLine($"         Source: {source}");
        }

        public void ReportRequestHeaders(string headers)
        {
            if (!string.IsNullOrEmpty(headers))
            {
                Console.WriteLine($"         Request Headers:");
                foreach (var line in headers.Split('\n'))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        Console.WriteLine($"           {line.Trim()}");
                }
            }
        }

        public void ReportPostData(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                Console.WriteLine($"         POST Data: {Truncate(data, 200)}");
            }
        }

        public void ReportResponseInfo(int statusCode, string headers)
        {
            Console.WriteLine($"         Response: {statusCode}");
            if (!string.IsNullOrEmpty(headers))
            {
                Console.WriteLine($"         Response Headers:");
                foreach (var line in headers.Split('\n'))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        Console.WriteLine($"           {line.Trim()}");
                }
            }
        }

        public void ReportResponseChunk(string chunk)
        {
            // 小数据块实时显示
            if (!string.IsNullOrEmpty(chunk) && chunk.Length < 100)
            {
                Console.WriteLine($"         Response Chunk: {chunk}");
            }
        }

        public void ReportCompleteRequest(RequestCompleteData data)
        {
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine($"COMPLETE HTTP TRANSACTION - {data.Method} {data.Url}");
            Console.WriteLine($"Duration: {data.DurationMs}ms | Status: {data.StatusCode} | Process: {data.SourceProcess}");
            Console.WriteLine($"Time: {data.StartTime:HH:mm:ss.fff} -> {data.EndTime:HH:mm:ss.fff}");

            if (!string.IsNullOrEmpty(data.RequestHeaders))
            {
                Console.WriteLine("\nREQUEST HEADERS:");
                Console.WriteLine(data.RequestHeaders);
            }

            if (!string.IsNullOrEmpty(data.RequestBody))
            {
                Console.WriteLine("\nREQUEST BODY:");
                Console.WriteLine(Truncate(data.RequestBody, 1000));
            }

            if (!string.IsNullOrEmpty(data.ResponseHeaders))
            {
                Console.WriteLine("\nRESPONSE HEADERS:");
                Console.WriteLine(data.ResponseHeaders);
            }

            if (!string.IsNullOrEmpty(data.ResponseBody))
            {
                Console.WriteLine("\nRESPONSE BODY:");
                Console.WriteLine(Truncate(data.ResponseBody, 2000));
            }
            Console.WriteLine("=".PadRight(80, '='));
        }

        public void LogMessage(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        }

        public void ReportError(string error)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {error}");
        }

        public void Ping()
        {
            LogMessage($"Ping ...");
        }

        public void IsInstalled(int processId)
        {
            LogMessage($"监控器已安装到进程：{processId}");
        }

        /// <summary>
        /// 重写此方法确保对象不过期
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null; // 返回 null 使对象永不过期
        }

        private string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            {
                return text;
            }

            return text.Substring(0, maxLength) + $"... [Truncated, total length: {text.Length}]";
        }
    }


}
