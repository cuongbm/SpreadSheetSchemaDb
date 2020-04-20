using System;
using System.Collections.Generic;
using Core.Converters;
using Core.Helpers;
using Core.Models.Converters;

namespace Core.Schemas
{
    public class SchemaModelConverter : IModelConverter
    {
        private readonly PropertySchema schema;

        public SchemaModelConverter(PropertySchema schema)
        {
            this.schema = schema;
        }

        public IList<T> ParseAll<T>(IList<IList<object>> values)
        {
            Guards.NotNull(values, "values");
            var result = new List<T>();

            foreach (var row in values)
            {
                result.Add(Parse<T>(row));
            }

            return result;
        }

        public T Parse<T>(IList<object> row)
        {
            var item = Activator.CreateInstance<T>();
            ObtainValues(item, row);
            return item;
        }

        private void ObtainValues(object obj, IList<object> row)
        {
            foreach (var systemDefintion in schema.SystemPropertyDefinitions)
            {
                if (systemDefintion.OffsetPos >= row.Count) continue;
                var converter = ConverterFactory.GetConverter(systemDefintion);
                var convertedValue = converter.FromInput(row[systemDefintion.OffsetPos]);
                converter.SetProperty(obj, systemDefintion.Value, convertedValue);
            }

            if (typeof(IHasCustomProperties).IsAssignableFrom(obj.GetType()))
            {
                var typedObject = (IHasCustomProperties)obj;
                foreach (var customDefinition in schema.CustomPropertyDefinitions)
                {
                    var converter = ConverterFactory.GetConverter(customDefinition);
                    var rawValue = row.Count > customDefinition.OffsetPos ?
                        row[customDefinition.OffsetPos] : "";
                    typedObject.SetCustomProperty
                        (new CustomProperty(customDefinition, converter.FromInput(rawValue)));
                }
            }
        }
    }
}
