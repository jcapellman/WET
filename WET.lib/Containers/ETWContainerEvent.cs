using System;

using WET.lib.Enums;

namespace WET.lib.Containers
{
    public class ETWEventContainerItem
    {
        public Guid ID { get; internal set; }

        public string Hostname { get; internal set; }

        public DateTimeOffset Timestamp { get; internal set; }

        public MonitorTypes MonitorType { get; internal set; }

        public OutputFormat Format { get; internal set; }

        public string Payload { get; internal set; }
    }
}