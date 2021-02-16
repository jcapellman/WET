using System;
using System.Collections.Generic;

using WET.lib.Enums;
using WET.lib.OutputFormatters.Base;

namespace WET.lib.OutputFormatters
{
    public class CsvOutputFormatter : BaseOutputFormatter
    {
        public override OutputFormat Formatter => OutputFormat.CSV;

        public override string ConvertData(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var properties = item.GetType().GetProperties();

            var values = new List<string>();

            foreach (var property in properties)
            {
                var val = property.GetValue(item);

                if (val == null)
                {
                    values.Add(@"""");

                    continue;
                }

                values.Add(val.ToString());
            }

            return string.Join(',', values);
        }
    }
}