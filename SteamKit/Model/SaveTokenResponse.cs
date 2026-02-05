using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 保存Token响应
    /// </summary>
    public class SaveTokenResponse
    {
        /// <summary>
        /// 错误状态码
        /// Success为false生效
        /// </summary>
        [JsonProperty("result")]
        public ErrorCodes Result { get; set; }
    }
}
