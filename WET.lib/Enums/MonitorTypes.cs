using System;

namespace WET.lib.Enums
{
    [Flags]
    public enum MonitorTypes
    {
        ImageLoad = 1,
        ProcessStart = 2,
        ProcessStop = 4
    }
}