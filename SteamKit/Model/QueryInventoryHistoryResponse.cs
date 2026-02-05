
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询库存历史记录响应
    /// </summary>
    public class QueryInventoryHistoryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("html")]
        public string? Html { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("num")]
        public int Num { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("descriptions")]
        public IEnumerable<InventoryHistoryDescription>? Descriptions { get; set; } = new List<InventoryHistoryDescription>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cursor")]
        public InventoryHistoryCursor? Cursor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class InventoryHistoryCursor
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("time")]
            public long Time { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("time_frac")]
            public int TimeFrac { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("s")]
            public string S { get; set; } = string.Empty;
        }
    }
}
