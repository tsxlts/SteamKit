namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 物品定义
    /// </summary>
    public class Items : Item
    {
        /// <summary>
        /// DefIndex
        /// </summary>
        public uint DefIndex { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool BaseItem { get; set; }
    }
}
