using System;
using System.Runtime.InteropServices;
using System.Text;
using EasyHook;
using HttpMonitor.Extension;
using HttpMonitor.Monitors;

namespace HttpMonitor.Injector.Hooks
{
    internal class WinsockHook
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int SendDelegate(IntPtr socket, byte[] buf, int len, int flags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int SendToDelegate(IntPtr socket, byte[] buf, int len, int flags, IntPtr to, int tolen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int RecvDelegate(IntPtr socket, byte[] buf, int len, int flags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int RecvFromDelegate(IntPtr socket, byte[] buf, int len, int flags, IntPtr from, IntPtr fromlen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int ConnectDelegate(IntPtr socket, IntPtr name, int namelen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int WSASendDelegate(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesSent, int flags, IntPtr overlapped, IntPtr completionRoutine);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        delegate int WSARecvDelegate(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesRecvd, ref int flags, IntPtr overlapped, IntPtr completionRoutine);

        private readonly IHttpMonitor monitor;

        private LocalHook _sendHook;
        private LocalHook _sendToHook;
        private LocalHook _recvHook;
        private LocalHook _recvFromHook;
        private LocalHook _connectHook;
        private LocalHook _wsaSendHook;
        private LocalHook _wsaRecvHook;

        public WinsockHook(IHttpMonitor monitor)
        {
            this.monitor = monitor;
        }

        public void SetupHooks()
        {
            try
            {
                monitor?.LogMessage("开始设置 Winsock 挂钩");

                _sendHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "send"), new SendDelegate(Hooked_send), this);
                _sendHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _sendToHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "sendto"), new SendToDelegate(Hooked_sendto), this);
                _sendToHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _recvHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "recv"), new RecvDelegate(Hooked_recv), this);
                _recvHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _recvFromHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "recvfrom"), new RecvFromDelegate(Hooked_recvfrom), this);
                _recvFromHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _connectHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "connect"), new ConnectDelegate(Hooked_connect), this);
                _connectHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _wsaSendHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "WSASend"), new WSASendDelegate(Hooked_WSASend), this);
                _wsaSendHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                _wsaRecvHook = LocalHook.Create(LocalHook.GetProcAddress("ws2_32.dll", "WSARecv"), new WSARecvDelegate(Hooked_WSARecv), this);
                _wsaRecvHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                monitor?.LogMessage("Winsock 挂钩设置完成，监控已启动");
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"挂钩设置失败: {ex.Message}");
                throw;
            }
        }

        private int Hooked_send(IntPtr socket, byte[] buf, int len, int flags)
        {
            string text = GetData(buf, len);
            monitor?.LogMessage($"发送数据：\n{text}");

            return WindowsApi.send(socket, buf, len, flags);
        }

        private int Hooked_sendto(IntPtr socket, byte[] buf, int len, int flags, IntPtr to, int tolen)
        {
            string text = GetData(buf, len);
            monitor?.LogMessage($"发送数据：\n{text}");

            return WindowsApi.sendto(socket, buf, len, flags, to, tolen);
        }

        private int Hooked_recv(IntPtr socket, byte[] buf, int len, int flags)
        {
            int result = WindowsApi.recv(socket, buf, len, flags);
            if (result > 0)
            {
                string text = GetData(buf, len);
                monitor?.LogMessage($"接收数据：\n{text}");
            }

            return result;
        }

        private int Hooked_recvfrom(IntPtr socket, byte[] buf, int len, int flags, IntPtr from, IntPtr fromlen)
        {
            int result = WindowsApi.recvfrom(socket, buf, len, flags, from, fromlen);
            if (result > 0)
            {
                string text = GetData(buf, len);
                monitor?.LogMessage($"接收数据：\n{text}");
            }

            return result;
        }

        private int Hooked_connect(IntPtr socket, IntPtr name, int namelen)
        {
            try
            {
                if (namelen == 16)
                {
                    var sockaddr = Marshal.PtrToStructure<SockAddrIn>(name);
                    if (sockaddr.sin_family == 2)
                    {
                        string ip = $"{sockaddr.sin_addr & 0xFF}.{(sockaddr.sin_addr >> 8) & 0xFF}.{(sockaddr.sin_addr >> 16) & 0xFF}.{(sockaddr.sin_addr >> 24) & 0xFF}";
                        ushort port = (ushort)((sockaddr.sin_port >> 8) | (sockaddr.sin_port << 8));

                        monitor?.LogMessage($"连接到 {ip}:{port}");
                    }
                }
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"Hooked connect Error:{ex.Message}");
            }

            return WindowsApi.connect(socket, name, namelen);
        }

        private int Hooked_WSASend(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesSent, int flags, IntPtr overlapped, IntPtr completionRoutine)
        {
            int result = WindowsApi.WSASend(socket, buffers, bufferCount, out bytesSent, flags, overlapped, completionRoutine);

            if (result == 0 && bytesSent > 0)
            {
                try
                {
                    for (int i = 0; i < bufferCount; i++)
                    {
                        var wsaBuf = Marshal.PtrToStructure<WSABUF>(IntPtr.Add(buffers, i * Marshal.SizeOf<WSABUF>()));
                        if (wsaBuf.len > 0)
                        {
                            byte[] buffer = new byte[wsaBuf.len];
                            Marshal.Copy(wsaBuf.buf, buffer, 0, buffer.Length);

                            string text = GetData(buffer, buffer.Length);
                            monitor?.LogMessage($"发送数据\n{text}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    monitor?.ReportError($"Hooked WSASend Error:{ex.Message}");
                }
            }

            return result;
        }

        private int Hooked_WSARecv(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesRecvd, ref int flags, IntPtr overlapped, IntPtr completionRoutine)
        {
            int result = WindowsApi.WSARecv(socket, buffers, bufferCount, out bytesRecvd, ref flags, overlapped, completionRoutine);

            if (result == 0 && bytesRecvd > 0)
            {
                try
                {
                    for (int i = 0; i < bufferCount; i++)
                    {
                        var wsaBuf = Marshal.PtrToStructure<WSABUF>(IntPtr.Add(buffers, i * Marshal.SizeOf<WSABUF>()));
                        if (wsaBuf.len > 0)
                        {
                            byte[] buffer = new byte[Math.Min(wsaBuf.len, 4096)];
                            Marshal.Copy(wsaBuf.buf, buffer, 0, buffer.Length);

                            string text = GetData(buffer, buffer.Length);
                            monitor?.LogMessage($"接收数据\n{text}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    monitor?.ReportError($"Hooked WSARecv Error:{ex.Message}");
                }
            }

            return result;
        }

        public string GetData(byte[] data, int length)
        {
            return Convert.ToBase64String(data, 0, length);

            string text = Encoding.UTF8.GetString(data, 0, Math.Min(length, data.Length));
            return text;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SockAddrIn
        {
            public short sin_family;
            public ushort sin_port;
            public uint sin_addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sin_zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct WSABUF
        {
            public int len;
            public IntPtr buf;
        }
    }
}
