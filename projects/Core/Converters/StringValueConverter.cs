﻿namespace Core.Models.Converters
{
    public class StringValueConverter : BaseValueConverter, IValueConverter
    {
        public object Convert(object obj)
        {
            if (obj == null) return "";
            return obj.ToString();
        }

        public object Output(object obj)
        {
            if (obj == null || ReadOnly) return "";
            return obj.ToString();
        }
    }
}
