using SteamKit.Client.Model.GC.CS2;
using SteamKit.Internal;
using SteamKit.Zip;

namespace SteamKit.Game.CS2.Models
{
    /// <summary>
    /// 商店数据
    /// </summary>
    public class StoreDataResponse
    {
        private readonly SteamEnum.Language language;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeUserDataDataResponse"></param>
        /// <param name="language"></param>
        public StoreDataResponse(CMsgStoreGetUserDataResponse storeUserDataDataResponse, SteamEnum.Language language)
        {
            this.language = language;
            Result = storeUserDataDataResponse.result;
            PriceSheetVersion = storeUserDataDataResponse.price_sheet_version;

            using (var compressed = new MemoryStream(storeUserDataDataResponse.price_sheet))
            {
                if (LzmaUtil.TryDecompress(compressed, static capacity => new MemoryStream(capacity), out var decompressed))
                {
                    using (decompressed)
                    {
                        PriceSheet.TryReadAsBinary(decompressed);
                    }
                }
            }

            PriceSheetItems = new Lazy<IEnumerable<StoreItem>>(ConvertItems);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Result { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint PriceSheetVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        public KeyValue PriceSheet { get; } = new("PriceSheet");

        /// <summary>
        /// 
        /// </summary>
        public Lazy<IEnumerable<StoreItem>> PriceSheetItems { get; }

        private IEnumerable<StoreItem> ConvertItems()
        {
            foreach (KeyValue entry in PriceSheet["store"]["entries"].Children)
            {
                string? itemLink = entry["item_link"].AsString();
                if (itemLink == null)
                {
                    continue;
                }

                yield return new StoreItem(itemLink, this.language).SetPrices(entry["prices"], entry["sale_prices"]);
            }
        }
    }
}
