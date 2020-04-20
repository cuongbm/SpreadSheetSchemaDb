﻿using Core.Exceptions;
using Core.Helpers;
using System;

namespace Core.Schemas
{
    public class PropertyValidator
    {
        private readonly BasePropertyDefinition definition;

        public PropertyValidator(BasePropertyDefinition definition)
        {
            Guards.NotNull(definition, "definition cannot be null");
            this.definition = definition;
        }
        public void Validate(object typedValue)
        {
            switch (definition.Type)
            {
                case SchemaPropertyType.Decimal:
                    ValidateMinMax(typedValue);
                    break;
                case SchemaPropertyType.Int:
                    ValidateMinMax(typedValue);
                    break;
                case SchemaPropertyType.SingleChoice:
                    ValidatePossibleValue(typedValue);
                    break;
                case SchemaPropertyType.Double:
                    ValidateMinMax(typedValue);
                    break;
                case SchemaPropertyType.Boolean:
                    break;
                case SchemaPropertyType.Percentage:
                    break;
                case SchemaPropertyType.String:
                    break;
            }
        }

        private void ValidateMinMax(object typedValue)
        {
            var v = Convert.ToDecimal(typedValue);
            if (definition.Min.HasValue && v < definition.Min.Value && v != 0)
            {
                throw new InvalidValueException($"Thuộc tính ({definition.Value}-{definition.Text}):  Giá trị {v} nhỏ hơn giá trị tối thiểu {definition.Min}");
            }

            if (definition.Max.HasValue && v > definition.Max.Value)
            {
                throw new InvalidValueException($"Thuộc tính ({definition.Value}-{definition.Text}):  Giá trị {v} lớn hơn giá trị tối đa {definition.Max}");
            }
        }

        private void ValidatePossibleValue(object value)
        {
            var strVal = value.ToString();
            if (definition.PossibleValues.Count > 0 && !definition.PossibleValues.Contains(strVal))
            {
                throw new InvalidValueException($"Giá trị {value} không cho phép");
            }
        }
    }
}