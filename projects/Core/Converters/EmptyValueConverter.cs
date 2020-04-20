﻿namespace Core.Models.Converters
{
    public class EmptyValueConverter : BaseValueConverter, IValueConverter
    {
        public object Convert(object obj)
        {
            return null;
        }

        public object Output(object input)
        {
            return "";
        }
    }
}
