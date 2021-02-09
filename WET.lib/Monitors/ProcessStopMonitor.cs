using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ProcessStopMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Process;

        public override MonitorTypes MonitorType => MonitorTypes.ProcessStop;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData)eventData;
            
            return new ProcessStopMonitorItem
            {
                FileName = obj.ImageFileName,
                ProcessID = obj.ProcessID
            };
        }
    }
}