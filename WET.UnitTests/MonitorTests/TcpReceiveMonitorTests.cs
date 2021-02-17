using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class TcpReceiveMonitorTests : BaseMonitorTests
    {
        private readonly TcpReceiveMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TcpReceiveMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TcpReceiveMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void TcpReceiveMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.TcpReceive);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}