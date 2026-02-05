using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 获取拥有的游戏库存响应
    /// </summary>
    public class AppInventoryContextsResponse
    {
        /// <summary>
        /// 游戏Id
        /// AppId
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 游戏名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 游戏图标
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("load_failed")]
        public int LoadFailed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("owner_only")]
        public bool OwnerOnly { get; set; }

        /// <summary>
        /// 资产数量
        /// </summary>
        [JsonProperty("asset_count")]
        public int AssetCount { get; set; }

        /// <summary>
        /// 交易权限
        /// <para>全部：FULL</para>
        /// <para>仅可送出物品：SENDONLY</para>
        /// <para>仅可送出物品，库存已满：SENDONLY_FULLINVENTORY</para>
        /// <para>仅可接收物品：RECEIVEONLY</para>
        /// </summary>
        [JsonProperty("trade_permissions")]
        public string TradePermissions { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<AppContext> Contexts { get; set; } = new List<AppContext>();

        /// <summary>
        /// 是否可以送出物品
        /// </summary>
        /// <returns></returns>
        public bool AllowedToTradeItems()
        {
            return new[] { "FULL", "SENDONLY", "SENDONLY_FULLINVENTORY" }.Contains(TradePermissions);
        }

        /// <summary>
        /// 是否可以接收物品
        /// </summary>
        /// <returns></returns>
        public bool AllowedToRecieveItems()
        {
            return new[] { "FULL", "RECEIVEONLY" }.Contains(TradePermissions);
        }

        /// <summary>
        /// 库存是否已满
        /// </summary>
        /// <returns></returns>
        public bool InventoryIsFull()
        {
            return new[] { "SENDONLY_FULLINVENTORY" }.Contains(TradePermissions);
        }

        /// <summary>
        /// 
        /// </summary>
        public class AppContext
        {
            /// <summary>
            /// 资产数量
            /// </summary>
            [JsonProperty("asset_count")]
            public int AssetCount { get; set; }

            /// <summary>
            /// ContextId
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 资产数量
            /// </summary>
            [JsonProperty("owner_only")]
            public bool OwnerOnly { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("hide_context")]
            public bool HideContext { get; set; }
        }
    }
}
