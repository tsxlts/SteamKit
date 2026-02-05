
using Newtonsoft.Json;
using static SteamKit.Model.BeginAuthSessionViaCredentialsResponse;

namespace SteamKit.Model
{
    /// <summary>
    /// 二维码登录开始授权
    /// </summary>
    public class BeginAuthSessionViaQRResponse
    {
        /// <summary>
        /// ClientId
        /// </summary>
        [JsonProperty("client_id")]
        public string? ClientId { get; set; }

        /// <summary>
        /// RequestId
        /// </summary>
        [JsonProperty("request_id")]
        public string? RequestId { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        [JsonProperty("interval")]
        public double Interval { get; set; }

        /// <summary>
        /// ChallengeUrl
        /// </summary>
        [JsonProperty("challenge_url")]
        public string? ChallengeUrl { get; set; }

        /// <summary>
        /// AllowedConfirmations
        /// </summary>
        [JsonProperty("allowed_confirmations")]
        public List<Confirmations>? AllowedConfirmations { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        [JsonProperty("version")]
        public long Version { get; set; }
    }

}
