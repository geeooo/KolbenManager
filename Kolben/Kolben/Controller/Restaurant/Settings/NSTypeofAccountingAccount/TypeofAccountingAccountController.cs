using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.Settings.NSTypeofAccountingAccount;
using KolbenService;
using KolbenService.Database.Entities.Typeof;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofAccountingAccount
{
    public class TypeofAccountingAccountController : BaseController
    {
        #region Attributes
        private List<VMTypeofAccountingAccount> _localTypeofAccountingAccounts;
        private ObservableCollection<VMTypeofAccountingAccount> _typeofAccountingAccounts;
        private VMTypeofAccountingAccount _typeofAccountingAccount;

        private ActionData _addNewTypeofAccountingAccountActionData;
        private ActionData _deleteTypeofAccountingAccountActionData;
        #endregion

        #region Getters / Setters
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
        #endregion

        public TypeofAccountingAccountController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();

            TypeofAccountingAccount = TypeofAccountingAccounts.FirstOrDefault();
        }

        protected override void Display()
        {
            TypeofAccountingAccounts = new ObservableCollection<VMTypeofAccountingAccount>(_localTypeofAccountingAccounts);
        }

        protected override void InitCommands()
        {
            _addNewTypeofAccountingAccountActionData = new Utils.ActionData("Nouveau type de compte", Symbol.Add, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => AddTypeofProductCategoryCommandExecute()));
            _deleteTypeofAccountingAccountActionData = new Utils.ActionData("Supprimer le type de compte", Symbol.Delete, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => DeleteTypeofAccountingAccountCommandExecute(), o => TypeofAccountingAccountCanExecute()));

            Actions = new ObservableCollection<ObservableCollection<Utils.ActionData>>()
            {
                new ObservableCollection<Utils.ActionData>()
                {
                    _addNewTypeofAccountingAccountActionData
                },
                new ObservableCollection<Utils.ActionData>()
                {
                    _deleteTypeofAccountingAccountActionData
                }
            };
        }

        protected override async Task Search()
        {
            var typeOfAccountingAccounts = await KolbenServiceUnit.TypeofAccountingAccountService.GetAll();
            _localTypeofAccountingAccounts = typeOfAccountingAccounts.Select(toaa => new VMTypeofAccountingAccount(toaa)).ToList();
        }

        #region Commands
        private async void AddTypeofProductCategoryCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'un nouveau type de compte",
                Content = new TypeofAccountingAccountAddPage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmTypeofAccountingAccount = ((TypeofAccountingAccountAddController)((TypeofAccountingAccountAddPage)dialog.Content).DataContext).TypeofAccountingAccount;
                var idTypeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.Add(new TypeofAccountingAccount() { Name = vmTypeofAccountingAccount.Name, Prefix = vmTypeofAccountingAccount.Prefix });
                var typeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.GetSingle(idTypeofAccountingAccount);

                var newVmTypeofAccountingAccount = new VMTypeofAccountingAccount(typeofAccountingAccount);
                _localTypeofAccountingAccounts.Add(newVmTypeofAccountingAccount);

                TypeofAccountingAccounts.Add(newVmTypeofAccountingAccount);
                TypeofAccountingAccounts.OrderBy(toaa => toaa.Name);
                TypeofAccountingAccount = TypeofAccountingAccounts.FirstOrDefault(toaa => toaa.Id == newVmTypeofAccountingAccount.Id);
            }
        }

        private async void DeleteTypeofAccountingAccountCommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer ce type de compte ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.TypeofAccountingAccountService.Delete(TypeofAccountingAccount.Id);
           await Search();
           Display();
           TypeofAccountingAccount = TypeofAccountingAccounts.FirstOrDefault();
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }

        private bool TypeofAccountingAccountCanExecute()
        {
            return TypeofAccountingAccount != null && TypeofAccountingAccount.Id > 0 && TypeofAccountingAccount.IsEditable;
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "TypeofAccountingAccount")
            {
                if (_deleteTypeofAccountingAccountActionData != null)
                {
                    _deleteTypeofAccountingAccountActionData.Command.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
