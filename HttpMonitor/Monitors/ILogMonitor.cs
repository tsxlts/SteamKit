namespace HttpMonitor.Injector.Monitors
{
    public interface ILogMonitor
    {
        void LogMessage(string message);
        void ReportError(string error);
    }
}
