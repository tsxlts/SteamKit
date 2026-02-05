
namespace SteamKit.Model
{
    /// <summary>
    /// 用户简介响应
    /// </summary>
    public class PlayerProfilesResponse
    {
        /// <summary>  
        /// SteamId  
        /// </summary>  
        public string SteamId { set; get; } = string.Empty;

        /// <summary>  
        /// 用户昵称  
        /// </summary>  
        public string SteamName { set; get; } = string.Empty;

        /// <summary>  
        /// 用户小头像  
        /// </summary>  
        public string Avatar { set; get; } = string.Empty;

        /// <summary>  
        /// 用户中等像  
        /// </summary>  
        public string AvatarMedium { set; get; } = string.Empty;

        /// <summary>  
        /// 用户大头像  
        /// </summary>  
        public string AvatarFull { set; get; } = string.Empty;

        /// <summary>  
        /// 真实姓名  
        /// </summary>  
        public string Realname { set; get; } = string.Empty;

        /// <summary>
        /// 自定义个人资料链接
        /// </summary>
        public string CustomURL { set; get; } = string.Empty;

        /// <summary>
        /// VAC封禁
        /// </summary>
        public bool VacBanned { get; set; }

        /// <summary>
        /// 受限帐户
        /// </summary>
        public bool IsLimitedAccount { get; set; }
    }
}
