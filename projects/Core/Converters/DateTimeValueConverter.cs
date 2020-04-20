using System;
using System.Globalization;
using Core.Helpers;
using Core.Models.Converters;
using Core.Settings;

namespace Core.Converters
{
    public class DateTimeValueConverter : BaseValueConverter, IValueConverter
    {
        public string InputFormat { get; }

        public string OutputFormat { get; }

        public DateTimeValueConverter(): this(DateTimeFormat.DefaultInputDateTimeFormat, DateTimeFormat.DefaultOutputDateTimeFormat) { }

        public DateTimeValueConverter(string inputFormat, string outputFormat = null)
        {
            Guards.NotNull(inputFormat, "format");
            this.InputFormat = inputFormat;
            this.OutputFormat = outputFormat ?? inputFormat;
        }

        public object FromInput(object obj)
        {
            if (obj == null) return null;
            if (obj is long || obj is double) {
                return DateTime.FromOADate(double.Parse(obj.ToString()));
            }
            if (obj is DateTime) 
            {
                return obj;
            }
            string val = obj.ToString();
            if (string.IsNullOrEmpty(val)) 
            {
                return null;
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dt = DateTime.ParseExact(val, InputFormat, provider, DateTimeStyles.AllowWhiteSpaces);
            return dt.ToUniversalTime();
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            if (input is DateTime time) 
            {
                return time.ToString(OutputFormat);
            }
            return input.ToString();
        }
    }
}
