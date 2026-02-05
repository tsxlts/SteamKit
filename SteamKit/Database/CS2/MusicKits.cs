namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 音乐盒定义
    /// </summary>
    public class MusicKits
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string loc_name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string? loc_description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? image_inventory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? pedestal_display_model { get; set; }
    }
}
