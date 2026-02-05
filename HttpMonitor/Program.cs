using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Threading;
using EasyHook;
using HttpMonitor.Injectors;

namespace HttpMonitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("请输入要监控的进程ID或名称: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int pid))
                    {
                        InjectToProcess(pid);
                    }
                    else
                    {
                        InjectByProcessName(input);
                    }


                    Console.WriteLine("\n监控运行中... 按 Q 键退出");
                    while (Console.ReadKey(true).Key != ConsoleKey.Q)
                    {
                        Thread.Sleep(100);
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"程序错误: {ex.Message}");
                }
            }
        }

        static void InjectByProcessName(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                throw new ArgumentException($"未找到进程: {processName}");
            }

            if (processes.Length > 1)
            {
                throw new ArgumentException($"找到多个同名进程");
            }

            InjectToProcess(processes[0].Id);
        }

        static void InjectToProcess(int processId)
        {
            var process = Process.GetProcessById(processId);

            string channelName = null;
            RemoteHooking.IpcCreateServer<Internal.HttpMonitor>(ref channelName, WellKnownObjectMode.Singleton);

            RemoteHooking.Inject(
                processId,
                InjectionOptions.DoNotRequireStrongName,
                typeof(HttpMonitorInjector).Assembly.Location,
                typeof(HttpMonitorInjector).Assembly.Location,
                channelName
            );
        }
    }
}
