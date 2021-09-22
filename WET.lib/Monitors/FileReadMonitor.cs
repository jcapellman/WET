using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class FileReadMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.DiskFileIO |
                                                                      KernelTraceEventParser.Keywords.DiskIO |
                                                                      KernelTraceEventParser.Keywords.FileIOInit |
                                                                      KernelTraceEventParser.Keywords.FileIO;

        public override MonitorTypes MonitorType => MonitorTypes.FileRead;

        public override Type ExpectedEventDataType => typeof(DiskIOTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as DiskIOTraceData;

            if (string.IsNullOrEmpty(obj.FileName))
            {
                return null;
            }

            return new FileReadMonitorItem
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID
            };
        }

        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}