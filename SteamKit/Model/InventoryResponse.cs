using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 库存响应
    /// </summary>
    public class InventoryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("last_assetid")]
        public ulong? LastAssetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions")]
        public IEnumerable<TagDescription>? Descriptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("asset_properties")]
        public IEnumerable<AssetProperty>? AssetProperties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("assets")]
        public IEnumerable<Asset>? Assets { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("missing_assets")]
        public IEnumerable<Asset>? MissingAssets { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("error")]
        public string? Error { get; set; }
    }
}
