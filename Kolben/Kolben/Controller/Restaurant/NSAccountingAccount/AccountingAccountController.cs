using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.NSAccountingAccount;
using KolbenService;
using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.NSAccountingAccount
{
    public class AccountingAccountController : BaseController
    {
        #region Attributes
        private List<VMAccountingAccount> _localAccountingAccounts;
        private ObservableCollection<VMAccountingAccount> _accountingAccounts;
        private VMAccountingAccount _accountingAccount;

        private ActionData _addNewAccountingAccountActionData;
        private ActionData _deleteAccountingAccountActionData;
        #endregion

        #region Getters / Setters
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
        #endregion

        public AccountingAccountController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();

            AccountingAccount = AccountingAccounts.FirstOrDefault();
        }

        protected override void Display()
        {
            AccountingAccounts = new ObservableCollection<VMAccountingAccount>(_localAccountingAccounts);
        }

        protected override void InitCommands()
        {
            _addNewAccountingAccountActionData = new ActionData("Nouveau compte", Symbol.Add, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => AddAccountingAccountCommandExecute()));
            _deleteAccountingAccountActionData = new ActionData("Supprimer le compte", Symbol.Delete, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => DeleteAccountingAccountCommandExecute(), o => AccountingAccountCanExecute()));

            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewAccountingAccountActionData
                },
                new ObservableCollection<ActionData>()
                {
                    _deleteAccountingAccountActionData
                }
            };
        }

        protected override async Task Search()
        {
            var accountingAccounts = await KolbenServiceUnit.AccountingAccountService.GetAll(aa => aa.TypeofAccountingAccount, aa => aa.AccountingAccountEntries);
            _localAccountingAccounts = accountingAccounts.Select(aa => new VMAccountingAccount(aa)).ToList();
        }

        #region Commands
        private async void AddAccountingAccountCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'un nouveau compte",
                Content = new AccountingAccountAddPage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmAccountingAccount = ((AccountingAccountAddController)((AccountingAccountAddPage)dialog.Content).DataContext).AccountingAccount;
                var idAccountingAccount = await KolbenServiceUnit.AccountingAccountService.Add(new AccountingAccount() { Name = vmAccountingAccount.Name, AccountNumber = vmAccountingAccount .AccountNumber, IdTypeofAccountingAccount = vmAccountingAccount.TypeofAccountingAccount.Id });
                var accountingAccount = await KolbenServiceUnit.AccountingAccountService.GetSingle(idAccountingAccount, aa => aa.TypeofAccountingAccount);

                var newVmAccountingAccount = new VMAccountingAccount(accountingAccount);
                _localAccountingAccounts.Add(newVmAccountingAccount);

                AccountingAccounts.Add(newVmAccountingAccount);
                AccountingAccounts = new ObservableCollection<VMAccountingAccount>(AccountingAccounts.OrderBy(toTVA => toTVA.Name));
                AccountingAccount = newVmAccountingAccount;
            }
        }

        private async void DeleteAccountingAccountCommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer le compte ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.AccountingAccountService.Delete(AccountingAccount.Id);
           await Search();
           Display();
           AccountingAccount = AccountingAccounts.FirstOrDefault();
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }

        private bool AccountingAccountCanExecute()
        {
            return AccountingAccount != null && AccountingAccount.Id > 0;
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "AccountingAccount")
            {
                if(_deleteAccountingAccountActionData != null)
                {
                    _deleteAccountingAccountActionData.Command.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
