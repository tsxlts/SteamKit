namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 品质
    /// </summary>
    public class Rarity
    {
        /// <summary>
        /// 
        /// </summary>
        public string HashName { get; set; } = string.Empty;

        /// <summary>
        /// 标识
        /// <para>例如：Rarity_Common</para>
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 标识-武器类
        /// <para>例如：Rarity_Uncommon_Weapon</para>
        /// </summary>
        public string WeaponKey { get; set; } = string.Empty;

        /// <summary>
        /// 名称-武器类
        /// <para>例如：Rarity_Uncommon_Weapon</para>
        /// </summary>
        public string WeaponName { get; set; } = string.Empty;

        /// <summary>
        /// 标识-探员类
        /// <para>例如：Rarity_Uncommon_Character</para>
        /// </summary>
        public string CharacterKey { get; set; } = string.Empty;

        /// <summary>
        /// 名称-探员类
        /// <para>例如：Rarity_Uncommon_Character</para>
        /// </summary>
        public string CharacterName { get; set; } = string.Empty;

        /// <summary>
        /// 下一级品质
        /// </summary>
        public string NextRarity { get; set; } = string.Empty;
    }
}
