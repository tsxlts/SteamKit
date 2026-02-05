
using System.Runtime.Versioning;
using System.Text;

namespace SteamKit.Internal.Provider
{
    [SupportedOSPlatform("linux")]
    internal sealed class LinuxMachineInfoProvider : IMachineInfoProvider
    {
        public byte[]? GetMachineGuid()
        {
            string[] machineFiles = new[]
            {
                "/etc/machine-id",
                "/var/lib/dbus/machine-id",
                "/sys/class/net/eth0/address",
                "/sys/class/net/eth1/address",
                "/sys/class/net/eth2/address",
                "/sys/class/net/eth3/address",
                "/etc/hostname",
            };

            foreach (var fileName in machineFiles)
            {
                try
                {
                    return File.ReadAllBytes(fileName);
                }
                catch
                {
                    // if we can't read a file, continue to the next until we hit one we can
                    continue;
                }
            }

            return null;
        }

        public byte[]? GetMacAddress() => null;

        public byte[]? GetDiskId()
        {
            string[] bootParams = GetBootOptions();

            string[] paramsToCheck = new[]
            {
                "root=UUID=",
                "root=PARTUUID=",
            };

            foreach (string param in paramsToCheck)
            {
                var paramValue = GetParamValue(bootParams, param);

                if (!string.IsNullOrEmpty(paramValue))
                {
                    return Encoding.UTF8.GetBytes(paramValue);
                }
            }

            string[] diskUuids = GetDiskUUIDs();

            if (diskUuids.Length > 0)
            {
                return Encoding.UTF8.GetBytes(diskUuids[0]);
            }

            return null;
        }


        static string[] GetBootOptions()
        {
            string bootOptions;

            try
            {
                bootOptions = File.ReadAllText("/proc/cmdline");
            }
            catch
            {
                return new string[0];
            }

            return bootOptions.Split(' ');
        }

        static string[] GetDiskUUIDs()
        {
            try
            {
                var dirInfo = new DirectoryInfo("/dev/disk/by-uuid");

                // we want the oldest disk symlinks first
                return dirInfo.GetFiles()
                    .OrderBy(f => f.LastWriteTime)
                    .Select(f => f.Name)
                    .ToArray();
            }
            catch
            {
                return new string[0];
            }
        }

        static string? GetParamValue(string[] bootOptions, string param)
        {
            var paramString = bootOptions
                .FirstOrDefault(p => p.StartsWith(param, StringComparison.OrdinalIgnoreCase));

            if (paramString == null)
                return null;

            return paramString[param.Length..];
        }
    }
}
