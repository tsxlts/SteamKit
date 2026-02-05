using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace SteamKit.Internal
{
    internal static class ProcessingHelper
    {
        public static int GetParentProcessId(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                return GetParentProcessId(process);
            }
            catch (Exception e) when (!(e is NotSupportedException))
            {
                return -1;
            }
        }

        public static int GetParentProcessId(Process process)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return GetParentProcessIdWindows(process);
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return GetParentProcessIdUnix(process);
                }
            }
            catch
            {
                return -1;
            }

            throw new PlatformNotSupportedException("Unsupported OS platform");
        }

        [SupportedOSPlatform("windows")]
        private static int GetParentProcessIdWindows(Process process)
        {
            var pbi = new PROCESS_BASIC_INFORMATION();
            int status = NtQueryInformationProcess(process.Handle, 0, ref pbi, Marshal.SizeOf(pbi), out _);

            if (status != 0)
            {
                throw new Win32Exception(status);
            }

            return (int)pbi.InheritedFromUniqueProcessId;
        }

        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("macos")]
        private static int GetParentProcessIdUnix(Process process)
        {
            string statPath = $"/proc/{process.Id}/stat";
            if (File.Exists(statPath))
            {
                string stat = File.ReadAllText(statPath);
                string[] parts = stat.Split(' ');
                return int.Parse(parts[3]);
            }
            return -1;
        }

        [DllImport("ntdll.dll")]
        private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_BASIC_INFORMATION
        {
            public IntPtr ExitStatus;
            public IntPtr PebBaseAddress;
            public IntPtr AffinityMask;
            public IntPtr BasePriority;
            public UIntPtr UniqueProcessId;
            public IntPtr InheritedFromUniqueProcessId;
        }
    }
}
