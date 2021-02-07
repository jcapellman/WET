namespace WET.lib.MonitorItems
{
    public class ProcessStartMonitorItem
    {
        public string FileName { get; internal set; }

        public int ParentProcessID { get; internal set; }

        public string CommandLineArguments { get; internal set; }
    }
}