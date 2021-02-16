using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib.Enums;
using WET.lib.MonitorItems;
using WET.lib.OutputFormatters;

namespace WET.UnitTests.OutputFormattersTests
{
    [TestClass]
    public class JsonOutputFormatterTests
    {
        private readonly JsonOutputFormatter _jsonParser = new();

        [TestMethod]
        public void JsonOutput_FormatterValidity()
        {
            Assert.IsTrue(_jsonParser.Formatter == OutputFormat.JSON);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JsonOutput_NullTest()
        {
            _jsonParser.ConvertData(null);
        }

        [TestMethod]
        public void JsonOutput_ObjectTest()
        {
            var result = _jsonParser.ConvertData(new ProcessStartMonitorItem());

            Assert.IsNotNull(result);
        }
    }
}