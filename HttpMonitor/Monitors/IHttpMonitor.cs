using HttpMonitor.Injector.Model;
using HttpMonitor.Injector.Monitors;

namespace HttpMonitor.Monitors
{
    internal interface IHttpMonitor : ILogMonitor
    {
        void ReportHttpRequest(string method, string url, string source);

        void ReportRequestHeaders(string headers);

        void ReportPostData(string data);

        void ReportResponseInfo(int statusCode, string headers);

        void ReportResponseChunk(string chunk);

        void ReportCompleteRequest(RequestCompleteData data);

        void Ping();

        void IsInstalled(int processId);
    }
}
