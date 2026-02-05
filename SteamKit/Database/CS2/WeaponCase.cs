namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 武器箱
    /// </summary>
    public class WeaponCase : Item
    {
        /// <summary>
        /// DefIndex
        /// </summary>
        public uint DefIndex { get; set; }

        /// <summary>
        /// 武器箱系列
        /// </summary>
        public uint CrateSeries { get; set; }

        /// <summary>
        /// 钥匙
        /// DefIndex
        /// </summary>
        public uint KeyDefIndex { get; set; }

        /// <summary>
        /// 包含的物品品质
        /// </summary>
        public List<Rarity> Rarities { get; set; } = new List<Rarity>();
    }
}
