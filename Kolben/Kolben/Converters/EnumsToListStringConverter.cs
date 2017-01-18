using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Reflection;
using KolbenService;
using Kolben.Utils;

namespace Kolben.Converters
{
    public class EnumsToListStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return Enum.GetValues(value.GetType()).Cast<Enum>().Select(item => new EnumComboboxBinding() { Description = (CustomAttributeExtensions.GetCustomAttribute(item.GetType().GetField(item.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, Value = item.ToString(), EnumType = item.GetType()}).OrderBy(o => o.Value).ToList();          
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
