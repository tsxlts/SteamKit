using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询国家响应
    /// </summary>
    public class QueryCountryResponse
    {
        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("countries", NullValueHandling = NullValueHandling.Ignore)]
        public List<Country> Countries { get; set; } = new List<Country>();
    }

    /// <summary>
    /// 国家
    /// </summary>
    public class Country
    {
        /// <summary>
        /// 国家编码
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 国家名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
