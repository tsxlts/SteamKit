using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询用户等级响应
    /// </summary>
    public class QuerySteamLevelResponse
    {
        /// <summary>
        /// 等级
        /// </summary>
        [JsonProperty("player_level")]
        public int PlayerLevel { get; set; }
    }
}
