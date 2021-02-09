namespace WET.lib.MonitorItems
{
    public class ImageUnloadMonitorItem
    {
        public string FileName { get; internal set; }

        public int ProcessID { get; internal set; }

        public int ThreadID { get; internal set; }
    }
}