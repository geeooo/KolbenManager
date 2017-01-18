using Kolben.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.NSCashRegister
{
    public class TablesController : BaseController
    {
        private ObservableCollection<VMTable> _tables;
        private List<VMTable> _localTables;

        public ObservableCollection<VMTable> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
