namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 
    /// </summary>
    public class RarityKits
    {
        /// <summary>
        /// 
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? loc_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? loc_key_weapon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? loc_key_character { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string next_rarity { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string? drop_sound { get; set; }
    }
}
