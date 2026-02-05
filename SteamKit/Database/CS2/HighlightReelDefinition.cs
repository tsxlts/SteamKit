using Newtonsoft.Json;

namespace SteamKit.Database.CS2
{
    /// <summary>
    /// 高光时刻定义
    /// </summary>
    public class HighlightReelDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public string id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tournament event id")]
        public string tournament_event_id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tournament event stage id")]
        public string tournament_event_stage_id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("map")]
        public string map { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tournament event team0 id")]
        public string tournament_event_team0_id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tournament event team1 id")]
        public string tournament_event_team1_id { get; set; } = string.Empty;
    }
}
