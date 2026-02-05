using Newtonsoft.Json;
using SteamKit.Converter;

namespace SteamKit.Model
{
    /// <summary>
    /// 描述信息
    /// </summary>
    public class BaseDescription
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("classid")]
        public ulong ClassId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("instanceid")]
        public ulong InstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// 是否可交易
        /// false在7天冷却期
        /// </summary>
        [JsonProperty("tradable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Tradable { get; set; }

        /// <summary>
        /// 是否可出售
        /// </summary>
        [JsonProperty("marketable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Marketable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("commodity")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Commodity { get; set; }

        /// <summary>
        /// 是否受到保护的
        /// </summary>
        [JsonProperty("sealed")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Sealed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_name")]
        public string MarketName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name_color")]
        public string NameColor { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<AssetDescription> Descriptions { get; set; } = new List<AssetDescription>();
    }
}
