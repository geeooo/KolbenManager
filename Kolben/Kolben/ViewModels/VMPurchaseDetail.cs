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
    public class VMPurchaseDetail : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private VMProduct _product;
        public VMProduct Product
        {
            get { return _product; }
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _unitQuantity;
        public decimal? UnitQuantity
        {
            get { return _unitQuantity; }
            set
            {
                if (_unitQuantity != value)
                {
                    _unitQuantity = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _kgQuantity;
        public decimal? KgQuantity
        {
            get { return _kgQuantity; }
            set
            {
                if (_kgQuantity != value)
                {
                    _kgQuantity = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        private VMTypeofTVA _typeofTVA;
        public VMTypeofTVA TypeofTVA
        {
            get { return _typeofTVA; }
            set
            {
                if (_typeofTVA != value)
                {
                    _typeofTVA = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMPurchaseDetail()
        {

        }

        public VMPurchaseDetail(PurchaseDetail purchaseDetail)
        {
            Id = purchaseDetail.Id;
            Product = new VMProduct(purchaseDetail.Product);
            UnitQuantity = purchaseDetail.UnitQuantity;
            KgQuantity = purchaseDetail.KGQuantity;
            Price = purchaseDetail.Price;
            TypeofTVA = new VMTypeofTVA(purchaseDetail.TypeOfTVA);         
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
