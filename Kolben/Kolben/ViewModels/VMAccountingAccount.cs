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
    public class VMAccountingAccount : INotifyPropertyChanged
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

        private string _name;
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

        private VMTypeofAccountingAccount _typeofAccountingAccount;
        public VMTypeofAccountingAccount TypeofAccountingAccount
        {
            get { return _typeofAccountingAccount; }
            set
            {
                if (_typeofAccountingAccount != value)
                {
                    _typeofAccountingAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (_accountNumber != value)
                {
                    _accountNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<VMAccountingAccountEntry> _accountingAccountEntries;
        public ObservableCollection<VMAccountingAccountEntry> AccountingAccountEntries
        {
            get { return _accountingAccountEntries; }
            set
            {
                if (_accountingAccountEntries != value)
                {
                    _accountingAccountEntries = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMAccountingAccount()
        {

        }

        public VMAccountingAccount(AccountingAccount accountingAccount)
        {
            Id = accountingAccount.Id;
            Name = accountingAccount.Name;
            AccountNumber = accountingAccount.AccountNumber;

            if(accountingAccount.AccountingAccountEntries != null && accountingAccount.AccountingAccountEntries.Any())
            {
                AccountingAccountEntries = new ObservableCollection<VMAccountingAccountEntry>(accountingAccount.AccountingAccountEntries.Select(aa => new VMAccountingAccountEntry(aa)));
                ComputeBalance();
            }

            if(accountingAccount.TypeofAccountingAccount != null)
            {
                TypeofAccountingAccount = new VMTypeofAccountingAccount(accountingAccount.TypeofAccountingAccount);
            }
        }

        private void ComputeBalance()
        {
            foreach (var accountingAccountEntry in AccountingAccountEntries)
            {
                if (accountingAccountEntry.AccountingAccountEntryOperation == AccountingAccountEntryOperation.Credit)
                {
                    Balance += accountingAccountEntry.Amount;
                }
                else if (accountingAccountEntry.AccountingAccountEntryOperation == AccountingAccountEntryOperation.Debit)
                {
                    Balance -= accountingAccountEntry.Amount;
                }
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

        protected bool Equals(VMAccountingAccount other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VMAccountingAccount)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                return hashCode;
            }
        }
    }
}
