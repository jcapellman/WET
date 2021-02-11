using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class ImageUnloadMonitorItem : BaseMonitorItem
    {
        public string FileName { get; internal set; }
        
        public int ThreadID { get; internal set; }
    }
}