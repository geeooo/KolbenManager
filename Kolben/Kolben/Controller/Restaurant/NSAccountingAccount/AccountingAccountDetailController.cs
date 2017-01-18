using Kolben.ViewModels;
using KolbenService;
using KolbenService.Database.Entities;
using KolbenService.Database.Entities.Typeof;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.NSAccountingAccount
{
    public class AccountingAccountDetailController : BaseController
    {
        #region Attributes
        private VMAccountingAccount _accountingAccount;
        private List<VMTypeofAccountingAccount> _localTypeofAccountingAccounts;
        private ObservableCollection<VMTypeofAccountingAccount> _typeofAccountingAccounts;
        #endregion

        #region Getters / Setters
        public VMAccountingAccount AccountingAccount
        {
            get { return _accountingAccount; }
            set
            {
                if (_accountingAccount != value)
                {
                    _accountingAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VMTypeofAccountingAccount> TypeofAccountingAccounts
        {
            get { return _typeofAccountingAccounts; }
            set
            {
                if (_typeofAccountingAccounts != value)
                {
                    _typeofAccountingAccounts = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public AccountingAccountDetailController(ref VMAccountingAccount accountingAccount)
        {
            Init(accountingAccount);         
        }

        private async void Init(VMAccountingAccount accountingAccount)
        {
            await base.Init();

            AccountingAccount = accountingAccount;
            AccountingAccount.PropertyChanged += AccountingAccount_PropertyChanged;
        }

        protected override async Task Search()
        {
            var typeofAccountingAccounts = await KolbenServiceUnit.TypeofAccountingAccountService.GetAll();            
            _localTypeofAccountingAccounts = typeofAccountingAccounts.Select(toaa => new VMTypeofAccountingAccount(toaa)).ToList();
        }

        protected override void Display()
        {
            TypeofAccountingAccounts = new ObservableCollection<VMTypeofAccountingAccount>(_localTypeofAccountingAccounts.OrderBy(toaa => toaa.Name));
        }

        private async void AccountingAccount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentAccountingAccount = (VMAccountingAccount)sender;
            var accountingAccount = new AccountingAccount() { Id = currentAccountingAccount.Id, Name = currentAccountingAccount.Name, AccountNumber = currentAccountingAccount.AccountNumber, IdTypeofAccountingAccount = currentAccountingAccount.TypeofAccountingAccount.Id };
            await KolbenServiceUnit.AccountingAccountService.Update(accountingAccount);
        }
    }
}
