using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class ImageLoadMonitorTests : BaseMonitorTests
    {
        private readonly ImageLoadMonitor _monitor = new();

        [TestMethod]
        public void ImageLoadMonitor_NullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _monitor.ParseKernel(null!));
        }

        [TestMethod]
        public void ImageLoadMonitor_InvalidArgument()
        {
            Assert.ThrowsException<ArgumentException>(() => _monitor.ParseKernel(new TestEventData()));
        }

        [TestMethod]
        public void ImageLoadMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.ImageLoad);
            Assert.IsTrue(_monitor.KeyWordMap == KernelTraceEventParser.Keywords.ImageLoad);
        }
    }
}