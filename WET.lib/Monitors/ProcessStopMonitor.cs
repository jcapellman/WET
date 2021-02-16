using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ProcessStopMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Process;

        public override MonitorTypes MonitorType => MonitorTypes.ProcessStop;

        public override Type ExpectedEventDataType => typeof(ProcessTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as ProcessTraceData;
            
            return new ProcessStopMonitorItem
            {
                FileName = obj.ImageFileName,
                ProcessID = obj.ProcessID
            };
        }
    }
}