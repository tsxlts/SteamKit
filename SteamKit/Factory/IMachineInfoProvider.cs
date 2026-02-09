namespace SteamKit.Factory
{
    /// <summary>
    /// MachineInfoProvider
    /// </summary>
    public interface IMachineInfoProvider
    {
        /// <summary>
        /// 获取机器Id
        /// </summary>
        /// <returns></returns>
        public byte[]? GetMachineGuid();

        /// <summary>
        /// 获取机器MAC地址
        /// </summary>
        /// <returns></returns>
        public byte[]? GetMacAddress();

        /// <summary>
        /// 获取机器磁盘Id
        /// </summary>
        /// <returns></returns>
        public byte[]? GetDiskId();
    }
}
