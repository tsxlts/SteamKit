using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// Api响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Response
        /// </summary>
        [JsonProperty("response")]
        public T? Response { get; set; }
    }
}
