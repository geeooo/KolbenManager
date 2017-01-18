using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Kolben.Utils
{
    public class ActionData : INotifyPropertyChanged
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        private RelayCommand<object> _command;
        public RelayCommand<object> Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private Symbol _icon;

        public Symbol Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public ActionData(string label, Symbol icon, RelayCommand<object> command)
        {
            Label = label;
            Icon = icon;
            Command = command;
        }


        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
