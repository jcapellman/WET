using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class FileReadMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.DiskFileIO |
                                                                      KernelTraceEventParser.Keywords.FileIOInit |
                                                                      KernelTraceEventParser.Keywords.FileIO;

        public override MonitorTypes MonitorType => MonitorTypes.FileRead;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.DiskIOTraceData)eventData;

            return new FileReadMonitorItem
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID
            };
        }
    }
}