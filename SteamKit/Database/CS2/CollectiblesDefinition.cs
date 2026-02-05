namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 收藏品定义
    /// </summary>
    public class CollectiblesDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string set_description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string is_collection { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> items { get; set; } = new Dictionary<string, string>();
    }
}
