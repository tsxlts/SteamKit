
namespace SteamKit.Client.Model.Proto
{
    [global::ProtoBuf.ProtoContract()]
    public class CSteamNotification_NotificationsReceived_Notification : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public List<SteamNotificationData> notifications { get; } = new List<SteamNotificationData>();

        [global::ProtoBuf.ProtoMember(2)]
        public uint pending_gift_count { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public uint pending_friend_count { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public uint pending_family_invite_count { get; set; }
    }

    [global::ProtoBuf.ProtoContract()]
    public class SteamNotificationData : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong notification_id { get; set; }

        /// <summary>
        /// <see cref="ESteamNotificationTargets"/>
        /// </summary>
        [global::ProtoBuf.ProtoMember(2)]
        public uint notification_targets { get; set; }

        /// <summary>
        /// <see cref="ESteamNotificationType"/>
        /// </summary>
        [global::ProtoBuf.ProtoMember(3)]
        public int notification_type { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public string body_data { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public bool read { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public uint timestamp { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public bool hidden { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public uint expiry { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public uint viewed { get; set; }
    }

    /// <summary>
    /// Steam通知目标
    /// </summary>
    [global::ProtoBuf.ProtoContract()]
    public enum ESteamNotificationTargets
    {
        /// <summary>
        /// 不通知
        /// </summary>
        None = 0,

        /// <summary>
        /// 全部通知
        /// </summary>
        All = 1,

        /// <summary>
        /// 通知SteamApp
        /// </summary>
        SteamApp = 3,

        /// <summary>
        /// 通知Steam PC客户端
        /// </summary>
        SteamClient = 9,

        /// <summary>
        /// 通知SteamApp和Steam PC客户端
        /// </summary>
        SteamAppAndSteamClient = 11
    }

    [global::ProtoBuf.ProtoContract()]
    public enum ESteamNotificationType
    {
        k_ESteamNotificationType_Invalid = 0,
        k_ESteamNotificationType_Test = 1,
        k_ESteamNotificationType_Gift = 2,
        k_ESteamNotificationType_Comment = 3,
        k_ESteamNotificationType_Item = 4,
        k_ESteamNotificationType_FriendInvite = 5,
        k_ESteamNotificationType_MajorSale = 6,
        k_ESteamNotificationType_PreloadAvailable = 7,
        k_ESteamNotificationType_Wishlist = 8,
        k_ESteamNotificationType_TradeOffer = 9,
        k_ESteamNotificationType_General = 10,
        k_ESteamNotificationType_HelpRequest = 11,
        k_ESteamNotificationType_AsyncGame = 12,
        k_ESteamNotificationType_ChatMsg = 13,
        k_ESteamNotificationType_ModeratorMsg = 14,
        k_ESteamNotificationType_ParentalFeatureAccessRequest = 15,
        k_ESteamNotificationType_FamilyInvite = 16,
        k_ESteamNotificationType_FamilyPurchaseRequest = 17,
        k_ESteamNotificationType_ParentalPlaytimeRequest = 18,
        k_ESteamNotificationType_FamilyPurchaseRequestResponse = 19,
        k_ESteamNotificationType_ParentalFeatureAccessResponse = 20,
        k_ESteamNotificationType_ParentalPlaytimeResponse = 21,
        k_ESteamNotificationType_RequestedGameAdded = 22,
        k_ESteamNotificationType_SendToPhone = 23,
        k_ESteamNotificationType_ClipDownloaded = 24,
        k_ESteamNotificationType_2FAPrompt = 25,
        k_ESteamNotificationType_MobileConfirmation = 26,
        k_ESteamNotificationType_PartnerEvent = 27,
        k_ESteamNotificationType_PlaytestInvite = 28,
        k_ESteamNotificationType_TradeReversal = 29,
    }
}
