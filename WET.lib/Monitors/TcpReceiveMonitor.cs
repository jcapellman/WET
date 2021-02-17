using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib.Monitors
{
    public class TcpReceiveMonitor : BaseMonitor
    {
        public override KernelTraceEventParser.Keywords KeyWordMap => 
            KernelTraceEventParser.Keywords.NetworkTCPIP;

        public override MonitorTypes MonitorType => MonitorTypes.TcpReceive;

        public override Type ExpectedEventDataType => typeof(TcpIpTraceData);

        protected override object ParseTraceEvent(TraceEvent eventData)
        {
            var obj = eventData as TcpIpTraceData;

            return new TcpReceiveMonitorItem
            {
                SenderIP = obj.saddr.ToString(),
                SenderPort = obj.sport,
                Size = obj.size,
                ProcessID = obj.ProcessID
            };
        }
    }
}