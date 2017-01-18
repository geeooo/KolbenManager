using Kolben.Utils;
using KolbenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Kolben.Converters
{
    public class SingleEnumItemToDictionnary : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return Enum.GetValues(value.GetType()).Cast<Enum>().Select(item => new EnumComboboxBinding() { Description = (CustomAttributeExtensions.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, Value = value.ToString(), EnumType = value.GetType()}).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;

            var enumComboboxBinding = (EnumComboboxBinding)value;
            var test = Enum.GetValues(enumComboboxBinding.EnumType).Cast<Enum>().FirstOrDefault(item => (CustomAttributeExtensions.GetCustomAttribute(item.GetType().GetField(item.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description.Equals(enumComboboxBinding.Description));
            return test;
        }
    }
}
