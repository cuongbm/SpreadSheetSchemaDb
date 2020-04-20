﻿ namespace Core.Converters
{
    public class StringValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            if (obj == null) return "";
            return obj.ToString();
        }

        public object ToOutput(object obj)
        {
            if (obj == null || ReadOnly) return "";
            return obj.ToString();
        }
    }
}
