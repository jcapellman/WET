using System;
using System.Windows;

using WET.lib;
using WET.lib.Enums;

namespace WET.WPF
{
    public partial class MainWindow : Window
    {
        private readonly lib.ETWMonitor _monitor;

        public MainWindow()
        {
            InitializeComponent();

            _monitor = new ETWMonitor();

            _monitor.OnImageLoad += Monitor_OnImageLoad;
            _monitor.OnProcessStart += _monitor_OnProcessStart;
            _monitor.OnProcessStop += _monitor_OnProcessStop;
            _monitor.OnFileRead += _monitor_OnFileRead;
            _monitor.OnRegistryUpdate += _monitor_OnRegistryUpdate;
            _monitor.OnRegistryCreate += _monitor_OnRegistryCreate;

            Closing += MainWindow_Closing;

            _monitor.Start(monitorTypes: MonitorTypes.ProcessStart | MonitorTypes.RegistryUpdate | MonitorTypes.RegistryCreate);
        }

        private void _monitor_OnRegistryCreate(object sender, lib.MonitorItems.RegistryCreateMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"Registry Create: {e.ProcessID}|{e.ProcessName}|{e.KeyName}{Environment.NewLine}");
            });
        }

        private void _monitor_OnRegistryUpdate(object sender, lib.MonitorItems.RegistryUpdateMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"Registry Update: {e.ProcessID}|{e.ProcessName}|{e.ValueName}{Environment.NewLine}");
            });
        }

        private void _monitor_OnFileRead(object sender, lib.MonitorItems.FileReadMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"File Read: {e.ProcessID}|{e.FileName}{Environment.NewLine}");
            });
        }

        private void _monitor_OnProcessStop(object sender, lib.MonitorItems.ProcessStopMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"Process Stop: {e.ProcessID}|{e.FileName}{Environment.NewLine}");
            });
        }

        private void _monitor_OnProcessStart(object sender, lib.MonitorItems.ProcessStartMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"Process Start: {e.ParentProcessID}|{e.FileName}|{e.CommandLineArguments}{Environment.NewLine}");
            });
        }

        private void Monitor_OnImageLoad(object sender, lib.MonitorItems.ImageLoadMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"DLL Load: {e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
            });
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _monitor.Stop();
        }
    }
}