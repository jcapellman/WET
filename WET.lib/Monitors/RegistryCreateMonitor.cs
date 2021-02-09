using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class RegistryCreateMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Registry;

        public override MonitorTypes MonitorType => MonitorTypes.RegistryCreate;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData)eventData;

            return new RegistryCreateMonitorItem
            {
                ProcessID = obj.ProcessID,
                KeyName = obj.KeyName,
                ProcessName = obj.ProcessName
            };
        }
    }
}