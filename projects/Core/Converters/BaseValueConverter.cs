﻿using Core.Helpers;

namespace Core.Models.Converters
{
    public abstract class BaseValueConverter
    {
        public bool ReadOnly { get; set; }

        public virtual void SetProperty<T>(T obj, string propertyName, object value)
        {
            Reflector.SetProperty(obj, propertyName, value);
        }
    }
}
