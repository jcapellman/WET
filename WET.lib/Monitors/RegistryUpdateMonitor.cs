using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class RegistryUpdateMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Registry;
        
        public override MonitorTypes MonitorType => MonitorTypes.RegistryUpdate;

        public override Type ExpectedEventDataType => typeof(RegistryTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as RegistryTraceData;

            return new RegistryUpdateMonitorItem
            {
                ProcessName = obj.ProcessName,
                ValueName = obj.ValueName,
                ProcessID = obj.ProcessID
            };
        }

        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}