using System;

using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

using WET.lib.Enums;

namespace WET.lib.Monitors.Base
{
    public abstract class BaseMonitor
    {
        public abstract KernelTraceEventParser.Keywords KeyWordMap { get; }

        public abstract MonitorTypes MonitorType { get; }

        public abstract Type ExpectedEventDataType { get; }

        public bool KernelEventTracing = true;

        public object ParseKernel(TraceEvent eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            if (eventData.GetType() != ExpectedEventDataType)
            {
                throw new ArgumentException($"Argument was not of type {ExpectedEventDataType} (Got {eventData.GetType()} instead)", nameof(eventData));
            }

            return ParseKernelTraceEvent(eventData);
        }

        public object Parse(object eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            if (eventData.GetType() != ExpectedEventDataType)
            {
                throw new ArgumentException($"Argument was not of type {ExpectedEventDataType} (Got {eventData.GetType()} instead)", nameof(eventData));
            }

            return ParseTraceEvent(eventData);
        }

        protected abstract object ParseKernelTraceEvent(TraceEvent eventData);
        
        protected abstract object ParseTraceEvent(object eventData);
    }
}