using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Internal.Server
{
    internal interface IEcon
    {
        public CEcon_GetInventoryItemsWithDescriptions_Response GetInventoryItemsWithDescriptions(CEcon_GetInventoryItemsWithDescriptions_Request message);

        public CEcon_GetAssetClassInfo_Response GetAssetClassInfo(CEcon_GetAssetClassInfo_Request message);

        public CEcon_GetTradeOfferAccessToken_Response GetTradeOfferAccessToken(CEcon_GetTradeOfferAccessToken_Request message);
    }
}
