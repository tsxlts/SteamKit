using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Internal.Server
{
    internal interface IPlayer
    {
        CPlayer_GetPrivacySettings_Response GetPrivacySettings(CPlayer_GetPrivacySettings_Request request);

        CPlayer_GetOwnedGames_Response GetOwnedGames(CPlayer_GetOwnedGames_Request request);
    }
}
