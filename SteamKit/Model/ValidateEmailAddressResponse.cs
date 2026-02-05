
using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 邮箱验证响应
    /// </summary>
    public class ValidateEmailAddressResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("was_validated")]
        public bool WasValidated { get; set; }
    }
}
