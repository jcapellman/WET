using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class TcpConnectMonitorItem : BaseMonitorItem
    {
        public int DestinationPort { get; internal set; }

        public string DestinationIP { get; internal set; }
    }
}