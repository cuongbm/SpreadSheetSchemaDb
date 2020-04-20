﻿namespace Core.Models.Converters
{
    public class IdValueConverter : BaseValueConverter, IValueConverter
    {
        public object Convert(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return -1;
            long.TryParse(obj.ToString(), out long result);
            return result;
        }

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            var value = (long)input;
            if (value < 0) return "";
            return input.ToString();
        }
    }
}
