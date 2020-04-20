﻿namespace Core.Models.Converters
{
    public interface IValueConverter
    {
        bool ReadOnly { get; set; }

        object Convert(object sheetValue);

        void SetProperty<T>(T obj, string propertyName, object value);

        object Output(object input);
        
    }
}
