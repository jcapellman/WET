using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class TcpDisconnectMonitorTests : BaseMonitorTests
    {
        private readonly TcpDisconnectMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TcpDisconnectMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TcpDisconnectMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void TcpDisconnectMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.TcpDisconnect);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.NetworkTCPIP);
        }
    }
}