using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询服务器时间响应
    /// </summary>
    public class QueryServerTimeResponse
    {
        /// <summary>
        /// 服务器时间
        /// 秒时间戳
        /// </summary>
        [JsonProperty("server_time")]
        public long ServerTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("skew_tolerance_seconds")]
        public int SkewToleranceSeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("large_time_jink")]
        public int LargeTimeJink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("probe_frequency_seconds")]
        public int ProbeFrequencySeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("adjusted_time_probe_frequency_seconds")]
        public int AdjustedTimeProbeFrequencySeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hint_probe_frequency_seconds")]
        public int HintProbeFrequencySeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sync_timeout")]
        public int SyncTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("try_again_seconds")]
        public int TryAgainSeconds { get; set; }

        /// <summary>
        /// 最大尝试次数
        /// </summary>
        [JsonProperty("max_attempts")]
        public int MaxAttempts { get; set; }
    }
}
