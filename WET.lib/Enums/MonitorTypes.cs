using System;

namespace WET.lib.Enums
{
    [Flags]
    public enum MonitorTypes
    {
        ImageLoad = 1,
        ImageUnload = 2,
        ProcessStart = 4,
        ProcessStop = 8,
        FileRead = 16,
        FileWrite = 32,
        FileDelete = 64,
        RegistryUpdate = 128,
        RegistryDelete = 256,
        RegistryCreate = 512,
        TcpConnect = 1024
    }
}