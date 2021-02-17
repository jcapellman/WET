using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class UdpReceiveMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => 
            KernelTraceEventParser.Keywords.NetworkTCPIP;

        public override MonitorTypes MonitorType => MonitorTypes.UdpReceive;

        public override Type ExpectedEventDataType => typeof(UdpIpTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as UdpIpTraceData;

            return new UdpReceiveMonitorItem
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