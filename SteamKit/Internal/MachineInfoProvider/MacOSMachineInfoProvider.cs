
using System.Runtime.Versioning;
using System.Text;
using static SteamKit.Internal.Provider.CoreFoundation;
using static SteamKit.Internal.Provider.DiskArbitration;
using static SteamKit.Internal.Provider.IOKit;
using static SteamKit.Internal.Provider.LibC;

namespace SteamKit.Internal.Provider
{
    [SupportedOSPlatform("macos")]
    sealed class MacOSMachineInfoProvider : IMachineInfoProvider
    {
        public byte[]? GetMachineGuid()
        {
            uint platformExpert = IOServiceGetMatchingService(kIOMasterPortDefault, IOServiceMatching("IOPlatformExpertDevice"));
            if (platformExpert != 0)
            {
                try
                {
                    using var serialNumberKey = CFStringCreateWithCString(CFTypeRef.None, kIOPlatformSerialNumberKey, CFStringEncoding.kCFStringEncodingASCII);
                    var serialNumberAsString = IORegistryEntryCreateCFProperty(platformExpert, serialNumberKey, CFTypeRef.None, 0);
                    var sb = new StringBuilder(64);
                    if (CFStringGetCString(serialNumberAsString, sb, sb.Capacity, CFStringEncoding.kCFStringEncodingASCII))
                    {
                        return Encoding.ASCII.GetBytes(sb.ToString());
                    }
                }
                finally
                {
                    _ = IOObjectRelease(platformExpert);
                }
            }

            return null;
        }

        public byte[]? GetMacAddress() => null;

        public byte[]? GetDiskId()
        {
            var stat = new StatFS();
            var statted = statfs64("/", ref stat);
            if (statted == 0)
            {
                using var session = DASessionCreate(CFTypeRef.None);
                using var disk = DADiskCreateFromBSDName(CFTypeRef.None, session, stat.f_mntfromname);
                using var properties = DADiskCopyDescription(disk);
                using var key = CFStringCreateWithCString(CFTypeRef.None, DiskArbitration.kDADiskDescriptionMediaUUIDKey, CFStringEncoding.kCFStringEncodingASCII);
                IntPtr cfuuid = IntPtr.Zero;
                if (CFDictionaryGetValueIfPresent(properties, key, out cfuuid))
                {
                    using var uuidString = CFUUIDCreateString(CFTypeRef.None, cfuuid);
                    var stringBuilder = new StringBuilder(64);
                    if (CFStringGetCString(uuidString, stringBuilder, stringBuilder.Capacity, CFStringEncoding.kCFStringEncodingASCII))
                    {
                        return Encoding.ASCII.GetBytes(stringBuilder.ToString());
                    }
                }
            }

            return null;
        }
    }
}
