using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class ImageUnloadMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.ImageLoad;

        public override MonitorTypes MonitorType => MonitorTypes.ImageUnload;

        public override Type ExpectedEventDataType => typeof(ImageLoadTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as ImageLoadTraceData;

            return new ImageUnloadMonitorItem
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID,
                ThreadID = obj.ThreadID,
                ProcessName = obj.ProcessName
            };
        }
        
        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}