﻿using Core.Helpers;
using System;
using System.Globalization;

namespace Core.Models.Converters
{
    public class EnumValueConverter<T> : BaseValueConverter, IValueConverter where T : struct, IComparable, IFormattable, IConvertible
    {
        public EnumValueConverter()
        {
        }

        public object Convert(object obj)
        {
            if (obj is Enum) return obj;
            var intVal = (int)new IntValueConverter().Convert(obj);
            return Enum.ToObject(typeof(T), intVal);
        }

        public object Output(object input)
        {
            var parsed = (T)input;
            var result = parsed.ToInt32(CultureInfo.InvariantCulture);

            return result == 0 ? "" : result.ToString();
        }

        public override void SetProperty<TObject>(TObject obj, string propertyName, object value)
        {
            var property = Reflector.GetProperty(obj, propertyName);
            if (property == null)
            {
                throw new InvalidOperationException($"Property {propertyName} not found");
            }

            if (value == null || string.IsNullOrEmpty(value.ToString())) return;

            var inputValue = (int)value;
            var enumValue = Enum.ToObject(property.PropertyType, inputValue);

            Reflector.SetProperty(obj, propertyName, enumValue);
        }
    }
}
