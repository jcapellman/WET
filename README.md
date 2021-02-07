# WET (Windows Event Tracing) Library
The purpose of this library is to hook into various Event Tracing events on Windows with just 2 lines of code for the integrator.

## Requirements
* .NET 5
* Windows 7 SP1+

## Usage
### DLL Load Hook
To get an event hook on every DLL Load simply add a NuGet reference and the code below:

   var monitor = new ImageLoadMonitor();

   monitor.OnImageLoad += Monitor_OnImageLoad;
  
   private void Monitor_OnImageLoad(object sender, ImageLoadMonitor.ImageLoadMonitorItem e)
   {
       Console.WriteLine($"{e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
   }
   
Make sure to call `monitor.stop()` upon exiting your application.
