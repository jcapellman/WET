using System;
using System.Windows;

using WET.lib;

namespace WET.WPF
{
    public partial class MainWindow : Window
    {
        private readonly lib.ImageLoadMonitor _monitor;

        public MainWindow()
        {
            InitializeComponent();

            _monitor = new ImageLoadMonitor();

            _monitor.OnImageLoad += Monitor_OnImageLoad;

            Closing += MainWindow_Closing;

            _monitor.Start();
        }

        private void Monitor_OnImageLoad(object sender, ImageLoadMonitor.ImageLoadMonitorItem e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"{e.ProcessID}|{e.ThreadID}|{e.FileName}{Environment.NewLine}");
            }));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _monitor.Stop();
        }
    }
}