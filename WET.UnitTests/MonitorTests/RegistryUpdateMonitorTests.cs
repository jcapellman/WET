using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class RegistryUpdateMonitorTests : BaseMonitorTests
    {
        private readonly RegistryUpdateMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistryUpdateMonitor_NullTest()
        {
            _monitor.ParseKernel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistryUpdateMonitor_InvalidArgument()
        {
            _monitor.ParseKernel(new TestEventData());
        }

        [TestMethod]
        public void RegistryUpdateMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.RegistryUpdate);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Registry);
        }
    }
}