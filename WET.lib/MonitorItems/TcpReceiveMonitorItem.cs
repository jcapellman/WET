using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class TcpReceiveMonitorItem :  BaseMonitorItem
    {
        public int Size { get; internal set; }

        public string SenderIP { get; internal set; }

        public int SenderPort { get; internal set; }
    }
}