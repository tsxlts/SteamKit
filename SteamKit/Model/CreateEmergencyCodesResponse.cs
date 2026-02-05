
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 创建备用码响应
    /// </summary>
    public class CreateEmergencyCodesResponse
    {
        /// <summary>
        /// 备用码
        /// </summary>
        [JsonProperty("codes")]
        public List<string> Codes { get; set; } = new List<string>();
    }
}
