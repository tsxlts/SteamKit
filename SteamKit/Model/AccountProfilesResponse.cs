using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 用户个人资料响应
    /// </summary>
    public class AccountProfilesResponse
    {
        /// <summary>
        /// 个人资料名称
        /// 昵称
        /// </summary>
        [JsonProperty("strPersonaName")]
        public string PersonaName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("strFilteredPersonaName")]
        public string? FilteredPersonaName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty("strRealName")]
        public string? RealName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("strFilteredRealName")]
        public string? FilteredRealName { get; set; }

        /// <summary>
        /// 自定义链接
        /// </summary>
        [JsonProperty("strCustomURL")]
        public string? CustomURL { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [JsonProperty("strSummary")]
        public string? Summary { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty("strAvatarHash")]
        public string? AvatarHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rtPersonaNameBannedUntil")]
        public int PersonaNameBannedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rtProfileSummaryBannedUntil")]
        public int ProfileSummaryBannedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rtAvatarBannedUntil")]
        public int AvatarBannedUntil { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [JsonProperty("LocationData", NullValueHandling = NullValueHandling.Ignore)]
        public LocationData Location { get; set; } = new LocationData();

        /// <summary>
        /// 个人资料偏好
        /// </summary>
        [JsonProperty("ProfilePreferences", NullValueHandling = NullValueHandling.Ignore)]
        public ProfilePreferences ProfilePreferences { get; set; } = new ProfilePreferences();

        /// <summary>
        /// 隐私设置
        /// </summary>
        [JsonProperty("Privacy", NullValueHandling = NullValueHandling.Ignore)]
        public AccountPrivacy Privacy { get; set; } = new AccountPrivacy();
    }

    /// <summary>
    /// 位置信息
    /// </summary>
    public class LocationData
    {
        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("locCountry")]
        public string? Country { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        [JsonProperty("locCountryCode")]
        public string? CountryCode { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [JsonProperty("locState")]
        public string? State { get; set; }

        /// <summary>
        /// 省份代码
        /// </summary>
        [JsonProperty("locStateCode")]
        public string? StateCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty("locCity")]
        public string? City { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        [JsonProperty("locCityCode")]
        public string? CityCode { get; set; }
    }

    /// <summary>
    /// 个人资料偏好
    /// </summary>
    public class ProfilePreferences
    {
        /// <summary>
        /// 在个人资料中隐藏社区奖励
        /// </summary>
        [JsonProperty("hide_profile_awards")]
        public bool HideProfileAwards { get; set; }
    }
}
