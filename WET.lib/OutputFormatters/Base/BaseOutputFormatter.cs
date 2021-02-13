using WET.lib.Enums;

namespace WET.lib.OutputFormatters.Base
{
    public abstract class BaseOutputFormatter
    {
        public abstract OutputFormat Formatter { get; }

        public abstract string ConvertData(object item);
    }
}