﻿using Core.Helpers;
using System.Collections.Generic;

namespace Core.Models.Converters
{
    public class DecimalValueConverter : BaseValueConverter, IValueConverter
    {

        public IEnumerable<string> RemoveCharacters { get; }

        public DecimalValueConverter(IEnumerable<string> removeCharacters)
        {
            Guards.NotNull(removeCharacters, "removeCharacters cannot be null");
            foreach (var removeCharacter in removeCharacters) 
            {
                Guards.NotNullOrEmpty(removeCharacter, "removeCharacter cannot be null");
            }
            this.RemoveCharacters = removeCharacters;
        }

        public DecimalValueConverter() : this(new[] { ",", "đ" })
        {
        }

        public object Convert(object obj)
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

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
