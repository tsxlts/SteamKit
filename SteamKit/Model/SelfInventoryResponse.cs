using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 库存响应
    /// </summary>
    public class SelfInventoryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Error { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<Inventory>? Inventories { get; set; } = new List<Inventory>();

        /// <summary>
        /// 
        /// </summary>
        public object? Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SelfInventoryDescription>? Descriptions { get; set; } = new List<SelfInventoryDescription>();

        /// <summary>
        /// 
        /// </summary>
        public bool More { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MoreStart { get; set; } = string.Empty;
    }

    /// <summary>
    /// 库存
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public ulong AssetId { get; set; }

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
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hide_in_china")]
        public bool HideInChina { get; set; }
    }
}
