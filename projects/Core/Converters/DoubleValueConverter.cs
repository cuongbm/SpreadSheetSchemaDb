﻿using System.Collections.Generic;

namespace Core.Models.Converters
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

        public object Convert(object obj)
        {
            if (obj == null) return 0.0d;
            string val = obj.ToString();
            foreach (var d in RemoveCharacters)
            {
                val = val.Replace(d, "");
            }
            _ = double.TryParse(val, out double result);
            return result;
        }

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            return input.ToString();
        }
    }
}
