﻿using Core.Models.Converters;

 namespace Core.Converters
{
    public class IdValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return -1;
            long.TryParse(obj.ToString(), out long result);
            return result;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            var value = (long)input;
            if (value < 0) return "";
            return input.ToString();
        }
    }
}
