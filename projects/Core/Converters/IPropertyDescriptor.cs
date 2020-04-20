﻿namespace Core.Models.Converters
{
    public interface IPropertyDescriptor
    {
        bool PrimaryKey { get; }

        string Name { get; set; }

        IValueConverter Converter { get; set; }
    }
}
