using System.Linq;

using WET.lib.Enums;
using WET.lib.OutputFormatters.Base;

namespace WET.lib.OutputFormatters
{
    public class CsvOutputFormatter : BaseOutputFormatter
    {
        public override OutputFormat Formatter => OutputFormat.CSV;

        public override string Convert(object item) =>
            string.Join(",",
                GetType().GetProperties().Select(a =>
                    a.GetValue(this, null)?.ToString())
            );
    }
}