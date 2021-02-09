using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ImageUnloadMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.ImageLoad;

        public override MonitorTypes MonitorType => MonitorTypes.ImageUnload;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData)eventData;

            return new ImageUnloadMonitorItem
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID,
                ThreadID = obj.ThreadID
            };
        }
    }
}