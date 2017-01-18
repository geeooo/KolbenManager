using GalaSoft.MvvmLight.Command;
using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.NSPurchases;
using KolbenService;
using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.NSPurchases
{
    public class PurchasesController : BaseController
    {
        #region Attributes
        private List<VMPurchase> _localPurchases;
        private ObservableCollection<VMPurchase> _purchases;
        private VMPurchase _currentPurchase;

        private ActionData _addNewPurchaseActionData;
        #endregion

        #region Getters / Setters
        public ObservableCollection<VMPurchase> Purchases
        {
            get { return _purchases; }
            set
            {
                if (_purchases != value)
                {
                    _purchases = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMPurchase CurrentPurchase
        {
            get { return _currentPurchase; }
            set
            {
                if (_currentPurchase != value)
                {
                    _currentPurchase = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public PurchasesController()
        {
            Init();
        }

        protected override void InitReferences()
        {
            base.InitReferences();

            Purchases = new ObservableCollection<VMPurchase>();
            _localPurchases = new List<VMPurchase>();
        }

        private async void Init()
        {
            await base.Init();

            CurrentPurchase = Purchases.FirstOrDefault();
        }

        protected override void Display()
        {
            Purchases = new ObservableCollection<VMPurchase>(_localPurchases.OrderByDescending(p => p.PurchaseDate));
        }

        protected override async Task Search()
        {
            var purchases = await KolbenServiceUnit.PurchaseService.GetAll(p => p.ProviderAccountingAccount, p => p.PurchaseDetails);
            _localPurchases = purchases.Select(p => new VMPurchase(p)).ToList();
        }

        protected override void InitCommands()
        {
            _addNewPurchaseActionData = new ActionData("Nouvel Achat", Symbol.Add, new RelayCommand<object>(o => AddNewPurchaseCommandExecute()));
            
            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewPurchaseActionData
                }
            };
        }

        #region Commands
        private async void AddNewPurchaseCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'un achat",
                Content = new AddPurchasePage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmPurchase = ((AddPurchaseController)((AddPurchasePage)dialog.Content).DataContext).Purchase;

                var idPurchase = await KolbenServiceUnit.PurchaseService.Add(new Purchase() { Label = vmPurchase.Label, PurchaseDate = vmPurchase.PurchaseDate, IdProviderAccountingAccount = vmPurchase.ProviderAccountingAccount.Id, IdDebitAccountingAccount = vmPurchase.DebitAccountingAccount.Id });
                var purchase = await KolbenServiceUnit.PurchaseService.GetSingle(idPurchase, p => p.ProviderAccountingAccount, p => p.PurchaseDetails);

                var newVmPurchase = new VMPurchase(purchase);
                _localPurchases.ToList().Add(newVmPurchase);
                Purchases.Add(newVmPurchase);
                Purchases.OrderBy(p => p.PurchaseDate);
                CurrentPurchase = newVmPurchase;
            }
        }
        #endregion
    }
}
