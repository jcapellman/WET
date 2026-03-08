using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class ImageUnloadMonitorTests : BaseMonitorTests
    {
        private readonly ImageUnloadMonitor _monitor = new();

        [TestMethod]
        public void ImageUnloadMonitor_NullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _monitor.ParseKernel(null!));
        }

        [TestMethod]
        public void ImageUnloadMonitor_InvalidArgument()
        {
            Assert.ThrowsException<ArgumentException>(() => _monitor.ParseKernel(new TestEventData()));
        }

        [TestMethod]
        public void ImageUnloadMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.ImageUnload);
            
            Assert.AreEqual(KernelTraceEventParser.Keywords.ImageLoad, _monitor.KeyWordMap);
        }
    }
}