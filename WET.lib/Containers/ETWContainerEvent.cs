using System;

using WET.lib.Enums;

namespace WET.lib.Containers
{
    public class ETWEventContainerItem
    {
        public Guid id { get; internal set; }
        
        public string hostname { get; internal set; }

        public DateTimeOffset Timestamp { get; internal set; }

        public MonitorTypes MonitorType { get; internal set; }

        public OutputFormat Format { get; internal set; }

        public string Payload { get; internal set; }

        public long hostid { get; internal set; }

        public override string ToString() => System.Text.Json.JsonSerializer.Serialize(this);
    }
}