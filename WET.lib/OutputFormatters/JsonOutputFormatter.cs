using System.Text.Json;
using System.Text.Json.Serialization;

using WET.lib.Enums;
using WET.lib.OutputFormatters.Base;

namespace WET.lib.OutputFormatters
{
    public class JsonOutputFormatter : BaseOutputFormatter
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public override OutputFormat Formatter => OutputFormat.JSON;

        public override string Convert(object item) => 
            JsonSerializer.Serialize(item, _options);
    }
}