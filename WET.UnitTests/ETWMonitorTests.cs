using System;
using System.Linq;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WET.lib;
using WET.lib.Enums;

namespace WET.UnitTests
{
    [TestClass]
    public class ETWMonitorTests
    {
        [TestMethod]
        public void ETWMonitor_Null()
        {
            var etw = new ETWMonitor();
            
            etw.Start(null, MonitorTypes.All, OutputFormat.CSV);
            
            etw.Stop();
        }

        [TestMethod]
        public void ETWMonitor_AllMonitors()
        {
            using var etw = new ETWMonitor();

            etw.OnEvent += Etw_OnEvent;

            etw.Start("UnitTesto", MonitorTypes.All, OutputFormat.CSV);

            Thread.Sleep(60000);

            etw.Stop();
        }

        private void Etw_OnEvent(object sender, lib.Containers.ETWEventContainerItem e)
        {
            Assert.IsNotNull(e);

            Assert.IsNotNull(e.MonitorType);
            Assert.IsNotNull(e.Timestamp);
            Assert.IsNotNull(e.Format);
            Assert.IsNotNull(e.id);
            Assert.IsNotNull(e.Payload);

            Assert.AreEqual(OutputFormat.CSV, e.Format);
        }
    }
}