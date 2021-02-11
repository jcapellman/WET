using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

using WET.lib.Enums;
using WET.lib.Extensions;
using WET.lib.MonitorItems;
using WET.lib.Monitors.Base;

namespace WET.lib
{
    public class ETWMonitor : IDisposable
    {
        public const string DefaultSessionName = nameof(ETWMonitor);

        private readonly CancellationTokenSource _ctSource = new();

        private TraceEventSession _session;

        public event EventHandler<FileReadMonitorItem> OnFileRead; 

        public event EventHandler<ImageLoadMonitorItem> OnImageLoad;

        public event EventHandler<ImageUnloadMonitorItem> OnImageUnload;

        public event EventHandler<ProcessStartMonitorItem> OnProcessStart;

        public event EventHandler<ProcessStopMonitorItem> OnProcessStop;

        public event EventHandler<RegistryCreateMonitorItem> OnRegistryCreate;

        public event EventHandler<RegistryDeleteMonitorItem> OnRegistryDelete;

        public event EventHandler<RegistryUpdateMonitorItem> OnRegistryUpdate;

        public event EventHandler<TcpConnectMonitorItem> OnTcpConnect;

        public event EventHandler<TcpDisconnectMonitorItem> OnTcpDisconnect;

        public event EventHandler<TcpReceiveMonitorItem> OnTcpReceive;

        public event EventHandler<TcpSendMonitorItem> OnTcpSend;

        public event EventHandler<UdpSendMonitorItem> OnUdpSend;

        private readonly List<BaseMonitor> _monitors;
        
        private object ParseData(TraceEvent eventData, MonitorTypes monitorType) =>
            _monitors.FirstOrDefault(a => a.MonitorType == monitorType)?.ParseTraceEvent(eventData);

        public ETWMonitor()
        {
            _monitors = GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseMonitor))
                .Select(a => (BaseMonitor) Activator.CreateInstance(a)).ToList();
        }

        private void InitializeMonitor(string sessionName, MonitorTypes monitorTypes)
        {
            _session = new TraceEventSession(sessionName);

            var enabledMonitors = _monitors.Where(a => monitorTypes.HasFlag(a.MonitorType)).ToList();

            _session.EnableKernelProvider(enabledMonitors.Select(a => a.KeyWordMap).ToKeywords());

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
                }
            }

            _session.Source.Process();
        }
        
        public void Start(string sessionName = DefaultSessionName, MonitorTypes monitorTypes = MonitorTypes.ImageLoad | MonitorTypes.ProcessStart)
        {
            Task.Run(() =>
            {
                InitializeMonitor(sessionName, monitorTypes);
            }, _ctSource.Token);
        }

        private void Kernel_UdpIpSend(Microsoft.Diagnostics.Tracing.Parsers.Kernel.UdpIpTraceData obj) =>
            OnUdpSend?.Invoke(this, (UdpSendMonitorItem)ParseData(obj, MonitorTypes.UdpSend));

        private void Kernel_TcpIpConnect(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpConnectTraceData obj) =>
            OnTcpConnect?.Invoke(this, (TcpConnectMonitorItem)ParseData(obj, MonitorTypes.TcpConnect));

        private void Kernel_TcpIpDisconnect(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpTraceData obj) =>
            OnTcpDisconnect?.Invoke(this, (TcpDisconnectMonitorItem)ParseData(obj, MonitorTypes.TcpDisconnect));

        private void Kernel_TcpIpRecv(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpTraceData obj) =>
            OnTcpReceive?.Invoke(this, (TcpReceiveMonitorItem)ParseData(obj, MonitorTypes.TcpReceive));
        
        private void Kernel_TcpIpSend(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpSendTraceData obj) =>
            OnTcpSend?.Invoke(this, (TcpSendMonitorItem)ParseData(obj, MonitorTypes.TcpSend));

        private void Kernel_RegistryCreate(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            OnRegistryCreate?.Invoke(this, (RegistryCreateMonitorItem)ParseData(obj, MonitorTypes.RegistryCreate));
        
        private void Kernel_RegistryDelete(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            OnRegistryDelete?.Invoke(this, (RegistryDeleteMonitorItem)ParseData(obj, MonitorTypes.RegistryDelete));
        
        private void Kernel_RegistrySetValue(Microsoft.Diagnostics.Tracing.Parsers.Kernel.RegistryTraceData obj) =>
            OnRegistryUpdate?.Invoke(this, (RegistryUpdateMonitorItem)ParseData(obj, MonitorTypes.RegistryUpdate));

        private void Kernel_DiskIORead(Microsoft.Diagnostics.Tracing.Parsers.Kernel.DiskIOTraceData obj) =>
            OnFileRead?.Invoke(this, (FileReadMonitorItem)ParseData(obj, MonitorTypes.FileRead));
        
        private void Kernel_ProcessStop(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData obj) =>
            OnProcessStop?.Invoke(this, (ProcessStopMonitorItem)ParseData(obj, MonitorTypes.ProcessStop));

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData obj) => 
            OnProcessStart?.Invoke(this, (ProcessStartMonitorItem)ParseData(obj, MonitorTypes.ProcessStart));

        private void Kernel_ImageLoad(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj) =>
            OnImageLoad?.Invoke(this, (ImageLoadMonitorItem)ParseData(obj, MonitorTypes.ImageLoad));

        private void Kernel_ImageUnload(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj) =>
            OnImageUnload?.Invoke(this, (ImageUnloadMonitorItem)ParseData(obj, MonitorTypes.ImageUnload));

        public void Stop()
        {
            _ctSource.Cancel();

            _session.Stop(true);
        }
        
        public void Dispose()
        {
            _ctSource.Cancel();

            _session.Stop(true);

            GC.SuppressFinalize(this);
        }
    }
}