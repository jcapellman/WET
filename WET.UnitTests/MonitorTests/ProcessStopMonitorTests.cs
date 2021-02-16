using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class ProcessStopMonitorTests : BaseMonitorTests
    {
        private readonly ProcessStopMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessStopMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessStopMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void ProcessStopMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.ProcessStop);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Process);
        }
    }
}