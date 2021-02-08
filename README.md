# WET (Windows Event Tracing) Library
The purpose of this library is to hook into various Event Tracing events on Windows with just 2 lines of code for the integrator.

## Requirements
* .NET 5
* Windows 7 SP1+

## Installation
### Package Manager
```
Install-Package WET.lib -Version 0.2.0
```

### .NET CLI
```
dotnet add package WET.lib --version 0.2.0
```

## Usage
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
Make sure to call `monitor.stop()` upon exiting your application.
