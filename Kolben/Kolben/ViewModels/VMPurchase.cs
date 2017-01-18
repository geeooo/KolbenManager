using KolbenService.Database.Entities;
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
    public class VMPurchase : INotifyPropertyChanged
    {
        private int _id;
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

        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _purchaseDate;
        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set
            {
                if (_purchaseDate != value)
                {
                    _purchaseDate = value;
                    OnPropertyChanged();
                    OnPropertyChanged("PurchaseDateString");
                }
            }
        }

        public string PurchaseDateString
        {
            get { return PurchaseDate.ToString("ddd dd MMM yyyy"); }
        }

        private VMAccountingAccount _providerAccountingAccount;
        public VMAccountingAccount ProviderAccountingAccount
        {
            get { return _providerAccountingAccount; }
            set
            {
                if (_providerAccountingAccount != value)
                {
                    _providerAccountingAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        private VMAccountingAccount _debitAccountingAccount;
        public VMAccountingAccount DebitAccountingAccount
        {
            get { return _debitAccountingAccount; }
            set
            {
                if (_debitAccountingAccount != value)
                {
                    _debitAccountingAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<VMPurchaseDetail> _purchaseDetails;
        public ObservableCollection<VMPurchaseDetail> PurchaseDetails
        {
            get { return _purchaseDetails; }
            set
            {
                if (_purchaseDetails != value)
                {
                    _purchaseDetails = value;
                    OnPropertyChanged();
                }
            }
        }


        public VMPurchase() { }

        public VMPurchase(Purchase purchase)
        {
            Id = purchase.Id;
            Label = purchase.Label;
            PurchaseDate = purchase.PurchaseDate;

            if(purchase.ProviderAccountingAccount != null)
            {
                ProviderAccountingAccount = new VMAccountingAccount(purchase.ProviderAccountingAccount);
            }

            if (purchase.DebitAccountingAccount != null)
            {
                DebitAccountingAccount = new VMAccountingAccount(purchase.DebitAccountingAccount);
            }

            if (purchase.PurchaseDetails != null)
            {
                PurchaseDetails = new ObservableCollection<VMPurchaseDetail>(purchase.PurchaseDetails.Select(pd => new VMPurchaseDetail(pd)));
            }
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
