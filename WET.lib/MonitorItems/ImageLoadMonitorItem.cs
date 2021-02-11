using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class ImageLoadMonitorItem : BaseMonitorItem
    {
        public string FileName { get; internal set; }
        
        public int ThreadID { get; internal set; }
    }
}