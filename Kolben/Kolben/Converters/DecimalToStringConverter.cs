using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Kolben.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var decimalValue = (decimal)value;
            return decimalValue.ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var separator = System.Convert.ToChar(CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (separator == ',' && value.ToString().Contains('.'))
            {
                value = value.ToString().Replace('.', ',');
            }
            else if (separator == '.' && value.ToString().Contains(','))
            {
                value = value.ToString().Replace(',', '.');
            }

            decimal decimalValue;
            if (decimal.TryParse(value.ToString(), out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
    }
}
