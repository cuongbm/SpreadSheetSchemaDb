using System;
using System.Collections.Generic;
using Core.Models.Converters;
using Core.Schemas;

namespace Core.Converters
{
    public static class ConverterFactory
    {
        private static Dictionary<string, IValueConverter> covertersMap = new Dictionary<string, IValueConverter>() {
            { "Int32", new IntValueConverter()},
            { "Int64", new IdValueConverter()},
            { "Decimal", new DecimalValueConverter()},
            { "String", new StringValueConverter()},
        };

        public static IValueConverter GetConverter(BasePropertyDefinition defintion)
        {
            switch (defintion.Type)
            {
                case SchemaPropertyType.Decimal:
                    return new DecimalValueConverter();
                case SchemaPropertyType.Int:
                    return new IntValueConverter();
                case SchemaPropertyType.SingleChoice:
                    return new StringValueConverter();
                case SchemaPropertyType.Double:
                    return new DoubleValueConverter();
                case SchemaPropertyType.Boolean:
                    return new BooleanValueConverter();
                case SchemaPropertyType.Percentage:
                    return new PercentageValueConverter();
                case SchemaPropertyType.String:
                    return new StringValueConverter();
                case SchemaPropertyType.Date:
                    return new DateValueConverter();
                case SchemaPropertyType.Id:
                    return new IdValueConverter();
                case SchemaPropertyType.MatchEnum:
                    return CreateMatchEnumConverter(defintion);
                case SchemaPropertyType.Enum:
                    return CreateEnumConverter(defintion);
                default:
                    throw new InvalidCastException($"Converter for type {defintion.Type} is not configured");
            }
        }

        private static IValueConverter CreateMatchEnumConverter(BasePropertyDefinition defintion)
        {
            if (defintion.EnumType == null || defintion.MatchEnumValue == null)
            {
                throw new ArgumentException($"Definition for property {defintion.Value} of type MatchEnum " +
                    $"but EnumType and MatchEnumValue is not set");
            }

            var converterType = typeof(MatchEnumValueConverter<>);
            var intVal = defintion.MatchEnumValue is Enum ? (int)defintion.MatchEnumValue :
                (int)new IntValueConverter().FromInput(defintion.MatchEnumValue);

            var enumValue = Enum.ToObject(defintion.EnumType, intVal);
            Type[] typeArgs = { defintion.EnumType };
            var genericType = converterType.MakeGenericType(typeArgs);
            var converter = (IValueConverter)Activator.CreateInstance(genericType, enumValue);
            return converter;
        }

        private static IValueConverter CreateEnumConverter(BasePropertyDefinition defintion)
        {
            var converterType = typeof(EnumValueConverter<>);
            Type[] typeArgs = { defintion.EnumType };
            var genericType = converterType.MakeGenericType(typeArgs);
            var converter = (IValueConverter)Activator.CreateInstance(genericType);
            return converter;
        }
    }
}
