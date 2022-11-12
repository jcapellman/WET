# WET (Windows Event Tracing) Library
The purpose of this library is to hook into various Event Tracing events on Windows with just 2 lines of code for the integrator.

The library currently hooks into the following events:
* DLL Load
* DLL Unload
* Event Log (Application)
* File Create
* File Delete
* File Read
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
* .NET 6
* Windows 7 SP1+

Host process due to the nature of the hooks requires an admministrator (checks as of 0.7.1 will throw an Unauthorized Exception).

## Installation
Either clone this repository or use the pre-built version that has been uploaded to NuGet.org. Below are two methods of adding the package to your existing project:

### Package Manager
```
Install-Package WET.lib -Version 0.7.1
```

### .NET CLI
```
dotnet add package WET.lib --version 0.7.1
```

## Roadmap
Hooks for the following are planned:
* macOS implementation
* Linux implementation
* More Storage Implementations

## Usage
The library is designed for flexibility so you are not required to use all of the hooks the library provides. You can however use all of the hooks (and by default at least as of 0.7.1 - it defaults to all).

## Extensibility
### Storage
Knowing not everyone will want to just hook into the event and handle it in their C# app, an IEventStorage Interface is defined and allowed as a parameter. This allows you to store all of the tracing data into a SQL database, cloud storage or offloaded to disk.

For my own curiosity and fun I implemented a few of Microsoft Azure's storage mechanisms in the WET.Azure Library (https://github.com/jcapellman/WET.Azure).

### Event Filtering
Also knowing you may have internal logic rules to further filter out events, an IEventFilter interface is defined to handle events internally before being fired.

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
