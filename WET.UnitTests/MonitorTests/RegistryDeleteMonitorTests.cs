using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class RegistryDeleteMonitorTests : BaseMonitorTests
    {
        private readonly RegistryDeleteMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistryDeleteMonitor_NullTest()
        {
            _monitor.ParseKernel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistryDeleteMonitor_InvalidArgument()
        {
            _monitor.ParseKernel(new TestEventData());
        }

        [TestMethod]
        public void RegistryDeleteMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.RegistryDelete);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.Registry);
        }
    }
}