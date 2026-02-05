
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询用户封禁信息响应
    /// </summary>
    public class PlayerBansResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("players")]
        public IEnumerable<PlayerBans>? Players { get; set; }
    }

    /// <summary>
    /// 用户封禁信息
    /// </summary>
    public class PlayerBans
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("SteamId")]
        public string SteamId { get; set; } = string.Empty;

        /// <summary>
        /// 社区封禁
        /// </summary>
        [JsonProperty("CommunityBanned")]
        public bool CommunityBanned { get; set; }

        /// <summary>
        /// VAC封禁
        /// </summary>
        [JsonProperty("VACBanned")]
        public bool VACBanned { get; set; }

        /// <summary>
        /// VAC封禁次数
        /// </summary>
        [JsonProperty("NumberOfVACBans")]
        public int NumberOfVACBans { get; set; }

        /// <summary>
        /// 游戏封禁次数
        /// </summary>
        [JsonProperty("NumberOfGameBans")]
        public int NumberOfGameBans { get; set; }

        /// <summary>
        /// 距离上一次封禁天数
        /// </summary>
        [JsonProperty("DaysSinceLastBan")]
        public int DaysSinceLastBan { get; set; }

        /// <summary>
        /// 包含玩家在经济中的禁赛状态的字符串
        /// <para>如果玩家没有记录禁令，字符串将为“none”</para>
        /// <para>如果玩家处于试用期，字符串将显示“probation”</para>
        /// <para>交易封禁“banned”</para>
        /// 以此类推
        /// </summary>
        [JsonProperty("EconomyBan")]
        public string? EconomyBan { get; set; } = string.Empty;
    }
}