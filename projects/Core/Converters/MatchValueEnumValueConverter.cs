using System;
using System.Globalization;
using Core.Helpers;

namespace Core.Converters
{
    public class MatchEnumValueConverter<T> : BaseValueConverter, IValueConverter where T : struct, IComparable, IFormattable, IConvertible
    {
        public T Value { get; }

        public MatchEnumValueConverter(T value)
        {
            Guards.NotNull(value, "value");
            Value = value;
        }

        public object FromInput(object obj)
        {
            if (obj is Enum) 
            {
                if (obj.GetType() == Value.GetType() && ((int)obj & Value.ToInt32(CultureInfo.InvariantCulture)) > 0)
                {
                    return Value;
                }
                return null;
            }
            
            var intVal = (int)new IntValueConverter().FromInput(obj);
            if (intVal > 0)
            {
                return Value;
            };
            return null;
        }

        public object ToOutput(object input)
        {
            if (input == null) return "";
            var parsed = (T)input;
            var result = parsed.ToInt32(CultureInfo.InvariantCulture) & Value.ToInt32(CultureInfo.InvariantCulture);

            return result == 0 ? "" : result.ToString();
        }

        public override void SetProperty<TObject>(TObject obj, string propertyName, object value)
        {
            var property = Reflector.GetProperty(obj, propertyName);
            if (property == null)
            {
                throw new InvalidOperationException($"Property {propertyName} not found");
            }

            if (value == null) return;
            if (value.ToString() == "") return;

            var currentValue = (int)Reflector.GetPropertyValue(obj, propertyName);
            var inputValue = (int)value;
            var enumValue = Enum.ToObject(property.PropertyType, currentValue | inputValue);

            Reflector.SetProperty(obj, propertyName, enumValue);
        }
    }
}
