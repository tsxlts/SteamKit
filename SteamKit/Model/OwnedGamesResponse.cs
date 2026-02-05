using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询用户拥有的游戏响应
    /// </summary>
    public class OwnedGamesResponse
    {
        /// <summary>
        /// 游戏数量
        /// </summary>
        [JsonProperty("game_count")]
        public int GameCount { get; set; }

        /// <summary>
        /// 游戏
        /// </summary>
        [JsonProperty("games")]
        public IEnumerable<OwnedGame> Games { get; set; } = new List<OwnedGame>();
    }

    /// <summary>
    /// 拥有的游戏
    /// </summary>
    public class OwnedGame
    {
        /// <summary>
        /// 游戏Id
        /// AppId
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 游戏名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("img_icon_url", NullValueHandling = NullValueHandling.Ignore)]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("capsule_filename", NullValueHandling = NullValueHandling.Ignore)]
        public string CapsuleFileName { get; set; } = string.Empty;

        /// <summary>
        /// 最后打开游戏时间
        /// </summary>
        [JsonProperty("rtime_last_played")]
        public long LastPlayedTime { get; set; }

        /// <summary>
        /// 总游戏时长
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_forever")]
        public long PlayTimeForever { get; set; }

        /// <summary>
        /// Windows游戏时长
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_windows_forever")]
        public long PlayTimeWindowsForever { get; set; }

        /// <summary>
        /// Mac游戏时长
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_mac_forever")]
        public long PlayTimeMacForever { get; set; }

        /// <summary>
        /// Linux游戏时长
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_linux_forever")]
        public long PlayTimeLinuxForever { get; set; }

        /// <summary>
        /// Deck游戏时长
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_deck_forever")]
        public long PlayTimeDeckForever { get; set; }

        /// <summary>
        /// 过去两周游戏时长
        /// </summary>
        [JsonProperty("playtime_2weeks")]
        public long PlayTime2Weeks { get; set; }

        /// <summary>
        /// 分钟
        /// </summary>
        [JsonProperty("playtime_disconnected")]
        public long PlayTimeDisconnected { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("has_community_visible_stats")]
        public bool HasCommunityVisibleStats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("has_leaderboards")]
        public bool HasLeaderboards { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("has_workshop")]
        public bool HasWorkshop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("has_market")]
        public bool HasMarket { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("has_dlc")]
        public bool HasDLC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("content_descriptorids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> ContentDescriptorids { get; set; } = new List<int>();
    }
}
