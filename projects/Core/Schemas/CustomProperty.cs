using System.Collections.Generic;
using Core.Models.Converters;

namespace Core.Schemas
{
    public class CustomProperty
    {
        public CustomPropertyDefinition Definition { get; set; }
        public object Value { get; set; }

        public CustomProperty(CustomPropertyDefinition definition, object value)
        {
            Definition = definition;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Definition} - {Value}";
        }

        public override bool Equals(object obj)
        {
            return obj is CustomProperty property &&
                   EqualityComparer<CustomPropertyDefinition>.Default.Equals(Definition, property.Definition);
        }

        public override int GetHashCode()
        {
            return -1296319296 + Definition.GetHashCode();
        }

        public object OutputValue()
        {
            var converter = ConverterFactory.GetConverter(Definition);
            var output = converter.Output(Value);
            return output;
        }
    }
}
