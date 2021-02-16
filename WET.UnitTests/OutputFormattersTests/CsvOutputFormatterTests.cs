using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.OutputFormatters;

namespace WET.UnitTests.OutputFormattersTests
{
    [TestClass]
    public class CsvOutputFormatterTests
    {
        private readonly CsvOutputFormatter _csvFormatter = new();

        [TestMethod]
        public void CsvOutput_FormatterValidity()
        {
            Assert.IsTrue(_csvFormatter.Formatter == OutputFormat.CSV);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CsvOutput_NullTest()
        {
            _csvFormatter.ConvertData(null);
        }

        [TestMethod]
        public void CsvOutput_ObjectTest()
        {
            var result = _csvFormatter.ConvertData(new ProcessStartMonitorItem());

            Assert.IsNotNull(result);
        }
    }
}