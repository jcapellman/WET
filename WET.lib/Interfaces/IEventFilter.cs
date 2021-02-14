using WET.lib.Enums;

namespace WET.lib.Interfaces
{
    public interface IEventFilter
    {
        public bool IsFilteredOut(MonitorTypes monitorType, object payload);
    }
}