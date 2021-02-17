using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class TcpDisconnectMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.NetworkTCPIP;

        public override MonitorTypes MonitorType => MonitorTypes.TcpConnect;

        public override Type ExpectedEventDataType => typeof(TcpIpConnectTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as TcpIpConnectTraceData;
            
            return new TcpDisconnectMonitorItem
            {
                DestinationIP = obj.daddr.ToString(),
                DestinationPort = obj.dport,
                ProcessName = obj.ProcessName,
                ProcessID = obj.ProcessID
            };
        }
    }
}