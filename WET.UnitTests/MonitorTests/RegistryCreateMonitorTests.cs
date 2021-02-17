using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class RegistryCreateMonitorTests : BaseMonitorTests
    {
        private readonly RegistryCreateMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistryCreateMonitor_NullTest()
        {
            _monitor.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistryCreateMonitor_InvalidArgument()
        {
            _monitor.Parse(new TestEventData());
        }

        [TestMethod]
        public void RegistryCreateMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.RegistryCreate);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Registry);
        }
    }
}