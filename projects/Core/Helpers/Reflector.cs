﻿using Core.Models.Converters;
using System;
using System.Globalization;
using System.Reflection;

namespace Core.Helpers
{
    public static class Reflector
    {
        public static BindingFlags SetterBindingFlags { get; } = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

        private static BindingFlags GetterBindingFlags { get; } = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;

        public static object GetPropertyValue(object obj, string propName)
        {
            var property = GetProperty(obj, propName);
            return property?.GetValue(obj, null) ?? "";
        }

        public static PropertyInfo GetProperty(object obj, string propName)
        {
            return obj.GetType().GetProperty(propName, GetterBindingFlags);
        }

        public static void SetProperty<T>(T obj, string propertyName, object value)
        {
            obj.GetType().InvokeMember(propertyName,
                                SetterBindingFlags,
                                Type.DefaultBinder,
                                obj,
                                new[] { value }, CultureInfo.InvariantCulture);
        }

        public static void ConvertAndSetProperty(object obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = Reflector.GetProperty(obj, propertyName);
            if (propertyInfo == null) 
            {
                throw new ArgumentException($"Property '{propertyName}' not found for type {obj.GetType()}");
            }
            var converter = ConverterFactory.GetConverter(propertyInfo.PropertyType);
            converter.SetProperty(obj, propertyName, converter.Convert(value));
        }
    }
}
