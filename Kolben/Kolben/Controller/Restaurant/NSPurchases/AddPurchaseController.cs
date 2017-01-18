using GalaSoft.MvvmLight.Command;
using Kolben.ViewModels;
using KolbenService;
using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kolben.Controller.Restaurant.NSPurchases
{
    public class AddPurchaseController : BaseController
    {
        #region Attributes
        private List<VMAccountingAccount> _localAccountingAccounts;
        private VMPurchase _purchase;
        
        private ObservableCollection<VMAccountingAccount> _accountingAccounts;
        #endregion

        #region Getters / Setters
        public VMPurchase Purchase
        {
            get { return _purchase; }
            set
            {
                if (_purchase != value)
                {
                    _purchase = value;
                    OnPropertyChanged();
                }
            }
        }    
        public ObservableCollection<VMAccountingAccount> AccountingAccounts
        {
            get { return _accountingAccounts; }
            set
            {
                if (_accountingAccounts != value)
                {
                    _accountingAccounts = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public AddPurchaseController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();

            Purchase.PurchaseDate = DateTime.Now;
        }

        protected override void InitReferences()
        {
            Purchase = new VMPurchase();
            AccountingAccounts = new ObservableCollection<VMAccountingAccount>();
        }



        protected override async Task Search()
        {
            var accountingAccounts = await KolbenServiceUnit.AccountingAccountService.GetAll();
            _localAccountingAccounts = accountingAccounts.Select(aa => new VMAccountingAccount(aa)).ToList();
        }

        protected override void Display()
        {
            AccountingAccounts = new ObservableCollection<VMAccountingAccount>(_localAccountingAccounts.OrderBy(aa => aa.Name));
        }

        #region Commands

        #endregion

    }
}
