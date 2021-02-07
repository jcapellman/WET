using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing.Session;

using WET.lib.Enums;
using WET.lib.Extensions;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib
{
    public class ETWMonitor
    {
        public const string DefaultSessionName = nameof(ETWMonitor);

        private readonly CancellationTokenSource _ctSource = new();

        private TraceEventSession _session;

        public event EventHandler<ImageLoadMonitorItem> OnImageLoad;

        public event EventHandler<ProcessStartMonitorItem> OnProcessStart;

        private readonly List<BaseMonitor> _monitors;

        public ETWMonitor()
        {
            _monitors = this.GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseMonitor))
                .Select(a => (BaseMonitor) Activator.CreateInstance(a)).ToList();
        }

        public void Start(string sessionName = DefaultSessionName, MonitorTypes monitorTypes = MonitorTypes.ImageLoad | MonitorTypes.ProcessStart)
        {
            Task.Run(() =>
            {
                _session = new TraceEventSession(sessionName);

                var enabledMonitors = _monitors.Where(a => monitorTypes.HasFlag(a.MonitorType)).ToList();

                _session.EnableKernelProvider(enabledMonitors.Select(a => a.KeyWordMap).ToKeywords());

                foreach (var monitor in enabledMonitors)
                {
                    switch (monitor.MonitorType)
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