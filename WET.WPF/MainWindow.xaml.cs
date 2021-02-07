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
            Closing += MainWindow_Closing;

            _monitor.Start(monitorTypes: MonitorTypes.IMAGE_LOAD | MonitorTypes.PROCESS_START);
        }

        private void _monitor_OnProcessStart(object sender, lib.MonitorItems.ProcessStartMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"Process Start: {e.ParentProcessID}|{e.FileName}|{e.CommandLineArguments}{Environment.NewLine}");
            }));
        }

        private void Monitor_OnImageLoad(object sender, lib.MonitorItems.ImageLoadMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"DLL Load: {e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
            }));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _monitor.Stop();
        }
    }
}