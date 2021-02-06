using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace DLLMonitor.lib
{
    public class ImageLoadMonitor
    {
        public class ImageLoadMonitorItem
        {
            public string FileName { get; internal set; }

            public int ProcessID { get; internal set; }

            public int ThreadID { get; internal set; }
        }

        public const string DEFAULT_SESSION_NAME = nameof(DLLMonitor);

        private readonly CancellationTokenSource ctSource = new CancellationTokenSource();

        private TraceEventSession _session;

        public event EventHandler<ImageLoadMonitorItem> OnImageLoad; 

        public void Start(string sessionName = DEFAULT_SESSION_NAME)
        {
            Task.Run(() =>
            {
                _session = new TraceEventSession(sessionName);

                _session.EnableKernelProvider(KernelTraceEventParser.Keywords.ImageLoad);

                _session.Source.Kernel.ImageLoad += Kernel_ImageLoad;

                _session.Source.Process();
            }, ctSource.Token);
        }

        public void Stop()
        {
            ctSource.Cancel();

            _session.Stop(true);
        }

        private void Kernel_ImageLoad(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ImageLoadTraceData obj)
        {
            var item = new ImageLoadMonitorItem()
            {
                FileName = obj.FileName,
                ProcessID = obj.ProcessID,
                ThreadID = obj.ThreadID
            };

            OnImageLoad?.Invoke(this, item);
        }
    }
}