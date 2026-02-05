namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 收藏品
    /// </summary>
    public class Collectibles : Item
    {
        /// <summary>
        /// 
        /// </summary>
        public string EnName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool IsCollection { get; set; }

        /// <summary>
        /// 包含的物品品质
        /// </summary>
        public List<Rarity> Rarities { get; set; } = new List<Rarity>();
    }
}
