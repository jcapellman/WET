using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class UdpSendMonitorTests : BaseMonitorTests
    {
        private readonly UdpSendMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UdpSendMonitor_NullTest()
        {
            _monitor.ParseKernel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UdpSendMonitor_InvalidArgument()
        {
            _monitor.ParseKernel(new TestEventData());
        }

        [TestMethod]
        public void UdpSendMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.UdpSend);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}