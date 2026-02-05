using SteamKit.Client.Model;

namespace SteamKit.Game.CS2.Models
{
    /// <summary>
    /// 商店物品价格
    /// </summary>
    public class StoreItemPrice
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyCode"></param>
        public StoreItemPrice(ECurrencyCode currencyCode)
        {
            this.Currency = currencyCode;
        }

        /// <summary>
        /// 货币
        /// </summary>
        public ECurrencyCode Currency { get; }

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
