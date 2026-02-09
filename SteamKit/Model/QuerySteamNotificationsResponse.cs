using Newtonsoft.Json;
using static SteamKit.Enums;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询Steam通知响应
    /// </summary>
    public class QuerySteamNotificationsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("notifications", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notification> Notifications { get; set; } = new List<Notification>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("confirmation_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ConfirmationCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_gift_count", NullValueHandling = NullValueHandling.Ignore)]
        public int PendingGiftCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_friend_count", NullValueHandling = NullValueHandling.Ignore)]
        public int PendingFriendCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_family_invite_count", NullValueHandling = NullValueHandling.Ignore)]
        public int PendingFamilyInviteCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("unread_count", NullValueHandling = NullValueHandling.Ignore)]
        public int UnreadCount { get; set; }
    }

    /// <summary>
    /// Steam通知
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("notification_id", NullValueHandling = NullValueHandling.Ignore)]
        public ulong NotificationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("notification_targets", NullValueHandling = NullValueHandling.Ignore)]
        public SteamNotificationTargets NotificationTargets { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("notification_type", NullValueHandling = NullValueHandling.Ignore)]
        public SteamNotificationType NotificationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("body_data", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyData { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("read", NullValueHandling = NullValueHandling.Ignore)]
        public bool Read { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool Hidden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long Expiry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("viewed", NullValueHandling = NullValueHandling.Ignore)]
        public long Viewed { get; set; }
    }
}
