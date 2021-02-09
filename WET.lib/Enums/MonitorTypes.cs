using System;

namespace WET.lib.Enums
{
    [Flags]
    public enum MonitorTypes
    {
        ImageLoad = 1,
        ImageUnload = 2,
        ProcessStart = 4,
        ProcessStop = 8
    }
}