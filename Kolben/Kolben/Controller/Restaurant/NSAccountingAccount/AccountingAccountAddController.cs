using Kolben.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.NSAccountingAccount
{
    public class AccountingAccountAddController : BaseController
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

        public AccountingAccountAddController()
        {
            AccountingAccount = new VMAccountingAccount();

            Init();
        }

        protected override async Task Search()
        {
            var typeofAccountingAccounts = await KolbenService.KolbenServiceUnit.TypeofAccountingAccountService.GetAll();
            _localTypeofAccountingAccounts = typeofAccountingAccounts.Select(toaa => new VMTypeofAccountingAccount(toaa)).ToList();
        }

        protected override void Display()
        {
            TypeofAccountingAccounts = new ObservableCollection<VMTypeofAccountingAccount>(_localTypeofAccountingAccounts.OrderBy(toaa => toaa.Name));
        }
    }
}
