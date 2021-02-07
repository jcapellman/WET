using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing.Session;

using WET.lib.Enums;
using WET.lib.Extensions;
using WET.lib.MonitorItems;

namespace WET.lib
{
    public class ETWMonitor
    {
        public const string DefaultSessionName = nameof(ETWMonitor);

        private readonly CancellationTokenSource _ctSource = new();

        private TraceEventSession _session;

        public event EventHandler<ImageLoadMonitorItem> OnImageLoad;

        public event EventHandler<ProcessStartMonitorItem> OnProcessStart;

        public void Start(string sessionName = DefaultSessionName, MonitorTypes monitorTypes = MonitorTypes.ImageLoad | MonitorTypes.ProcessStart)
        {
            Task.Run(() =>
            {
                _session = new TraceEventSession(sessionName);
                
                _session.EnableKernelProvider(monitorTypes.ToKeywords());

                foreach (MonitorTypes monitorType in Enum.GetValues(typeof(MonitorTypes)))
                {
                    if (!monitorTypes.HasFlag(monitorType))
                    {
                        continue;
                    }

                    switch (monitorType)
                    {
                        case MonitorTypes.ImageLoad:
                            _session.Source.Kernel.ImageLoad += Kernel_ImageLoad;
                            
                            break;
                        case MonitorTypes.ProcessStart:
                            _session.Source.Kernel.ProcessStart += Kernel_ProcessStart;
                            
                            break;
                    }
                }
                
                _session.Source.Process();
            }, _ctSource.Token);
        }

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData obj)
        {
            var item = new ProcessStartMonitorItem
            {
                FileName = obj.ImageFileName,
                ParentProcessID = obj.ParentID,
                CommandLineArguments = obj.CommandLine
            };

            OnProcessStart?.Invoke(this, item);
        }

        public void Stop()
        {
            _ctSource.Cancel();

            _session.Stop(true);
        }

        private void Kernel_ImageLoad(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj)
        {
            var item = new ImageLoadMonitorItem()
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID,
                ThreadID = obj.ThreadID
            };

            OnImageLoad?.Invoke(this, item);
        }
    }
}