﻿namespace Core.Models.Converters
{
    public class IntValueConverter : BaseValueConverter, IValueConverter
    {
        public object Convert(object obj)
        {
            int.TryParse(obj.ToString(), out int result);
            return result;
        }

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
