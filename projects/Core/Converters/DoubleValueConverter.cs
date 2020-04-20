using System.Collections.Generic;
using Core.Models.Converters;

namespace Core.Converters
{
    public class DoubleValueConverter : BaseValueConverter, IValueConverter
    {

        public IEnumerable<string> RemoveCharacters { get; }

        public DoubleValueConverter(IEnumerable<string> removeCharacters)
        {
            this.RemoveCharacters = removeCharacters;
        }

        public DoubleValueConverter()
        {
            this.RemoveCharacters = new List<string>();
        }

        public object FromInput(object obj)
        {
            if (obj == null) return 0.0d;
            string val = obj.ToString();
            foreach (var d in RemoveCharacters)
            {
                val = val.Replace(d, "");
            }
            var result = double.Parse(val);
            return result;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
