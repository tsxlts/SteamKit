
using System.Net.NetworkInformation;
using System.Text;

namespace SteamKit.Internal.Provider
{
    internal sealed class DefaultMachineInfoProvider : IMachineInfoProvider
    {
        public static DefaultMachineInfoProvider Instance { get; } = new DefaultMachineInfoProvider();

        public byte[] GetMachineGuid()
        {
            return Encoding.UTF8.GetBytes($"{Environment.MachineName}-Steam");
        }

        public byte[] GetMacAddress()
        {
            try
            {
                var firstEth = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet || i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    .FirstOrDefault();

                if (firstEth != null)
                {
                    return firstEth.GetPhysicalAddress().GetAddressBytes();
                }
            }
            catch (NetworkInformationException)
            {
            }
            return Encoding.UTF8.GetBytes("Steam-MacAddress");
        }

        public byte[] GetDiskId()
        {
            return Encoding.UTF8.GetBytes("Steam-DiskId");
        }
    }
}
