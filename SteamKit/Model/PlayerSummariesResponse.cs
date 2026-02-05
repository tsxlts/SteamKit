using Newtonsoft.Json;
using static SteamKit.SteamEnum;

namespace SteamKit.Model
{
    /// <summary>
    /// 用户信息响应
    /// </summary>
    public class PlayerSummariesResponse
    {
        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty("players")]
        public IEnumerable<PlayerSummaries>? Players { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class PlayerSummaries
    {
        /// <summary>  
        /// SteamId  
        /// </summary>  
        [JsonProperty("steamid")]
        public string SteamId { set; get; } = string.Empty;

        /// <summary>  
        /// 用户昵称  
        /// </summary>  
        [JsonProperty("personaname")]
        public string SteamName { set; get; } = string.Empty;

        /// <summary>  
        /// 用户steam社区状态
        /// 表示个人资料是否可见，以及如果可见，为什么允许您查看它。
        /// 请注意，由于此 WebAPI 不使用身份验证，
        /// 因此仅返回两个可能的值：
        /// 1 - 该个人资料对您不可见（私人、仅限好友等），
        /// 3 - 该个人资料是“公开”的，并且数据可见
        /// </summary>
        [JsonProperty("communityvisibilitystate")]
        public CommunityVisibilityState CommunityVisibilityState { set; get; }

        /// <summary>  
        /// 用户个人资料状态，应该是个人性别地址等信息状态是否设置为隐私
        /// </summary>
        [JsonProperty("profilestate")]
        public int ProfileState { set; get; }

        /// <summary>
        /// 用户状态
        /// 如果玩家的个人资料是私人的，
        /// 则该值将始终为“离线”，
        /// 除非用户已将其状态设置为寻求交易或寻求玩游戏，
        /// 因为即使个人资料是私人的，
        /// 错误也会导致这些状态出现。
        /// </summary>
        [JsonProperty("personastate")]
        public PersonaState PersonaState { set; get; }

        /// <summary>  
        /// 个人主页地址
        /// </summary>  
        [JsonProperty("profileurl")]
        public string ProfileUrl { set; get; } = string.Empty;

        /// <summary>  
        /// 用户小头像  
        /// </summary>  
        [JsonProperty("avatar")]
        public string Avatar { set; get; } = string.Empty;

        /// <summary>  
        /// 用户中等像  
        /// </summary>  
        [JsonProperty("avatarmedium")]
        public string AvatarMedium { set; get; } = string.Empty;

        /// <summary>  
        /// 用户大头像  
        /// </summary>  
        [JsonProperty("avatarfull")]
        public string AvatarFull { set; get; } = string.Empty;

        /// <summary>
        /// 头像Hash值
        /// </summary>
        [JsonProperty("avatarhash")]
        public string AvatarHash { set; get; } = string.Empty;

        /// <summary>  
        /// 用户steam账号创建时间
        /// 秒时间戳
        /// </summary>
        [JsonProperty("timecreated")]
        public long TimeCreated { set; get; }

        /// <summary>  
        /// 最后一次离线时间
        /// 秒时间戳
        /// </summary>
        [JsonProperty("lastlogoff")]
        public long LastLogoff { set; get; }

        /// <summary>  
        /// 真实姓名  
        /// </summary>  
        [JsonProperty("realname")]
        public string Realname { set; get; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("primaryclanid")]
        public string PrimaryClanId { set; get; } = string.Empty;
    }
}
