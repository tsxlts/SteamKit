
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 待确认数据响应
    /// </summary>
    public class QueryConfirmationsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("needauth")]
        public bool NeedAuth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("detail")]
        public string? Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("conf")]
        public List<Confirmation>? Confirmations { get; set; }
    }

    /// <summary>
    /// 确认信息
    /// </summary>
    public class Confirmation : ConfirmationBase
    {
        /// <summary>
        /// 待确认信息类型
        /// </summary>
        [JsonProperty("type")]
        public SteamEnum.ConfirmationType ConfType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? ConfTypeName { get; set; }

        /// <summary>
        /// 待确认信息类型对应的业务Id
        /// 例如：待确认的报价，此值代表报价Id
        /// </summary>
        [JsonProperty("creator_id")]
        public ulong CreatorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("creation_time")]
        public long CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("headline", NullValueHandling = NullValueHandling.Ignore)]
        public string? Headline { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Summary { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("warn", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Warn { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "accept", NullValueHandling = NullValueHandling.Ignore)]
        public string Accept { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "cancel", NullValueHandling = NullValueHandling.Ignore)]
        public string Cancel { get; set; } = string.Empty;

        /// <summary>
        /// 是否支持批量确认
        /// </summary>
        [JsonProperty("multi")]
        public bool Multi { get; set; }
    }

    /// <summary>
    /// 确认信息
    /// </summary>
    public class ConfirmationBase
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nonce")]
        public string Key { get; set; } = string.Empty;
    }
}
