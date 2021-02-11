using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class ProcessStartMonitorItem : BaseMonitorItem
    {
        public string FileName { get; internal set; }

        public int ParentProcessID { get; internal set; }

        public string CommandLineArguments { get; internal set; }
    }
}