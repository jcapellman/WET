using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class TcpConnectMonitorTests : BaseMonitorTests
    {
        private readonly TcpConnectMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TcpConnectMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TcpConnectMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void TcpConnectMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.TcpConnect);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}