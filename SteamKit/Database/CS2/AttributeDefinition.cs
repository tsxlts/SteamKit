namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 属性定义
    /// </summary>
    public class AttributeDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string AttributeClass { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DescriptionFormat { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DescriptionString { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Hidden { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string EffectType { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string StoredAsInteger { get; set; } = string.Empty;
    }

}
