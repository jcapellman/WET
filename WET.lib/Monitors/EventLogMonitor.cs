using System;
using System.Diagnostics;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class EventLogMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.None;

        public override MonitorTypes MonitorType => MonitorTypes.EventLogError;

        public override Type ExpectedEventDataType => typeof(EventLogEntry);

        public new bool KernelEventTracing = false;

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            throw new NotImplementedException();
        }

        protected override object ParseTraceEvent(object eventData)
        {
            var obj = eventData as EventLogEntry;
            
            return new EventLogMonitorItem()
            {
                
            };
        }
    }
}