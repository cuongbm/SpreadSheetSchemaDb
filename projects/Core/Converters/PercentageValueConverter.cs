﻿using System.Collections.Generic;

namespace Core.Models.Converters
{
    public class PercentageValueConverter : BaseValueConverter, IValueConverter
    {
        private bool autoScale = false;
        public PercentageValueConverter() : this(true)
        {
        }

        public PercentageValueConverter(bool autoScale)
        {
            this.autoScale = autoScale;
        }

        public object Convert(object obj)
        {
            if (obj == null) return 0d;
            string val = obj.ToString();
            var multiplier = val.EndsWith("%") ? 0.01d : 1d;
            _ = double.TryParse(val.Replace("%", ""), out double result);
            return result * multiplier;
        }

        public object Output(object input)
        {
            if (input == null || ReadOnly) return "";
            if (input.ToString().EndsWith("%")) return input;
            _ = double.TryParse(input.ToString(), out double result);
            return (result * 100) + "%";
        }
    }
}
