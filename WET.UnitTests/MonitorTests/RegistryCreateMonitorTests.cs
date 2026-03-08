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
        public void RegistryCreateMonitor_NullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _monitor.ParseKernel(null!));
        }

        [TestMethod]
        public void RegistryCreateMonitor_InvalidArgument()
        {
            Assert.ThrowsException<ArgumentException>(() => _monitor.ParseKernel(new TestEventData()));
        }

        [TestMethod]
        public void RegistryCreateMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.RegistryCreate);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Registry);
        }
    }
}