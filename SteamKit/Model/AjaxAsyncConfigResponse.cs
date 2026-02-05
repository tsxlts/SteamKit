using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AjaxAsyncConfigResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data")]
        public AjaxAsyncConfig? Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AjaxAsyncConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("webapi_token")]
        public string WebApiToken { get; set; } = string.Empty;
    }
}
