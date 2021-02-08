using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;

namespace WET.lib.Monitors.Base
{
    public abstract class BaseMonitor
    {
        public abstract KernelTraceEventParser.Keywords KeyWordMap { get; }

        public abstract MonitorTypes MonitorType { get; }

        public abstract object ParseTraceEvent(TraceEvent eventData);
    }
}