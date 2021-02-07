using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ProcessStartMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Process;

        public override MonitorTypes MonitorType => MonitorTypes.ProcessStart;
    }
}