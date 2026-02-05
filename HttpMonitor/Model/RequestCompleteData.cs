using System;

namespace HttpMonitor.Injector.Model
{
    [Serializable]
    public class RequestCompleteData
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public string RequestHeaders { get; set; }

        public string RequestBody { get; set; }

        public int StatusCode { get; set; }

        public string ResponseHeaders { get; set; }

        public string ResponseBody { get; set; }

        public long DurationMs { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string SourceProcess { get; set; }
    }
}
