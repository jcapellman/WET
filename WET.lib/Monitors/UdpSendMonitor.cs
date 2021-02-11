using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class UdpSendMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.NetworkTCPIP;

        public override MonitorTypes MonitorType => MonitorTypes.UdpSend;

        public override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = (Microsoft.Diagnostics.Tracing.Parsers.Kernel.UdpIpTraceData)eventData;

            return new UdpSendMonitorItem
            {
                DestinationIP = obj.daddr.ToString(),
                DestinationPort = obj.dport,
                ProcessName = obj.ProcessName,
                ProcessID = obj.ProcessID,
                Size = obj.size
            };
        }
    }
}