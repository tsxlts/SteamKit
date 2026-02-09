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
        [JsonProperty("appid", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("classid", NullValueHandling = NullValueHandling.Ignore)]
        public ulong ClassId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("instanceid", NullValueHandling = NullValueHandling.Ignore)]
        public ulong InstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("icon_url", NullValueHandling = NullValueHandling.Ignore)]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// 是否可交易
        /// false在7天冷却期
        /// </summary>
        [JsonProperty("tradable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BoolConverter))]
        public bool Tradable { get; set; }

        /// <summary>
        /// 是否可出售
        /// </summary>
        [JsonProperty("marketable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BoolConverter))]
        public bool Marketable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("commodity", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BoolConverter))]
        public bool Commodity { get; set; }

        /// <summary>
        /// 是否受到保护的
        /// </summary>
        [JsonProperty("sealed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BoolConverter))]
        public bool Sealed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_hash_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketHashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name_color", NullValueHandling = NullValueHandling.Ignore)]
        public string NameColor { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = string.Empty;

        /*
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_listing_bucket_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarketListingBucketId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_listing_bucket_group_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarketListingBucketGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("market_listing_bucket_group_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarketListingBucketGroupName { get; set; }
        */

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<AssetDescription> Descriptions { get; set; } = new List<AssetDescription>();
    }
}
