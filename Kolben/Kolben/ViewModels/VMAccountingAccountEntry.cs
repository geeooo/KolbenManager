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
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Kolben.ViewModels
{
    public class VMAccountingAccountEntry : INotifyPropertyChanged
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

        private ObservableCollection<VMAccountingAccountEntryDetail> _accountingAccountEntryDetails;
        public ObservableCollection<VMAccountingAccountEntryDetail> AccountingAccountEntryDetails
        {
            get { return _accountingAccountEntryDetails; }
            set
            {
                if (_accountingAccountEntryDetails != value)
                {
                    _accountingAccountEntryDetails = value;
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

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                    OnPropertyChanged("DateString");
                }
            }
        }

        public string DateString {
            get
            {
                return _date.ToString("dd/MM/yyyy");
            }
        }


        private char _operationIcon;

        public char OperationIcon
        {
            get { return _operationIcon; }
            set
            {
                if (_operationIcon != value)
                {
                    _operationIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccountingAccountEntryOperation _accountingAccountEntryOperation;

        public AccountingAccountEntryOperation AccountingAccountEntryOperation
        {
            get { return _accountingAccountEntryOperation; }
            set
            {
                if (_accountingAccountEntryOperation != value)
                {
                    _accountingAccountEntryOperation = value;
                    OnPropertyChanged();
                }
            }
        }


        private SolidColorBrush _operationBackground;

        public SolidColorBrush OperationBackground
        {
            get { return _operationBackground; }
            set
            {
                if (_operationBackground != value)
                {
                    _operationBackground = value;
                    OnPropertyChanged();
                }
            }
        }


        public VMAccountingAccountEntry()
        {

        }

        public VMAccountingAccountEntry(AccountingAccountEntry accountingAccountEntry)
        {
            Id = accountingAccountEntry.Id;
            Label = accountingAccountEntry.Label;
            Date = accountingAccountEntry.Date;
            AccountingAccountEntryOperation = accountingAccountEntry.AccountingAccountEntryOperation;
            OperationIcon = accountingAccountEntry.AccountingAccountEntryOperation == AccountingAccountEntryOperation.Credit ? (char)Symbol.Add : (char)Symbol.Remove;
            OperationBackground = accountingAccountEntry.AccountingAccountEntryOperation == AccountingAccountEntryOperation.Credit ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);

            if (accountingAccountEntry.AccountingAccountEntryDetails != null && accountingAccountEntry.AccountingAccountEntryDetails.Any())
            {
                AccountingAccountEntryDetails = new ObservableCollection<VMAccountingAccountEntryDetail>(accountingAccountEntry.AccountingAccountEntryDetails.Select(aaed => new VMAccountingAccountEntryDetail(aaed)));
                Amount = AccountingAccountEntryDetails.Sum(aae => aae.Amount);
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
