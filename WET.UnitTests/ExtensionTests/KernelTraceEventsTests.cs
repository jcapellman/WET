using System.Collections.Generic;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Extensions;

namespace WET.UnitTests.ExtensionTests
{
    [TestClass]
    public class KernelTraceEventsTests
    {
        [TestMethod]
        public void KernelTraceEvents_EmptyArgument()
        {
            var empty = new List<KernelTraceEventParser.Keywords>().ToKeywords();

            Assert.IsNotNull(empty);
            Assert.IsTrue(empty == KernelTraceEventParser.Keywords.None);
        }

        [TestMethod]
        public void KernelTraceEvents_OneArgument()
        {
            var result = new List<KernelTraceEventParser.Keywords> {KernelTraceEventParser.Keywords.DiskIO}.ToKeywords();

            Assert.IsNotNull(result);
            Assert.IsTrue(result == KernelTraceEventParser.Keywords.DiskIO);
        }
    }
}