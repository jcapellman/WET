using System;

using Microsoft.O365.Security.ETW;

using Win32DLLHookMonitor;

namespace DLLMonitor
{
    public class DLLMonitor
    {
        private UserTrace _trace;
        
        public void Start()
        {
            _trace = new UserTrace();

            var processProvider = new Provider("Microsoft-Windows-Kernel-Process");
            
            processProvider.All = 0x40;

            var filter = new EventFilter(Filter.EventIdIs(5));

            filter.OnEvent += Filter_OnEvent;

            processProvider.AddFilter(filter);
            _trace.Enable(processProvider);

            _trace.Start();
        }

        private void Filter_OnEvent(IEventRecord record)
        {
            var dllName = record.GetUnicodeString("ImageName", "<UNKNOWN>");

            if (dllName.Contains("System32") || dllName.Contains("Program Files"))
            {
                return;
            }

            var fullPath = DevicePathMapper.FromDevicePath(dllName);

            Console.WriteLine(fullPath);
        }

        public void Stop()
        {
            _trace.Stop();
        }
    }
}