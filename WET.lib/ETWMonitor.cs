using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using Microsoft.Extensions.Logging;

using WET.lib.Containers;
using WET.lib.Enums;
using WET.lib.Extensions;
using WET.lib.Interfaces;
using WET.lib.Monitors.Base;
using WET.lib.OutputFormatters.Base;

namespace WET.lib
{
    public class ETWMonitor : IDisposable
    {
        public const string DefaultSessionName = nameof(ETWMonitor);

        private readonly CancellationTokenSource _ctSource = new();

        private TraceEventSession _session;

        public event EventHandler<ETWEventContainerItem> OnEvent; 

        private readonly List<BaseMonitor> _monitors;

        private readonly List<BaseOutputFormatter> _outputFormatters;

        private BaseOutputFormatter _selectedOutputFormatter;

        private IEventFilter _eventFilter;

        private ILogger _logger;

        private IEventStorage _eventStorage;

        private DateTime _lastPush = DateTime.Now;

        private TimeSpan? _interval;

        private int? _threshold;

        private long? _hostID;

        private ConcurrentBag<ETWEventContainerItem> _throttledItems = new();

        private static bool IsRunningAsAdmin()
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var wm = new WindowsPrincipal(WindowsIdentity.GetCurrent());
#pragma warning restore CA1416 // Validate platform compatibility

#pragma warning disable CA1416 // Validate platform compatibility
            return wm.IsInRole(WindowsBuiltInRole.Administrator);
#pragma warning restore CA1416 // Validate platform compatibility
        }

        public ETWMonitor()
        {
            _monitors = GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseMonitor))
                .Select(a => (BaseMonitor) Activator.CreateInstance(a)).ToList();

            _outputFormatters = GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseOutputFormatter))
                .Select(a => (BaseOutputFormatter) Activator.CreateInstance(a)).ToList();

            if (!IsRunningAsAdmin())
            {
                throw new UnauthorizedAccessException("Host process is not running as administrator");
            }
        }

        private void InitializeMonitor(string sessionName, MonitorTypes monitorTypes, OutputFormat outputFormat, 
            IEventFilter eventFilter, IEventStorage eventStorage, TimeSpan? interval = null, int? threshold = null, long? hostId = null, ILogger logger = null)
        {
            _logger = logger;

            _hostID = hostId;

            _interval = interval;

            _threshold = threshold;

            _eventFilter = eventFilter;

            _eventStorage = eventStorage;

            _selectedOutputFormatter = _outputFormatters.FirstOrDefault(a => a.Formatter == outputFormat);

            if (string.IsNullOrEmpty(sessionName))
            {
                throw new ArgumentNullException(nameof(sessionName));
            }

            _session = new TraceEventSession(sessionName);

            LogDebug($"Starting ETW Session ({sessionName})");

            var enabledMonitors = monitorTypes == MonitorTypes.All ? 
                _monitors : _monitors.Where(a => monitorTypes.HasFlag(a.MonitorType)).ToList();

            _session.EnableKernelProvider(enabledMonitors.Where(a => a.KernelEventTracing).Select(a => a.KeyWordMap).ToKeywords());
            
            foreach (var monitor in enabledMonitors)
            {
                switch (monitor.MonitorType)
                {
                    case MonitorTypes.FileRead:
                        _session.Source.Kernel.DiskIORead += Kernel_DiskIORead;
                        break;
                    case MonitorTypes.ImageLoad:
                        _session.Source.Kernel.ImageLoad += Kernel_ImageLoad;
                        break;
                    case MonitorTypes.ImageUnload:
                        _session.Source.Kernel.ImageUnload += Kernel_ImageUnload;
                        break;
                    case MonitorTypes.ProcessStart:
                        _session.Source.Kernel.ProcessStart += Kernel_ProcessStart;
                        break;
                    case MonitorTypes.ProcessStop:
                        _session.Source.Kernel.ProcessStop += Kernel_ProcessStop;
                        break;
                    case MonitorTypes.RegistryCreate:
                        _session.Source.Kernel.RegistryCreate += Kernel_RegistryCreate;
                        break;
                    case MonitorTypes.RegistryOpen:
                        _session.Source.Kernel.RegistryOpen += Kernel_RegistryOpen;
                        break;
                    case MonitorTypes.RegistryDelete:
                        _session.Source.Kernel.RegistryDelete += Kernel_RegistryDelete;
                        break;
                    case MonitorTypes.RegistryUpdate:
                        _session.Source.Kernel.RegistrySetValue += Kernel_RegistrySetValue;
                        break;
                    case MonitorTypes.TcpConnect:
                        _session.Source.Kernel.TcpIpConnect += Kernel_TcpIpConnect;
                        break;
                    case MonitorTypes.TcpDisconnect:
                        _session.Source.Kernel.TcpIpDisconnect += Kernel_TcpIpDisconnect;
                        break;
                    case MonitorTypes.TcpReceive:
                        _session.Source.Kernel.TcpIpRecv += Kernel_TcpIpRecv;
                        break;
                    case MonitorTypes.TcpSend:
                        _session.Source.Kernel.TcpIpSend += Kernel_TcpIpSend;
                        break;
                    case MonitorTypes.UdpSend:
                        _session.Source.Kernel.UdpIpSend += Kernel_UdpIpSend;
                        break;
                    case MonitorTypes.UdpReceive:
                        _session.Source.Kernel.UdpIpRecv += Kernel_UdpIpRecv;
                        break;
                    case MonitorTypes.EventLogs:
#pragma warning disable CA1416 // Validate platform compatibility
                        var eventLog = new EventLog("Application", ".");

                        eventLog.EntryWritten += EventLog_EntryWritten;
                        eventLog.EnableRaisingEvents = true;
#pragma warning restore CA1416 // Validate platform compatibility
                        break;
                }
            }
           
            _session.Source.Process();
        }

        public void Start(string sessionName = DefaultSessionName, MonitorTypes monitorTypes = MonitorTypes.All, 
            OutputFormat outputFormat = OutputFormat.JSON, IEventFilter eventFilter = null, IEventStorage eventStorage = null,
            TimeSpan? interval = null, int? threshold = null, long? hostId = null, ILogger logger = null)
        {
            Task.Run(() =>
            {
                InitializeMonitor(sessionName, monitorTypes, outputFormat, eventFilter, eventStorage, interval, threshold, hostId, logger);
            }, _ctSource.Token);
        }

        private void LogDebug(string message)
        {
            LogMessage(LogLevel.Debug, message);
        }

        private void LogError(string message)
        {
            LogMessage(LogLevel.Error, message);
        }

        private void LogMessage(LogLevel level, string message)
        {
            _logger.Log(level, message);
        }

        private void ParseKernelEvent(MonitorTypes monitorType, TraceEvent item)
        {
            var monitor = _monitors.FirstOrDefault(a => a.MonitorType == monitorType);

            if (monitor == null)
            {
                throw new Exception($"{monitorType} could not be mapped");
            }

            var data = monitor.ParseKernel(item);

            if (data == null)
            {
                LogDebug($"{monitorType} event data null was null - ignoring");

                return;
            }

            Parse(monitorType, data);
        }

        private async void Parse(MonitorTypes monitorType, object data)
        {
            if (_eventFilter != null && _eventFilter.IsFilteredOut(monitorType, data))
            {
                // Filtered out based on the implementation - do not fire the event

                return;
            }

            var containerItem = new ETWEventContainerItem
            {
                id = Guid.NewGuid(),
                hostid = _hostID ?? 0,
                MonitorType = monitorType,
                Format = _selectedOutputFormatter.Formatter,
                Payload = _selectedOutputFormatter.ConvertData(data),
                Timestamp = DateTimeOffset.Now,
                hostname = Environment.MachineName
            };

            // If either is set assume batching
            if (_interval.HasValue || _threshold.HasValue)
            {
                if ((_interval.HasValue && DateTime.Now.Subtract(_lastPush) > _interval.Value) ||
                    (_threshold.HasValue && _throttledItems.Count > _threshold.Value))
                {
                    var result = await _eventStorage.WriteBatchEventAsync(_throttledItems.ToList());

                    if (!result)
                    {
                        LogError($"Could not write batch of throttled items");

                        return;
                    }

                    _throttledItems.Clear();
                    _lastPush = DateTime.Now;

                    return;
                }
                
                _throttledItems.Add(containerItem);
                
                return;
            }

            if (_eventStorage != null)
            {
                var result = await _eventStorage.WriteEventAsync(containerItem);

                if (!result)
                {
                    LogError($"Could not write {containerItem} to Storage");
                }

                return;
            }

            OnEvent?.Invoke(this, containerItem);
        }

        private void ParseEvent(MonitorTypes monitorType, object item)
        {
            var data = _monitors.FirstOrDefault(a => a.MonitorType == monitorType)?.Parse(item);

            if (data == null)
            {
                throw new Exception($"{monitorType} could not be mapped");
            }
            
            Parse(monitorType, data);
        }

#pragma warning disable CA1416 // Validate platform compatibility
        private void EventLog_EntryWritten(object sender, EntryWrittenEventArgs obj) =>
            ParseEvent(MonitorTypes.EventLogs, obj.Entry);
#pragma warning restore CA1416 // Validate platform compatibility

        private void Kernel_UdpIpRecv(Microsoft.Diagnostics.Tracing.Parsers.Kernel.UdpIpTraceData obj) =>
            ParseKernelEvent(MonitorTypes.UdpReceive, obj);

        private void Kernel_UdpIpSend(Microsoft.Diagnostics.Tracing.Parsers.Kernel.UdpIpTraceData obj) =>
            ParseKernelEvent(MonitorTypes.UdpSend, obj);

        private void Kernel_TcpIpConnect(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpConnectTraceData obj) =>
            ParseKernelEvent(MonitorTypes.TcpConnect, obj);

        private void Kernel_TcpIpDisconnect(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpTraceData obj) =>
            ParseKernelEvent(MonitorTypes.TcpDisconnect, obj);

        private void Kernel_TcpIpRecv(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpTraceData obj) =>
            ParseKernelEvent(MonitorTypes.TcpReceive, obj);

        private void Kernel_TcpIpSend(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpSendTraceData obj) =>
            ParseKernelEvent(MonitorTypes.TcpSend, obj);

        private void Kernel_RegistryCreate(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            ParseKernelEvent(MonitorTypes.RegistryCreate, obj);

        private void Kernel_RegistryDelete(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            ParseKernelEvent(MonitorTypes.RegistryDelete, obj);

        private void Kernel_RegistryOpen(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            ParseKernelEvent(MonitorTypes.RegistryOpen, obj);

        private void Kernel_RegistrySetValue(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            ParseKernelEvent(MonitorTypes.RegistryUpdate, obj);

        private void Kernel_DiskIORead(Microsoft.Diagnostics.Tracing.Parsers.Kernel.DiskIOTraceData obj) =>
            ParseKernelEvent(MonitorTypes.FileRead, obj);

        private void Kernel_ProcessStop(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData obj) =>
            ParseKernelEvent(MonitorTypes.ProcessStop, obj);

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData obj) =>
            ParseKernelEvent(MonitorTypes.ProcessStart, obj);

        private void Kernel_ImageLoad(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj) =>
            ParseKernelEvent(MonitorTypes.ImageLoad, obj);

        private void Kernel_ImageUnload(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj) =>
            ParseKernelEvent(MonitorTypes.ImageUnload, obj);

        public void Stop()
        {
            _ctSource?.Cancel();

            _session?.Stop(true);

            _eventStorage?.Shutdown();
        }
        
        public void Dispose()
        {
            _ctSource.Cancel();

            _session.Stop(true);

            _eventStorage?.Shutdown();

            GC.SuppressFinalize(this);
        }
    }
}