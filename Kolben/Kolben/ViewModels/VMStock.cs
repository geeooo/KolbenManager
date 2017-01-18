using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.ViewModels
{
    public class VMStock : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }


        private decimal _unitQuantity;
        public decimal UnitQuantity
        {
            get { return _unitQuantity; }
            set
            {
                _unitQuantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _kgQuantity;
        public decimal KgQuantity
        {
            get { return _kgQuantity; }
            set
            {
                _kgQuantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                OnPropertyChanged();
            }
        }

        private decimal _kgPrice;
        public decimal KgPrice
        {
            get { return _kgPrice; }
            set
            {
                _kgPrice = value;
                OnPropertyChanged();
            }
        }

        public VMStock()
        {

        }

        public VMStock(Stock stock)
        {
            Id = stock.Id;
            UnitQuantity = stock.UnitQuantity;
            KgQuantity = stock.KgQuantity;
            UnitPrice = stock.UnitPrice;
            KgPrice = stock.KgPrice;          
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
