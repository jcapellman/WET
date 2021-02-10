namespace WET.lib.MonitorItems
{
    public class TcpSendMonitorItem
    {
        public int ProcessID { get; internal set; }

        public int Size { get; internal set; }

        public string DestinationIP { get; internal set; }

        public int DestinationPort { get; internal set; }
    }
}