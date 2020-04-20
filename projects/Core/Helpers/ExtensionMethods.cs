﻿using Core.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public static class ExtensionMethods
    {
        public static bool In(this Enum e, params Enum[] list)
        {
            return list.Contains(e);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static void AdoptValues(this IHasCustomProperties obj, KeyValuePair<string, object> propertyValue)
        {
            var existing = obj.CustomProperties.SingleOrDefault(x => x.Definition.Value == propertyValue.Key);

            if (existing == null)
            {
                return;
            }

            existing.Value = existing.Definition.ConvertThenValidate(propertyValue.Value);
        }

        public static IHasCustomProperties SetCustomProperty(this IHasCustomProperties obj, CustomProperty customProperty)
        {
            var existing = obj.CustomProperties.SingleOrDefault(x => x.Definition.Value == customProperty.Definition.Value);
            if (existing != null)
            {
                existing.Value = customProperty.Value;
            }
            else
            {
                obj.CustomProperties.Add(customProperty);
            }

            return obj;
        }

        public static object GetCustomPropertyValue(this IHasCustomProperties obj, string name)
        {
            var property = obj.CustomProperties.SingleOrDefault(x => x.Definition.Value == name);
            if (property == null)
            {
                throw new ArgumentException($"Property {name} not found");
            }

            return property.Value;
        }

        public static void Assign(this Dictionary<string, object> propertyValues, Dictionary<string, object> fromPropertyValues)
        {
            if (fromPropertyValues != null)
            {
                foreach (var kvp in fromPropertyValues)
                {
                    if (propertyValues.ContainsKey(kvp.Key))
                    {
                        propertyValues[kvp.Key] = kvp.Value;
                    }
                    else
                    {
                        propertyValues.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        public static IHasCustomProperties AddCustomProperties(this IHasCustomProperties obj, HashSet<CustomProperty> customProperties) 
        {
            customProperties.ForEach(cp => obj.CustomProperties.Add(cp));
            return obj;
        }
    }
}
