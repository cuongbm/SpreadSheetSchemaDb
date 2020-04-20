﻿using Core.Exceptions;
using Core.Helpers;
using Core.Models.Converters;
using System.Collections.Generic;
using System.Linq;

 namespace Core.Schemas
{
    public abstract class HasCustomProperties : IHasCustomProperties
    {
        public HashSet<CustomProperty> CustomProperties { get; set; } = new HashSet<CustomProperty>();

        public CustomProperty GetCustomProperty(string name)
        {
            return CustomProperties.SingleOrDefault(x => x.Definition.Value == name);
        }

        public CustomProperty GetCustomPropertyByLabel(string label)
        {
            return CustomProperties.SingleOrDefault(x => x.Definition.Text.Equals(label, System.StringComparison.OrdinalIgnoreCase));
        }
    }

    public interface IHasCustomProperties
    {
        HashSet<CustomProperty> CustomProperties { get; }
    }
}
