using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class ProcessStartMonitorTests : BaseMonitorTests
    {
        private readonly ProcessStartMonitor _monitor = new();

        [TestMethod]
        public void ProcessStartMonitor_NullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _monitor.ParseKernel(null!));
        }

        [TestMethod]
        public void ProcessStartMonitor_InvalidArgument()
        {
            Assert.ThrowsException<ArgumentException>(() => _monitor.ParseKernel(new TestEventData()));
        }

        [TestMethod]
        public void ProcessStartMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.ProcessStart);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Process);
        }
    }
}