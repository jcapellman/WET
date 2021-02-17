using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class RegistryOpenMonitor  : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Registry;

        public override MonitorTypes MonitorType => MonitorTypes.RegistryOpen;

        public override Type ExpectedEventDataType => typeof(RegistryTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as RegistryTraceData;

            return new RegistryOpenMonitorItem
            {
                ProcessID = obj.ProcessID,
                KeyName = obj.KeyName,
                ProcessName = obj.ProcessName
            };
        }
    }
}