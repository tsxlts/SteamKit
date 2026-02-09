using SteamKit.Client.Model;
using SteamKit.Database.CS2;
using SteamKit.Internal;

namespace SteamKit.Game.CS2.Models
{
    /// <summary>
    /// 商店物品
    /// </summary>
    public class StoreItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemLink"></param>
        /// <param name="language"></param>
        public StoreItem(string itemLink, Enums.Language language)
        {
            var def = CS2Utils.QueryItemDef(itemLink);
            if (def == null)
            {
                return;
            }

            DefIndex = def.DefIndex;
            Key = def.Key;
            HashName = CS2Utils.QueryItemDefName(def, Enums.Language.English) ?? "";
            Name = CS2Utils.QueryItemDefName(def, language) ?? "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prices"></param>
        /// <param name="salePrices"></param>
        /// <returns></returns>
        public StoreItem SetPrices(KeyValue prices, KeyValue salePrices)
        {
            foreach (var itemPrice in prices.Children)
            {
                if (!Enum.TryParse<ECurrencyCode>(itemPrice.Name, out var currency))
                {
                    continue;
                }

                var itemSalePrice = salePrices?.Children.FirstOrDefault(c => c.Name == itemPrice.Name);

                var price = itemPrice.AsUnsignedLong();
                var salePrice = itemSalePrice?.AsUnsignedLong();

                Prices[currency] = new StoreItemPrice(currency)
                {
                    Price = price,
                    SalePrice = salePrice ?? price,
                };
            }

            return this;
        }

        /// <summary>
        /// DefIndex
        /// </summary>
        public uint DefIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string HashName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 价格
        /// </summary>
        public IDictionary<ECurrencyCode, StoreItemPrice> Prices { get; } = new Dictionary<ECurrencyCode, StoreItemPrice>();
    }
}
