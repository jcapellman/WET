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
Install-Package WET.lib -Version 0.4.0
```

### .NET CLI
```
dotnet add package WET.lib --version 0.4.0
```

## Roadmap
Hooks for the following are planned:
* File Create/Delete/Read/Update

## Usage
The library is designed for flexibility so you are not required to use all of the hooks the library provides. You can however use all of the hooks (and by default at least as of 0.4.0 - it defaults to ).

## Example
### Hooking
To get an event hook simply add a NuGet reference and the code below:
```
private readonly lib.ETWMonitor _monitor;

public MainWindow()
{
    InitializeComponent();

    _monitor = new ETWMonitor();

    _monitor.OnEvent += _monitor_OnEvent;

    Closing += MainWindow_Closing;

    _monitor.Start();
}

private void _monitor_OnEvent(object sender, lib.Containers.ETWEventContainerItem e)
{
    Application.Current.Dispatcher.Invoke(() =>
    {
        txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"{e.MonitorType}: {e.JSON}{Environment.NewLine}");
    });
}

private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
    _monitor.Stop();
}
```
### Notes
Make sure to call `monitor.stop()` upon exiting your application - code has been put in place to properly shutdown the tracing upon being disposed, however ensuring a call to stop is highly recommended.
