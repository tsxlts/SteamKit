using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using EasyHook;
using HttpMonitor.Extension;
using HttpMonitor.Injector.Model;
using HttpMonitor.Monitors;

namespace HttpMonitor.Hooks
{
    internal class WinHttpHook
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        delegate IntPtr WinHttpOpenRequestDelegate(IntPtr hConnect, string lpszVerb, string lpszObjectName, string lpszVersion, string lpszReferrer, string lplpszAcceptTypes, uint dwFlags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        delegate bool WinHttpSendRequestDelegate(IntPtr hRequest, string lpszHeaders, int dwHeadersLength, IntPtr lpOptional, int dwOptionalLength, int dwTotalLength, IntPtr dwContext);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate bool WinHttpReceiveResponseDelegate(IntPtr hRequest, IntPtr lpReserved);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate bool WinHttpReadDataDelegate(IntPtr hRequest, IntPtr lpBuffer, int dwNumberOfBytesToRead, out int lpdwNumberOfBytesRead);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate bool WinHttpCloseHandleDelegate(IntPtr hInternet);

        private const uint WINHTTP_QUERY_STATUS_CODE = 19;
        private const uint WINHTTP_QUERY_RAW_HEADERS_CRLF = 22;

        private readonly Dictionary<IntPtr, HttpRequestContext> _requestContexts = new Dictionary<IntPtr, HttpRequestContext>();
        private readonly object _contextLock = new object();

        private readonly IHttpMonitor monitor;

        private LocalHook _winHttpOpenRequestHook;
        private LocalHook _winHttpSendRequestHook;
        private LocalHook _winHttpReceiveResponseHook;
        private LocalHook _winHttpReadDataHook;
        private LocalHook _winHttpCloseHandleHook;

        public WinHttpHook(IHttpMonitor monitor)
        {
            this.monitor = monitor;
        }

        public void SetupHooks()
        {
            try
            {
                monitor?.LogMessage("开始设置 HTTP 挂钩");

                // 1. 请求创建挂钩
                _winHttpOpenRequestHook = LocalHook.Create(LocalHook.GetProcAddress("winhttp.dll", "WinHttpOpenRequest"), new WinHttpOpenRequestDelegate(WinHttpOpenRequestHooked), this);
                _winHttpOpenRequestHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                // 2. 请求发送挂钩
                _winHttpSendRequestHook = LocalHook.Create(LocalHook.GetProcAddress("winhttp.dll", "WinHttpSendRequest"), new WinHttpSendRequestDelegate(WinHttpSendRequestHooked), this);
                _winHttpSendRequestHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                // 3. 响应接收挂钩
                _winHttpReceiveResponseHook = LocalHook.Create(LocalHook.GetProcAddress("winhttp.dll", "WinHttpReceiveResponse"), new WinHttpReceiveResponseDelegate(WinHttpReceiveResponseHooked), this);
                _winHttpReceiveResponseHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                // 4. 数据读取挂钩
                _winHttpReadDataHook = LocalHook.Create(LocalHook.GetProcAddress("winhttp.dll", "WinHttpReadData"), new WinHttpReadDataDelegate(WinHttpReadDataHooked), this);
                _winHttpReadDataHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                // 5. 句柄关闭挂钩（用于清理）
                _winHttpCloseHandleHook = LocalHook.Create(LocalHook.GetProcAddress("winhttp.dll", "WinHttpCloseHandle"), new WinHttpCloseHandleDelegate(WinHttpCloseHandleHooked), this);
                _winHttpCloseHandleHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                monitor?.LogMessage("HTTP 挂钩设置完成，监控已启动");
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"挂钩设置失败: {ex.Message}");
                throw;
            }
        }

        public void CleanupClosedHandles()
        {
            var now = DateTime.Now;
            var toRemove = new List<IntPtr>();

            lock (_contextLock)
            {
                foreach (var kvp in _requestContexts)
                {
                    if ((now - kvp.Value.StartTime).TotalMinutes > 5)
                    {
                        toRemove.Add(kvp.Key);
                    }
                }

                foreach (var handle in toRemove)
                {
                    _requestContexts.Remove(handle);
                }
            }
        }

        private IntPtr WinHttpOpenRequestHooked(IntPtr hConnect, string lpszVerb, string lpszObjectName, string lpszVersion, string lpszReferrer, string lplpszAcceptTypes, uint dwFlags)
        {
            var requestHandle = WindowsApi.WinHttpOpenRequest(hConnect, lpszVerb, lpszObjectName, lpszVersion, lpszReferrer, lplpszAcceptTypes, dwFlags);

            if (requestHandle != IntPtr.Zero)
            {
                var context = new HttpRequestContext
                {
                    Method = lpszVerb ?? "GET",
                    Url = lpszObjectName ?? "",
                    StartTime = DateTime.Now,
                    RequestHeaders = new Dictionary<string, string>(),
                    ResponseBodyChunks = new List<byte[]>()
                };

                _requestContexts[requestHandle] = context;

                monitor.ReportHttpRequest(context.Method, context.Url, "请求已创建");
            }

            return requestHandle;
        }

        private bool WinHttpSendRequestHooked(IntPtr hRequest, string lpszHeaders, int dwHeadersLength, IntPtr lpOptional, int dwOptionalLength, int dwTotalLength, IntPtr dwContext)
        {
            if (_requestContexts.TryGetValue(hRequest, out var context))
            {
                // 捕获请求头
                if (!string.IsNullOrEmpty(lpszHeaders))
                {
                    ParseAndStoreHeaders(lpszHeaders, context.RequestHeaders);
                    monitor.ReportRequestHeaders(lpszHeaders);
                }

                // 捕获 POST 数据
                if (lpOptional != IntPtr.Zero && dwOptionalLength > 0)
                {
                    byte[] postData = new byte[dwOptionalLength];
                    Marshal.Copy(lpOptional, postData, 0, dwOptionalLength);
                    context.RequestBody = postData;

                    string postDataText = Encoding.UTF8.GetString(postData);
                    monitor.ReportPostData(postDataText);
                }
            }

            return WindowsApi.WinHttpSendRequest(hRequest, lpszHeaders, dwHeadersLength, lpOptional, dwOptionalLength, dwTotalLength, dwContext);
        }

        private bool WinHttpReceiveResponseHooked(IntPtr hRequest, IntPtr lpReserved)
        {
            bool result = WindowsApi.WinHttpReceiveResponse(hRequest, lpReserved);

            if (result && _requestContexts.TryGetValue(hRequest, out var context))
            {
                // 获取响应状态码
                int statusCode = GetResponseStatusCode(hRequest);
                context.ResponseStatusCode = statusCode;

                // 获取响应头
                string responseHeaders = GetResponseHeaders(hRequest);
                monitor.ReportResponseInfo(statusCode, responseHeaders);
            }

            return result;
        }

        private bool WinHttpReadDataHooked(IntPtr hRequest, IntPtr lpBuffer, int dwNumberOfBytesToRead, out int lpdwNumberOfBytesRead)
        {
            bool result = WindowsApi.WinHttpReadData(hRequest, lpBuffer, dwNumberOfBytesToRead, out lpdwNumberOfBytesRead);

            if (result && lpdwNumberOfBytesRead > 0 && _requestContexts.TryGetValue(hRequest, out var context))
            {
                // 捕获响应体数据
                byte[] responseData = new byte[lpdwNumberOfBytesRead];
                Marshal.Copy(lpBuffer, responseData, 0, lpdwNumberOfBytesRead);
                context.ResponseBodyChunks.Add(responseData);

                // 实时报告响应数据
                if (lpdwNumberOfBytesRead < 1024) // 只报告小数据块
                {
                    string chunkText = Encoding.UTF8.GetString(responseData);
                    monitor.ReportResponseChunk(chunkText);
                }
            }

            return result;
        }

        private bool WinHttpCloseHandleHooked(IntPtr hInternet)
        {
            try
            {
                if (hInternet != IntPtr.Zero)
                {
                    lock (_contextLock)
                    {
                        if (_requestContexts.TryGetValue(hInternet, out var context))
                        {
                            context.EndTime = DateTime.Now;

                            // 报告完整的请求数据
                            ReportCompleteRequestData(context);

                            _requestContexts.Remove(hInternet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"CloseHandle 挂钩错误: {ex.Message}");
            }

            return WindowsApi.WinHttpCloseHandle(hInternet);
        }

        private void ParseAndStoreHeaders(string headers, Dictionary<string, string> headerDict)
        {
            // 解析HTTP头信息的实现
            var lines = headers.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    headerDict[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        private int GetResponseStatusCode(IntPtr hRequest)
        {
            try
            {
                uint bufferLength = sizeof(int);
                IntPtr buffer = Marshal.AllocHGlobal((int)bufferLength);

                if (WindowsApi.WinHttpQueryHeaders(hRequest, WINHTTP_QUERY_STATUS_CODE | 0x20000000, IntPtr.Zero, buffer, ref bufferLength, IntPtr.Zero))
                {
                    string statusText = Marshal.PtrToStringUni(buffer, (int)bufferLength / 2);
                    if (int.TryParse(statusText, out int statusCode))
                    {
                        Marshal.FreeHGlobal(buffer);
                        return statusCode;
                    }
                }

                Marshal.FreeHGlobal(buffer);
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"获取状态码失败: {ex.Message}");
            }

            return 0;
        }

        private string GetResponseHeaders(IntPtr hRequest)
        {
            try
            {
                uint bufferLength = 0;

                // 第一次调用获取所需缓冲区大小
                WindowsApi.WinHttpQueryHeaders(hRequest, WINHTTP_QUERY_RAW_HEADERS_CRLF, IntPtr.Zero, IntPtr.Zero, ref bufferLength, IntPtr.Zero);

                if (bufferLength > 0)
                {
                    IntPtr buffer = Marshal.AllocHGlobal((int)bufferLength);

                    if (WindowsApi.WinHttpQueryHeaders(hRequest, WINHTTP_QUERY_RAW_HEADERS_CRLF, IntPtr.Zero, buffer, ref bufferLength, IntPtr.Zero))
                    {
                        string headers = Marshal.PtrToStringUni(buffer, (int)bufferLength / 2);
                        Marshal.FreeHGlobal(buffer);
                        return headers;
                    }

                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"获取响应头失败: {ex.Message}");
            }

            return string.Empty;
        }

        private void ReportCompleteRequestData(HttpRequestContext context)
        {
            try
            {
                var completeData = new RequestCompleteData
                {
                    Method = context.Method,
                    Url = context.Url,
                    RequestHeaders = context.RequestHeadersString,
                    RequestBody = context.RequestBody != null ?
                        Encoding.UTF8.GetString(context.RequestBody) : string.Empty,
                    StatusCode = context.ResponseStatusCode,
                    ResponseHeaders = context.ResponseHeadersString,
                    ResponseBody = context.GetResponseBody(),
                    DurationMs = (long)(context.EndTime - context.StartTime).TotalMilliseconds,
                    StartTime = context.StartTime,
                    EndTime = context.EndTime,
                    SourceProcess = context.ProcessName
                };

                monitor?.ReportCompleteRequest(completeData);
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"报告完整请求数据失败: {ex.Message}");
            }
        }

        public class HttpRequestContext
        {
            public IntPtr RequestHandle { get; set; }

            public string Method { get; set; }

            public string Url { get; set; }

            public Dictionary<string, string> RequestHeaders { get; set; }

            public string RequestHeadersString { get; set; }

            public byte[] RequestBody { get; set; }

            public int ResponseStatusCode { get; set; }

            public string ResponseHeadersString { get; set; }

            public List<byte[]> ResponseBodyChunks { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }

            public string ProcessName { get; set; }

            public string GetResponseBody()
            {
                if (ResponseBodyChunks == null || ResponseBodyChunks.Count == 0)
                    return string.Empty;

                try
                {
                    int totalLength = 0;
                    foreach (var chunk in ResponseBodyChunks)
                    {
                        totalLength += chunk.Length;
                    }

                    byte[] fullBody = new byte[totalLength];
                    int offset = 0;

                    foreach (var chunk in ResponseBodyChunks)
                    {
                        Buffer.BlockCopy(chunk, 0, fullBody, offset, chunk.Length);
                        offset += chunk.Length;
                    }

                    return Encoding.UTF8.GetString(fullBody);
                }
                catch (Exception)
                {
                    return "[Unable to decode response body]";
                }
            }
        }
    }
}
