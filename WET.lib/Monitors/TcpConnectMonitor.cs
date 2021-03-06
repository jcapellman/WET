using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class TcpConnectMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => KernelTraceEventParser.Keywords.NetworkTCPIP;

        public override MonitorTypes MonitorType => MonitorTypes.TcpConnect;

        public override Type ExpectedEventDataType => typeof(TcpIpConnectTraceData);

        protected override object ParseKernelTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as TcpIpConnectTraceData;

            return new TcpConnectMonitorItem
            {
                DestinationIP = obj.daddr.ToString(),
                DestinationPort = obj.dport,
                ProcessName = obj.ProcessName,
                ProcessID = obj.ProcessID
            };
        }

        protected override object ParseTraceEvent(object eventData) => throw new NotImplementedException();
    }
}