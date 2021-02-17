using System.Text.Json;

using WET.lib.Enums;
using WET.lib.OutputFormatters.Base;

namespace WET.lib.OutputFormatters
{
    public class JsonOutputFormatter : BaseOutputFormatter
    {
        public override OutputFormat Formatter => OutputFormat.JSON;

        public override string Convert(object item) => 
            JsonSerializer.Serialize(item);
    }
}