using System;

using WET.lib.MonitorItems.Base;

namespace WET.lib.MonitorItems
{
    public class EventLogMonitorItem : BaseMonitorItem
    {
        
        public string EntryType { get; internal set; }
        
        public string UserName { get; internal set; }

        public long InstanceId { get; internal set; }

        public string Message { get; internal set; }

        public string Source { get; internal set; }

        public DateTime TimeWritten { get; internal set; }
    }
}