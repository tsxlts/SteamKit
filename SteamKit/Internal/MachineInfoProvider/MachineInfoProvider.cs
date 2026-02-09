
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using SteamKit.Factory;
using SteamKit.Types;

namespace SteamKit.Internal.Provider
{
    internal class MachineInfoProvider
    {
        private static ConditionalWeakTable<IMachineInfoProvider, Task<MachineId>> generationTable;

        static MachineInfoProvider()
        {
            generationTable = new ConditionalWeakTable<IMachineInfoProvider, Task<MachineId>>();
        }

        public static IMachineInfoProvider GetDefaultProvider()
        {
            IMachineInfoProvider? provider = null; ;
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    provider = new WindowsMachineInfoProvider();
                    return provider;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    provider = new MacOSMachineInfoProvider();
                    return provider;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    provider = new LinuxMachineInfoProvider();
                    return provider;
                }

                provider = new DefaultMachineInfoProvider();
                return provider;
            }
            finally
            {
                if (provider != null)
                {
                    lock (provider)
                    {
                        _ = generationTable.GetValue(provider, p => Task.Factory.StartNew(GenerateMachineID, state: p));
                    }
                }
            }
        }

        public static byte[]? GetMachineId(IMachineInfoProvider machineProvider)
        {
            if (!generationTable.TryGetValue(machineProvider, out var generateTask))
            {
                lock (machineProvider)
                {
                    _ = generationTable.GetValue(machineProvider, p => Task.Factory.StartNew(GenerateMachineID, state: p));
                }

                if (!generationTable.TryGetValue(machineProvider, out generateTask))
                {
                    return null;
                }
            }

            try
            {
                bool didComplete = generateTask.Wait(TimeSpan.FromSeconds(30));
                if (!didComplete)
                {
                    return null;
                }
            }
            catch (AggregateException ex) when (ex.InnerException != null && generateTask.IsFaulted)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            }

            MachineId machineId = generateTask.Result;
            using (MemoryStream ms = new MemoryStream())
            {
                machineId.WriteToStream(ms);
                byte[] buffer = ms.ToArray();
                return buffer;
            }
        }

        private static MachineId GenerateMachineID(object? state)
        {
            var provider = (IMachineInfoProvider)state!;

            var machineId = new MachineId();

            machineId.SetBB3(GetHexString(provider.GetMachineGuid() ?? DefaultMachineInfoProvider.Instance.GetMachineGuid()));
            machineId.SetFF2(GetHexString(provider.GetMacAddress() ?? DefaultMachineInfoProvider.Instance.GetMacAddress()));
            machineId.Set3B3(GetHexString(provider.GetDiskId() ?? DefaultMachineInfoProvider.Instance.GetDiskId()));
            return machineId;
        }

        private static string GetHexString(byte[] data)
        {
            data = SHA1.HashData(data);
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }
    }
}
