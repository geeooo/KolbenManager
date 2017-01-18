using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controls
{
    public class IconButton : Button
    {
        public Symbol Icon
        {
            get { return (Symbol)GetValue(IconProperty); }
            set
            {
                SetValue(IconProperty, value);
                SetValue(IconCharProperty, ((char)value).ToString());
            }
        }

        public string IconChar
        {
            get { return (string)GetValue(IconCharProperty); }
        }

        public static readonly DependencyProperty IconProperty =
                  DependencyProperty.Register("Icon",
                      typeof(Symbol), typeof(IconButton), null);

        public static readonly DependencyProperty IconCharProperty =
                  DependencyProperty.Register("IconChar",
                      typeof(string), typeof(IconButton), null);
    }
}
