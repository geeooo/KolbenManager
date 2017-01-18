using KolbenService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.ViewModels
{
    public class VMAccountingAccountEntryDetail : INotifyPropertyChanged
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

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
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

        public VMAccountingAccountEntryDetail()
        {
        }

        public VMAccountingAccountEntryDetail(AccountingAccountEntryDetail accountingAccountEntryDetail)
        {
            Id = accountingAccountEntryDetail.Id;
            Amount = accountingAccountEntryDetail.Amount;
            
            if(accountingAccountEntryDetail.TypeofTVA != null)
            {
                TypeofTVA = new VMTypeofTVA(accountingAccountEntryDetail.TypeofTVA);
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
