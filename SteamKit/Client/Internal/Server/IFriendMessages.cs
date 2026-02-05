using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Internal.Server
{
    internal interface IFriendMessages
    {
        public CFriendMessages_GetRecentMessages_Response GetRecentMessages(CFriendMessages_GetRecentMessages_Request request);

        public CFriendMessages_SendMessage_Response SendMessage(CFriendMessages_SendMessage_Request request);

        public NoResponse AckMessage(CFriendMessages_AckMessage_Notification request);
    }
}
