using KolbenService.Database.Entities;
using KolbenService.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.ViewModels
{
    public class VMProduct : INotifyPropertyChanged
    {
        #region Attributes
        private string _name;
        private int _id;
        private VMTypeofProductCategory _typeofProductCategory;
        private ObservableCollection<VMStock> _stocks;
        private decimal _unitPrice;
        private decimal _totalUnitQuantity;
        private decimal _totalKgQuantity;
        private decimal _totalValuePrice;
        #endregion

        #region Getters / Setters
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMTypeofProductCategory TypeofProductCategory
        {
            get { return _typeofProductCategory; }
            set
            {
                if (value != _typeofProductCategory)
                {
                    _typeofProductCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VMStock> Stocks
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
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


        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged();
                }
            }
        }


        public decimal TotalUnitQuantity
        {
            get { return _totalUnitQuantity; }
            set
            {
                if (_totalUnitQuantity != value)
                {
                    _totalUnitQuantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal TotalKgQuantity
        {
            get { return _totalKgQuantity; }
            set
            {
                if (_totalKgQuantity != value)
                {
                    _totalKgQuantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal TotalPriceValue
        {
            get { return _totalValuePrice; }
            set
            {
                if (_totalValuePrice != value)
                {
                    _totalValuePrice = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public VMProduct()
        {

        }

        public VMProduct(Product product)
        {
            Name = product.Name;
            Id = product.Id;

            if (product.TypeofProductCategory != null)
            {
                TypeofProductCategory = new VMTypeofProductCategory(product.TypeofProductCategory);
            }

            if (product.Stocks != null)
            {
                Stocks = new ObservableCollection<VMStock>(product.Stocks.Where(s => s.KgQuantity > 0 || s.UnitQuantity > 0).Select(s => new VMStock(s)));
            }

            if (Stocks != null && Stocks.Any())
            {
                TotalUnitQuantity = Stocks.Sum(s => s.UnitQuantity);
                TotalKgQuantity = Math.Round(Stocks.Sum(s => s.KgQuantity), 3);
                TotalPriceValue = Math.Round(Stocks.Sum(s => s.UnitQuantity > 0 ? s.UnitPrice * s.UnitQuantity : s.KgPrice * s.KgQuantity), 2);
                UnitPrice = Math.Round(Stocks.Average(s => s.UnitPrice), 2);
                KgPrice = Math.Round(Stocks.Average(s => s.KgPrice), 3);
            }
        }

        public override string ToString()
        {
            return Name;
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
