using System;

namespace WET.lib.Enums
{
    [Flags]
    public enum MonitorTypes
    {
        All = 1,
        ImageLoad = 2,
        ImageUnload = 4,
        ProcessStart = 8,
        ProcessStop = 16,
        FileRead = 32,
        FileWrite = 64,
        FileDelete = 128,
        RegistryUpdate = 256,
        RegistryDelete = 512,
        RegistryCreate = 1024,
        TcpConnect = 2048,
        TcpDisconnect = 4096,
        TcpReceive = 8192,
        TcpSend = 16384,
        UdpSend = 32768,
        UdpReceive = 65536,
        RegistryOpen = 131072
    }
}