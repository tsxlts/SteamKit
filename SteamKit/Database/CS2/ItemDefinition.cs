namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 物品定义
    /// </summary>
    public class ItemDefinition
    {
        /// <summary>
        /// DefIndex
        /// </summary>
        public uint DefIndex { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 是否基础物品定义
        /// </summary>
        public bool BaseItem { get; set; }

        /// <summary>
        /// 名称标识
        /// </summary>
        public string? ItemName { get; set; }
    }
}
