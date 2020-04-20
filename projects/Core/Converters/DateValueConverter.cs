using System;
using System.Globalization;
using Core.Helpers;
using Core.Models.Converters;
using Core.Settings;

namespace Core.Converters
{
    public class DateValueConverter : BaseValueConverter, IValueConverter
    {
        public string InputFormat { get; }

        public string OutputFormat { get; }

        public DateValueConverter(): this(DateTimeFormat.StorageInputDateFormat, DateTimeFormat.StorageOutputDateFormat) { }

        public DateValueConverter(string inputFormat, string outputFormat = null)
        {
            Guards.NotNull(inputFormat, "format");
            this.InputFormat = inputFormat;
            this.OutputFormat = outputFormat == null ? inputFormat : outputFormat;
        }

        public object FromInput(object obj)
        {
            if (obj == null) return DateTime.Now;
            if (obj is long || obj is double) {
                return DateTime.FromOADate(double.Parse(obj.ToString())).Date;
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
            var sArr = val.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            DateTime dt = DateTime.ParseExact(sArr[0], InputFormat, provider, DateTimeStyles.AllowWhiteSpaces);
            return dt.Date;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            if (input is DateTime) 
            {
                return ((DateTime)input).ToString(OutputFormat);
            }
            return input?.ToString() ?? "";
        }
    }
}
