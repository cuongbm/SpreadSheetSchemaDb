namespace Core.Converters
{
    public class PercentageValueConverter : BaseValueConverter, IValueConverter
    {
        public object FromInput(object obj)
        {
            if (obj == null) return 0d;
            string val = obj.ToString();
            var multiplier = val.EndsWith("%") ? 0.01d : 1d;
            _ = double.TryParse(val.Replace("%", ""), out double result);
            return result * multiplier;
        }

        public object ToOutput(object input)
        {
            if (input == null || ReadOnly) return "";
            if (input.ToString().EndsWith("%")) return input;
            _ = double.TryParse(input.ToString(), out double result);
            return (result * 100) + "%";
        }
    }
}
