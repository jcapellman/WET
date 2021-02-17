using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class TcpSendMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => 
            KernelTraceEventParser.Keywords.NetworkTCPIP;
        
        public override MonitorTypes MonitorType => MonitorTypes.TcpSend;

        public override Type ExpectedEventDataType => typeof(TcpIpSendTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as TcpIpSendTraceData;

            return new TcpSendMonitorItem
            {
                DestinationIP = obj.daddr.ToString(),
                DestinationPort = obj.dport,
                Size = obj.size,
                ProcessID = obj.ProcessID
            };
        }
    }
}