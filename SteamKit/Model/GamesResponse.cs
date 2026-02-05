
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询游戏响应数据
    /// </summary>
    public class GamesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("apps")]
        public List<Game> Games { get; set; } = new List<Game>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
