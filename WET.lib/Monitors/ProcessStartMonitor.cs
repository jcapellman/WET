using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ProcessStartMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.Process;

        public override MonitorTypes MonitorType => MonitorTypes.ProcessStart;

        public override Type ExpectedEventDataType => typeof(ProcessTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as ProcessTraceData;

            return new ProcessStartMonitorItem
            {
                FileName = obj.ImageFileName,
                ParentProcessID = obj.ParentID,
                CommandLineArguments = obj.CommandLine,
                ProcessID = obj.ProcessID,
                ProcessName = obj.ProcessName
            };
        }
        
        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}