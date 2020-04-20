﻿ namespace Core.Converters
{
    public class EmptyValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            return null;
        }

        public object ToOutput(object input)
        {
            return "";
        }
    }
}
