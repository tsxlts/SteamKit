using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询用户徽章响应
    /// </summary>
    public class QueryBadgesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("player_xp")]
        public int PlayerXP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("player_level")]
        public int PlayerLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("player_xp_needed_to_level_up")]
        public int PlayerXPNeededToLevelUp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("player_xp_needed_current_level")]
        public int PlayerXPNeededCurrentLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("badges")]
        public List<Badge> Badges { get; set; } = new List<Badge>();
    }

    /// <summary>
    /// 徽章
    /// </summary>
    public class Badge
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("badgeid")]
        public int BadgeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("level")]
        public int Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("completion_time")]
        public int CompletionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("xp")]
        public int XP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("scarcity")]
        public int Scarcity { get; set; }
    }
}
