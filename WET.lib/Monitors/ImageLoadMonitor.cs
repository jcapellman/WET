using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ImageLoadMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.ImageLoad;

        public override MonitorTypes MonitorType => MonitorTypes.ImageLoad;
    }
}