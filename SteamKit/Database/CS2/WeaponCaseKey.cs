namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 武器箱钥匙
    /// </summary>
    public class WeaponCaseKey : Item
    {
        /// <summary>
        /// DefIndex
        /// </summary>
        public uint DefIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; } = string.Empty;
    }
}
