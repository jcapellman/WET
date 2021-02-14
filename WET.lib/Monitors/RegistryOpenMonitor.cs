using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class RegistryOpenMonitor  : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Registry;

        public override MonitorTypes MonitorType => MonitorTypes.RegistryOpen;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData)eventData;

            return new RegistryOpenMonitorItem
            {
                ProcessID = obj.ProcessID,
                KeyName = obj.KeyName,
                ProcessName = obj.ProcessName
            };
        }
    }
}