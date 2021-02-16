using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WET.UnitTests.MonitorTests.Base
{
    [TestClass]
    public class BaseMonitorTests
    {
        public class TestEventData : TraceEvent
        {
            public TestEventData() : this(Int32.MinValue, 1, String.Empty, Guid.Empty, 1,string.Empty,Guid.Empty, String.Empty) { }
            
            public TestEventData(int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName) : base(eventID, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
            {
            }

            public override object PayloadValue(int index)
            {
                return string.Empty;
            }

            public override string[] PayloadNames => new[] {"Test"};

            protected override Delegate Target
            {
                get => Delegate.Combine(null, null);

                set
                {

                }
            }
        }
    }
}