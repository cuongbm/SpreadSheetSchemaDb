﻿using Core.Models.Converters;

 namespace Core.Converters
{
    public class NumberValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            if (obj == null || obj.ToString() == "") return 0L;
            return long.Parse(obj.ToString());
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
