using System;
using System.Collections.Generic;
using System.Threading;
using EasyHook;
using HttpMonitor.Hooks;
using HttpMonitor.Injector.Hooks;
using HttpMonitor.Monitors;

namespace HttpMonitor.Injectors
{
    public class HttpMonitorInjector : IEntryPoint
    {
        private static List<HttpMonitorInjector> _activeInstances = new List<HttpMonitorInjector>();
        private static object _instancesLock = new object();

        private readonly IHttpMonitor monitor;
        private readonly WinHttpHook winHttpHook;
        private readonly WinsockHook winsockHook;

        public HttpMonitorInjector(RemoteHooking.IContext context, string channelName)
        {
            monitor = RemoteHooking.IpcConnectClient<Internal.HttpMonitor>(channelName);

            winHttpHook = new WinHttpHook(monitor);
            winsockHook = new WinsockHook(monitor);

            monitor.Ping();
            monitor.IsInstalled(RemoteHooking.GetCurrentProcessId());

            lock (_instancesLock)
            {
                _activeInstances.Add(this);
            }
        }

        public void Run(RemoteHooking.IContext context, string channelName)
        {
            try
            {
                winHttpHook.SetupHooks();

                winsockHook.SetupHooks();

                while (true)
                {
                    Thread.Sleep(1000);
                    winHttpHook.CleanupClosedHandles();
                }
            }
            catch (Exception ex)
            {
                monitor?.ReportError($"注入器运行错误: {ex.Message}");
            }
        }
    }
}
