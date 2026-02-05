
namespace SteamKit.Attributes
{
    /// <summary>
    /// Language
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LanguageAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiCode">
        /// API 语言代码
        /// schinese
        /// </param>
        /// <param name="webApiCode">
        /// Web API 语言代码
        /// zh-CN
        /// </param>
        public LanguageAttribute(string apiCode, string webApiCode)
        {
            ApiCode = apiCode;
            WebApiCode = webApiCode;
        }

        /// <summary>
        /// API 语言代码
        /// schinese
        /// </summary>
        public string ApiCode { get; set; }

        /// <summary>
        /// Web API 语言代码
        /// zh-CN
        /// </summary>
        public string WebApiCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
