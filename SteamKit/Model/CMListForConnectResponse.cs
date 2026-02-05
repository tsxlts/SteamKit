
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 服务器链接地址响应
    /// </summary>
    public class CMListForConnectResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("serverlist")]
        public List<CMServer>? Servers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CMServer
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("legacy_endpoint")]
        public string LegacyEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("dc")]
        public string DC { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("realm")]
        public string Realm { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("load")]
        public int Load { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wtd_load")]
        public double WTD_Load { get; set; }
    }
}
