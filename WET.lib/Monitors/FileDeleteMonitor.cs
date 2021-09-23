using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class FileDeleteMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.DiskFileIO |
                                                                      KernelTraceEventParser.Keywords.DiskIO |
                                                                      KernelTraceEventParser.Keywords.FileIOInit |
                                                                      KernelTraceEventParser.Keywords.FileIO;

        public override MonitorTypes MonitorType => MonitorTypes.FileDelete;

        public override Type ExpectedEventDataType => typeof(FileIONameTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as FileIONameTraceData;

            if (string.IsNullOrEmpty(obj.FileName))
            {
                return null;
            }

            return new FileDeleteMonitorItem
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID
            };
        }

        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}