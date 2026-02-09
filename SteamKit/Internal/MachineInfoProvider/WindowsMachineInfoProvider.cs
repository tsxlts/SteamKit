using System.Net.NetworkInformation;
using System.Runtime.Versioning;
using System.Text;
using Microsoft.Win32;
using SteamKit.Factory;

namespace SteamKit.Internal.Provider
{
    [SupportedOSPlatform("windows")]
    internal sealed class WindowsMachineInfoProvider : IMachineInfoProvider
    {
        public byte[]? GetMachineGuid()
        {
            using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using var localKey = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");

            if (localKey == null)
            {
                return null;
            }

            var guid = localKey.GetValue("MachineGuid");

            if (guid == null)
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(guid.ToString()!);
        }

        public byte[] GetMacAddress()
        {
            // This part of the code finds  *Physical* network interfaces
            // based on : https://social.msdn.microsoft.com/Forums/en-US/46c86903-3698-41bc-b081-fcf444e8a127/get-the-ip-address-of-the-physical-network-card-?forum=winforms
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(adapter =>
                {
                    //Accessing the registry key corresponding to each adapter
                    string fRegistryKey =
                        $@"SYSTEM\CurrentControlSet\Control\Network\{{4D36E972-E325-11CE-BFC1-08002BE10318}}\{adapter.Id}\Connection";
                    using RegistryKey? rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                    if (rk == null) return false;

                    var instanceID = rk.GetValue("PnpInstanceID", "")?.ToString();
                    return instanceID?.Length > 3 && instanceID.StartsWith("PCI");
                })
                .Select(networkInterface => networkInterface.GetPhysicalAddress().GetAddressBytes()
                    //pad all found mac addresses to 8 bytes
                    .Append((byte)0)
                    .Append((byte)0)
                )
                //add fallbacks in case less than 2 adapters are found
                .Append(Enumerable.Repeat((byte)0, 8))
                .Append(Enumerable.Repeat((byte)0, 8))
                .Take(2)
                .SelectMany(b => b)
                .ToArray();
        }

        public byte[]? GetDiskId()
        {
            var serialNumber = Win32Helpers.GetBootDiskSerialNumber();

            if (string.IsNullOrEmpty(serialNumber))
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(serialNumber);
        }
    }
}
