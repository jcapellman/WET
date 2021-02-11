using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class UdpReceiveMonitorItem : BaseMonitorItem
    {
        public int Size { get; internal set; }
        
        public string DestinationIP { get; internal set; }

        public int DestinationPort { get; internal set; }
    }
}