﻿using Core.Models.Converters;
using System;
using System.Globalization;
using System.Reflection;
 using Core.Converters;

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
    }
}
