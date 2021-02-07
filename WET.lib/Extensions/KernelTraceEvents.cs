using System.Collections.Generic;
using System.Linq;

using Microsoft.Diagnostics.Tracing.Parsers;

namespace WET.lib.Extensions
{
    public static class KernelTraceEventsExtensions
    {
        public static KernelTraceEventParser.Keywords ToKeywords(this IEnumerable<KernelTraceEventParser.Keywords> keywords) => 
            keywords.Aggregate(KernelTraceEventParser.Keywords.None, (current, keyword) => current | keyword);
    }
}