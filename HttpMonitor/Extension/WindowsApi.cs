using System;
using System.Runtime.InteropServices;

namespace HttpMonitor.Extension
{
    internal static class WindowsApi
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr HttpOpenRequest(IntPtr hConnect, string lpszVerb, string lpszObjectName, string lpszVersion, string lpszReferrer, string lplpszAcceptTypes, uint dwFlags, IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern bool HttpSendRequest(IntPtr hRequest, string lpszHeaders, int dwHeadersLength, IntPtr lpOptional, int dwOptionalLength);

        [DllImport("winhttp.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr WinHttpOpenRequest(IntPtr hConnect, string lpszVerb, string lpszObjectName, string lpszVersion, string lpszReferrer, string lplpszAcceptTypes, uint dwFlags);

        [DllImport("winhttp.dll", CharSet = CharSet.Auto)]
        public static extern bool WinHttpSendRequest(IntPtr hRequest, string lpszHeaders, int dwHeadersLength, IntPtr lpOptional, int dwOptionalLength, int dwTotalLength, IntPtr dwContext);

        [DllImport("winhttp.dll")]
        public static extern bool WinHttpReceiveResponse(IntPtr hRequest, IntPtr lpReserved);

        [DllImport("winhttp.dll")]
        public static extern bool WinHttpReadData(IntPtr hRequest, IntPtr lpBuffer, int dwNumberOfBytesToRead, out int lpdwNumberOfBytesRead);

        [DllImport("winhttp.dll", CharSet = CharSet.Auto)]
        public static extern bool WinHttpQueryHeaders(IntPtr hRequest, uint dwInfoLevel, string lpName, IntPtr lpBuffer, ref int lpdwBufferLength, IntPtr lpdwIndex);

        [DllImport("winhttp.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WinHttpQueryHeaders(IntPtr hRequest, uint dwInfoLevel, IntPtr lpName, IntPtr lpBuffer, ref uint lpdwBufferLength, IntPtr lpdwIndex);

        [DllImport("winhttp.dll", SetLastError = true)]
        public static extern bool WinHttpCloseHandle(IntPtr hInternet);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int send(IntPtr socket, byte[] buf, int len, int flags);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int sendto(IntPtr socket, byte[] buf, int len, int flags, IntPtr to, int tolen);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int recv(IntPtr socket, byte[] buf, int len, int flags);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int recvfrom(IntPtr socket, byte[] buf, int len, int flags, IntPtr from, IntPtr fromlen);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int connect(IntPtr socket, IntPtr name, int namelen);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int WSASend(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesSent, int flags, IntPtr overlapped, IntPtr completionRoutine);

        [DllImport("ws2_32.dll", SetLastError = true)]
        public static extern int WSARecv(IntPtr socket, IntPtr buffers, int bufferCount, out int bytesRecvd, ref int flags, IntPtr overlapped, IntPtr completionRoutine);
    }
}
