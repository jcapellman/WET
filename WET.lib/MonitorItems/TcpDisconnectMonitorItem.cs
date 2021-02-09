namespace WET.lib.MonitorItems
{
    public class TcpDisconnectMonitorItem
    {
        public int DestinationPort { get; internal set; }

        public string DestinationIP { get; internal set; }

        public int ProcessID { get; internal set; }

        public string ProcessName { get; internal set; }
    }
}