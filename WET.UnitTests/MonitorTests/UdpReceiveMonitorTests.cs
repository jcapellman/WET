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
        public void UdpReceiveMonitor_NullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _monitor.ParseKernel(null!));
        }

        [TestMethod]
        public void UdpReceiveMonitor_InvalidArgument()
        {
            Assert.ThrowsException<ArgumentException>(() => _monitor.ParseKernel(new TestEventData()));
        }

        [TestMethod]
        public void UdpReceiveMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.UdpReceive);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}