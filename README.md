# WET (Windows Event Tracing) Library
The purpose of this library is to hook into various Event Tracing events on Windows with just 2 lines of code for the integrator.

The library currently hooks into the following events:
* DLL Load
* DLL Unload
* Process Start
* Process Stop
* Registry Key Create
* Registry Key Delete
* Registry Key Update
* TCP Connection
* TCP Disconnection
* TCP Receive
* TCP Send
* UDP Receive
* UDP Send

## Requirements
* .NET 5
* Windows 7 SP1+

## Installation
Either clone this repository or use the pre-built version that has been uploaded to NuGet.org. Below are two methods of adding the package to your existing project:

### Package Manager
```
Install-Package WET.lib -Version 0.3.0
```

### .NET CLI
```
dotnet add package WET.lib --version 0.3.0
```

## Roadmap
Hooks for the following are planned:
* File Create/Delete/Read/Update

## Usage
The library is designed for flexibility so you are not required to use all of the hooks the library provides. You can however use all of the hooks (and by default at least as of 0.3.0 - both the DLL Load and Process Start hooks are enabled).

## Examples
### DLL Load Hook
To get an event hook on every DLL Load simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnImageLoad += Monitor_OnImageLoad;
  
private void Monitor_OnImageLoad(object sender, lib.MonitorItems.ImageLoadMonitorItem e)
{
    Console.WriteLine($"{e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
}
```
### DLL Unload Hook
To get an event hook on every DLL Unload simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnImageUnload += _monitor_OnImageUnload;
  
private void _monitor_OnImageUnload(object sender, lib.MonitorItems.ImageUnloadMonitorItem e)
{
    Console.WriteLine($"DLL Unload: {e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
}
```
### Process Start Hook
To get an event hook on every DLL Load simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnProcessStart += _monitor_OnProcessStart;
  
private void _monitor_OnProcessStart(object sender, lib.MonitorItems.ProcessStartMonitorItem e)
{
    Console.WriteLine($"Process Start: {e.ParentProcessID}|{e.FileName}|{e.CommandLineArguments}{Environment.NewLine}");
}
```
### Process Stop Hook
To get an event hook on every Process Stop simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnProcessStop += _monitor_OnProcessStop
  
private void _monitor_OnProcessStop(object sender, lib.MonitorItems.ProcessStopMonitorItem e)
{
    Console.WriteLine($"Process Stop: {e.ProcessID}|{e.FileName}{Environment.NewLine}");
}
```
### Registry Create Hook
To get an event hook on every Registry Create key event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnRegistryCreate += _monitor_OnRegistryCreate;
  
private void _monitor_OnRegistryCreate(object sender, lib.MonitorItems.RegistryCreateMonitorItem e)
{
    Console.WriteLine($"Registry Create: {e.ProcessID}|{e.ProcessName}|{e.KeyName}{Environment.NewLine}");
}
```
### Registry Delete Hook
To get an event hook on every Registry Delete event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnRegistryDelete += _monitor_OnRegistryDelete;
  
private void _monitor_OnRegistryDelete(object sender, lib.MonitorItems.RegistryDeleteMonitorItem e)
{
    Console.WriteLine($"Registry Delete: {e.ProcessID}|{e.ProcessName}{Environment.NewLine}");
}
```
### Registry Update Hook
To get an event hook on every Registry Update event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnRegistryUpdate += _monitor_OnRegistryUpdate;
  
private void _monitor_OnRegistryDelete(object sender, lib.MonitorItems.RegistryDeleteMonitorItem e)
{
    Console.WriteLine($"Registry Update: {e.ProcessID}|{e.ProcessName}|{e.ValueName}{Environment.NewLine}");
}
```
### TCP Connect Hook
To get an event hook on every TCP Connect event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnTcpConnect += _monitor_OnTcpConnect;
  
private void _monitor_OnTcpConnect(object sender, lib.MonitorItems.TcpConnectMonitorItem e)
{
    Console.WriteLine($"Tcp Connect: {e.ProcessID}|{e.ProcessName}|{e.DestinationIP}:{e.DestinationPort}{Environment.NewLine}");
}
```
### TCP Disconnect Hook
To get an event hook on every TCP Disconnect event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnTcpDisconnect += _monitor_OnTcpDisconnect;
  
private void _monitor_OnTcpDisconnect(object sender, lib.MonitorItems.TcpDisconnectMonitorItem e)
{
    Console.WriteLine($"Tcp Disconnect: {e.ProcessID}|{e.ProcessName}|{e.DestinationIP}:{e.DestinationPort}{Environment.NewLine}");
}
```
### TCP Send Hook
To get an event hook on every TCP Send event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnTcpSend += _monitor_OnTcpSend;
  
private void _monitor_OnTcpSend(object sender, lib.MonitorItems.TcpSendMonitorItem e)
{
    Console.WriteLine($"Tcp Send: {e.ProcessID}|{e.ProcessName}|{e.DestinationIP}:{e.DestinationPort}{Environment.NewLine}");
}
```
### TCP Receive Hook
To get an event hook on every TCP Receive event simply add a NuGet reference and the code below:
```
var monitor = new ETWMonitor();

monitor.OnTcpReceive += _monitor_OnTcpReceive;
  
private void _monitor_OnTcpReceive(object sender, lib.MonitorItems.TcpReceiveMonitorItem e)
{
    Console.WriteLine($"Tcp Receive: {e.ProcessID}|{e.ProcessName}|{e.DestinationIP}:{e.DestinationPort}{Environment.NewLine}");
}
```
### Notes
Make sure to call `monitor.stop()` upon exiting your application - code has been put in place to properly shutdown the tracing upon being disposed, however ensuring a call to stop is highly recommended.
