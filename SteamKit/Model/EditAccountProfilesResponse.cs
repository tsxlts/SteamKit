using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 编辑个人资料响应
    /// </summary>
    public class EditAccountProfilesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("errmsg")]
        public string? ErrMsg { get; set; }
    }
}
