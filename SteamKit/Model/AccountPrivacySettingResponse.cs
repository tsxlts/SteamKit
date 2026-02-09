using Newtonsoft.Json;
using static SteamKit.Enums;

namespace SteamKit.Model
{
    /// <summary>
    /// 库存隐私设置响应
    /// </summary>
    public class AccountPrivacySettingResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("success")]
        public virtual ErrorCodes ErrorCode { get; set; }

        /// <summary>
        /// 隐私设置
        /// </summary>
        [JsonProperty("Privacy", NullValueHandling = NullValueHandling.Ignore)]
        public AccountPrivacy? Privacy { get; set; }
    }

    /// <summary>
    /// 隐私设置
    /// </summary>
    public class AccountPrivacy
    {
        /// <summary>
        /// 隐私设置
        /// </summary>
        [JsonProperty("PrivacySettings", NullValueHandling = NullValueHandling.Ignore)]
        public AccountPrivacySettings PrivacySettings { get; set; } = new AccountPrivacySettings();

        /// <summary>
        /// 在我的个人资料页面留言
        /// </summary>
        [JsonProperty("eCommentPermission")]
        public CommentPermission CommentPermission { get; set; }
    }

    /// <summary>
    /// 隐私设置
    /// </summary>
    public class AccountPrivacySettings
    {
        /// <summary>
        /// 个人资料
        /// </summary>
        [JsonProperty("PrivacyProfile")]
        public CommunityVisibilityState PrivacyProfile { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [JsonProperty("PrivacyInventory")]
        public CommunityVisibilityState PrivacyInventory { get; set; }

        /// <summary>
        /// Steam礼物
        /// </summary>
        [JsonProperty("PrivacyInventoryGifts")]
        public CommunityVisibilityState PrivacyInventoryGifts { get; set; }

        /// <summary>
        /// 游戏详情
        /// </summary>
        [JsonProperty("PrivacyOwnedGames")]
        public CommunityVisibilityState PrivacyOwnedGames { get; set; }

        /// <summary>
        /// 游戏时间
        /// </summary>
        [JsonProperty("PrivacyPlaytime")]
        public CommunityVisibilityState PrivacyPlaytime { get; set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        [JsonProperty("PrivacyFriendsList")]
        public CommunityVisibilityState PrivacyFriendsList { get; set; }
    }
}
