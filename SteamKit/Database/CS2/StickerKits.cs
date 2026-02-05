namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 
    /// </summary>
    public class StickerKits
    {
        /// <summary>
        /// 
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? item_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? description_string { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? sticker_material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? patch_material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? tournament_event_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSticker() => !string.IsNullOrWhiteSpace(sticker_material);

        /// <summary>
        /// 
        /// </summary>
        public bool IsPatch() => !string.IsNullOrWhiteSpace(patch_material);
    }

}
