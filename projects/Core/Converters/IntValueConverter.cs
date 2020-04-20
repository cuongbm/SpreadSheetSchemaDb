﻿using Core.Models.Converters;

 namespace Core.Converters
{
    public class IntValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            int.TryParse(obj.ToString(), out int result);
            return result;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
