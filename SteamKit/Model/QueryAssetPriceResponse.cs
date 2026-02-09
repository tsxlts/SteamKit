namespace SteamKit.Model
{
    /// <summary>
    /// 查询游戏类物品价格响应
    /// </summary>
    public class QueryAssetPriceResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<AppStoreAsset> Assets { get; set; } = new List<AppStoreAsset>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppStoreAsset
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AssetPrice> Prices { get; set; } = new List<AssetPrice>();

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ClassId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public class AssetPrice
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="currency"></param>
            public AssetPrice(Enums.Currency currency)
            {
                this.Currency = currency;
            }

            /// <summary>
            /// 货币
            /// </summary>
            public Enums.Currency Currency { get; }

            /// <summary>
            /// 价格
            /// 原价
            /// </summary>
            public ulong Price { get; set; }

            /// <summary>
            /// 售价
            /// 实际出售价格
            /// </summary>
            public ulong SalePrice { get; set; }
        }
    }
}
