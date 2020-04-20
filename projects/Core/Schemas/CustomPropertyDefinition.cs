﻿using Core.Models.Converters;
using System;
using System.Collections.Generic;
 using Core.Converters;

 namespace Core.Schemas
{
    public class CustomPropertyDefinition : BasePropertyDefinition
    {
        public CustomPropertyDefinition() { }

        public CustomPropertyDefinition(SchemaPropertyType type, string name, string label, int offsetPos, bool allowInput = false)
        {
            Type = type;
            Value = name;
            Text = label;
            AllowInput = allowInput;
            OffsetPos = offsetPos;
        }

        public static CustomPropertyDefinition String(string name, int offset = 0, string label = null)
        {
            return new CustomPropertyDefinition(SchemaPropertyType.String, name, label ?? name, offset, true);
        }

        public static CustomPropertyDefinition Int(string name, int offset = 0, string label = null)
        {
            if (string.IsNullOrEmpty(label)) label = name;
            return new CustomPropertyDefinition(SchemaPropertyType.Int, name, label ?? name, offset, true);
        }

        public static CustomPropertyDefinition Double(string name, int offset = 0, string label = null)
        {
            if (string.IsNullOrEmpty(label)) label = name;
            return new CustomPropertyDefinition(SchemaPropertyType.Double, name ?? name, label, offset, true);
        }

        public static CustomPropertyDefinition Decimal(string name, int offset = 0, string label = null)
        {
            return new CustomPropertyDefinition(SchemaPropertyType.Decimal, name, label ?? name, offset, true);
        }

        public static CustomPropertyDefinition Boolean(string name, int offset = 0, string label = null)
        {
            return new CustomPropertyDefinition(SchemaPropertyType.Boolean, name, label ?? name, offset, true);
        }

        public static CustomPropertyDefinition Percentage(string name, int offset = 0, string label = null)
        {
            return new CustomPropertyDefinition(SchemaPropertyType.Percentage, name, label ?? name, offset, true);
        }

        public static CustomPropertyDefinition SingleChoice(string name, int offset = 0, string label = null, params string[] possibleValues)
        {
            var result = new CustomPropertyDefinition(SchemaPropertyType.SingleChoice, name, label ?? name, offset, true);
            result.PossibleValues.AddRange(possibleValues);
            return result;
        }

        public CustomPropertyDefinition DisallowInput()
        {
            AllowInput = false;
            return this;
        }

        public CustomPropertyDefinition SetAallowInput()
        {
            AllowInput = true;
            return this;
        }

        public override string ToString()
        {
            return $"{Type};{Value};{Text}";
        }

        public override bool Equals(object obj)
        {
            return obj is CustomPropertyDefinition definition &&
                   Value == definition.Value;
        }

        public override int GetHashCode()
        {
            return 2049151605 + Value.GetHashCode();
        }
    }

    public class BasePropertyDefinition
    {
        public string Value { get; set; }

        public bool Key { get; set; } = false;

        /// <summary>
        /// Offset with schema DataCellRange
        /// </summary>
        public int OffsetPos { get; set; }

        public string Text { get; set; }

        public bool Visible { get; set; } = true;

        public SchemaPropertyType Type { get; set; }

        public Type EnumType { get; set; }

        public object MatchEnumValue { get; set; }

        public bool AllowInput { get; set; }

        public bool FormulaProperty { get; set; }

        public decimal? Min { get; set; }

        public decimal? Max { get; set; }

        public int Scale { get; set; } = 2;

        public List<string> PossibleValues { get; } = new List<string>();

        public List<string> Tags { get; } = new List<string>();

        public IValueConverter GetConverter()
        {
            return ConverterFactory.GetConverter(this);
        }

        public object ConvertAndOutput(object rawValue)
        {
            return GetConverter().ToOutput(ConvertThenValidate(rawValue));
        }

        public object ConvertThenValidate(object rawValue)
        {
            var convertedValue = GetConverter().FromInput(rawValue);
            new PropertyValidator(this).Validate(convertedValue);
            return convertedValue;
        }
    }
}
