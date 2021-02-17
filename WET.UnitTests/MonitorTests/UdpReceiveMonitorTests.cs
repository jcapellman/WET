using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class UdpReceiveMonitorTests : BaseMonitorTests
    {
        private readonly UdpReceiveMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UdpReceiveMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UdpReceiveMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void UdpReceiveMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.UdpReceive);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}