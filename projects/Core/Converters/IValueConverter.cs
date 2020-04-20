﻿namespace Core.Converters
{
    public interface IValueConverter
    {
        bool ReadOnly { get; set; }

        object FromInput(object sheetValue);

        void SetProperty<T>(T obj, string propertyName, object value);

        object ToOutput(object input);
        
    }
}
