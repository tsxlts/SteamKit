
namespace SteamKit.Model
{
    /// <summary>
    /// 编辑个人资料请求
    /// </summary>
    public class EditAccountProfilesParameter
    {
        /// <summary>
        /// 个人资料名称
        /// 昵称
        /// </summary>
        public string PersonaName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string? RealName { get; set; }

        /// <summary>
        /// 自定义链接
        /// https://steamcommunity.com/id/<see cref="customURL"/>/
        /// </summary>
        public string? CustomURL { get; set; }

        /// <summary>
        /// 国家
        /// 国家代码
        /// </summary>
        public string? CountryCode { get; set; }

        /// <summary>
        /// 省份
        /// 省份代码
        /// </summary>
        public string? StateCode { get; set; }

        /// <summary>
        /// 城市
        /// 城市代码
        /// </summary>
        public string? CityCode { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 在个人资料中隐藏社区奖励
        /// </summary>
        public bool HideProfileAwards { get; set; } = false;
    }
}
