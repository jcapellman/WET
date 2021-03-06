using System;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.Monitors;

using WET.UnitTests.MonitorTests.Base;

namespace WET.UnitTests.MonitorTests
{
    [TestClass]
    public class FileReadMonitorTests : BaseMonitorTests
    {
        private readonly FileReadMonitor _monitor = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileReadMonitor_NullTest()
        {
            _monitor.ParseKernel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileReadMonitor_InvalidArgument()
        {
            _monitor.ParseKernel(new TestEventData());
        }

        [TestMethod]
        public void FileReadMonitor_Validity()
        {
            Assert.IsTrue(_monitor.MonitorType == MonitorTypes.FileRead);

            const KernelTraceEventParser.Keywords expected = KernelTraceEventParser.Keywords.DiskFileIO |
                                                             KernelTraceEventParser.Keywords.FileIOInit |
                                                             KernelTraceEventParser.Keywords.FileIO;

            Assert.AreEqual(expected, _monitor.KeyWordMap);
        }
    }
}