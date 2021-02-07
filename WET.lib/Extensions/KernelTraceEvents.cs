using System;
using Microsoft.Diagnostics.Tracing.Parsers;
using WET.lib.Enums;

namespace WET.lib.Extensions
{
    public static class KernelTraceEventsExtensions
    {
        public static KernelTraceEventParser.Keywords ToKeywords(this MonitorTypes monitorTypes)
        {
            var kernelFlags = KernelTraceEventParser.Keywords.None;

            foreach (MonitorTypes monitorType in Enum.GetValues(typeof(MonitorTypes)))
            {
                if (!monitorTypes.HasFlag(monitorType))
                {
                    continue;
                }

                switch (monitorType)
                {
                    case MonitorTypes.ImageLoad:
                        kernelFlags |= KernelTraceEventParser.Keywords.ImageLoad;
                        break;
                    case MonitorTypes.ProcessStart:
                        kernelFlags |= KernelTraceEventParser.Keywords.Process;
                        break;
                }
            }

            return kernelFlags;
        }
    }
}