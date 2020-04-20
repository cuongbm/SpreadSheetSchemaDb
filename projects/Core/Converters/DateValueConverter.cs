﻿using Core.Helpers;
using Core.Settings;
using System;
using System.Globalization;

namespace Core.Models.Converters
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

        public object Convert(object obj)
        {
            if (obj == null) return DateTime.Now;
            if (obj.GetType() == typeof(long) || obj.GetType() == typeof(double)) {
                return DateTime.FromOADate(double.Parse(obj.ToString()));
            }
            if (obj.GetType() == typeof(DateTime)) 
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

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            if (input != null && input.GetType() == typeof(DateTime)) 
            {
                return ((DateTime)input).ToString(OutputFormat);
            }
            return input?.ToString() ?? "";
        }
    }
}
