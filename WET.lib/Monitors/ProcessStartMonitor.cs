using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ProcessStartMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Process;

        public override MonitorTypes MonitorType => MonitorTypes.ProcessStart;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData) eventData;

            return new ProcessStartMonitorItem
            {
                FileName = obj.ImageFileName,
                ParentProcessID = obj.ParentID,
                CommandLineArguments = obj.CommandLine
            };
        }
    }
}