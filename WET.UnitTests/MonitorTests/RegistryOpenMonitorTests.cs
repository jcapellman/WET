using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class RegistryOpenMonitorTests : BaseMonitorTests
    {
        private readonly RegistryOpenMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistryOpenMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistryOpenMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void RegistryOpenMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.RegistryOpen);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Registry);
        }
    }
}