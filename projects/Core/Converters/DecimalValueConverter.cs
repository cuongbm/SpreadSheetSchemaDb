using System.Collections.Generic;
using System.Linq;
using Core.Helpers;
using Core.Models.Converters;

namespace Core.Converters
{
    public class DecimalValueConverter : BaseValueConverter, IValueConverter
    {

        public IEnumerable<string> RemoveCharacters { get; }

        public DecimalValueConverter(IEnumerable<string> removeCharacters)
        {
            Guards.NotNull(removeCharacters, "removeCharacters cannot be null");
            var characters = removeCharacters as string[] ?? removeCharacters.ToArray();
            foreach (var removeCharacter in characters) 
            {
                Guards.NotNullOrEmpty(removeCharacter, "removeCharacter cannot be null");
            }
            this.RemoveCharacters = characters;
        }

        public DecimalValueConverter() : this(new[] { ",", "đ" })
        {
        }

        public object FromInput(object obj)
        {
            if (obj == null) return 0m;
            string val = obj.ToString();
            foreach (var d in RemoveCharacters)
            {
                val = val.Replace(d, "");
            }
            val = val.Trim();
            _ = decimal.TryParse(val, out decimal result);
            return result;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
