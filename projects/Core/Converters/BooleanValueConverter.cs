﻿using Core.Models.Converters;

 namespace Core.Converters
{
    public class BooleanValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            if (obj.ToString().ToLower() == "true" || obj.ToString() == "1") return true;
            if (obj.ToString().ToLower() == "false" || obj.ToString() == "0") return false;
            bool.TryParse(obj.ToString(), out bool result);
            return result;
        }

        public object ToOutput(object input)
        {
            if (input == null) return "";
            if (input is bool && ((bool) input) == true) {
                return "1";
            }
            return "";
        }
    }
}
