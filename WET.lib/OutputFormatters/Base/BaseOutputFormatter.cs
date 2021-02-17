using System;

using WET.lib.Enums;

namespace WET.lib.OutputFormatters.Base
{
    public abstract class BaseOutputFormatter
    {
        public abstract OutputFormat Formatter { get; }

        public string ConvertData(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Convert(item);
        }

        public abstract string Convert(object item);
    }
}