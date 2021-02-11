namespace WET.lib.MonitorItems
{
    public class TcpReceiveMonitorItem
    {
        public int ProcessID { get; internal set; }

        public int Size { get; internal set; }

        public string SenderIP { get; internal set; }

        public int SenderPort { get; internal set; }
    }
}