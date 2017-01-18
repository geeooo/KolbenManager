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
using Windows.UI.Popups;

namespace Kolben.Controller.Restaurant.NSPurchases
{
    public class PurchaseDetailController : BaseController
    {
        #region Attributes
        private List<VMAccountingAccount> _localAccountingAccounts;
        private List<VMProduct> _localProducts;
        private List<VMTypeofTVA> _localTypeofTVAs;

        private VMPurchase _purchase;
        private VMPurchaseDetail _newPurchaseDetail;

        private ObservableCollection<VMAccountingAccount> _accountingAccounts;
        private ObservableCollection<VMProduct> _products;
        private ObservableCollection<VMTypeofTVA> _typeofTVAs;

        private ICommand _addNewPurchaseDetailCommand;
        private ICommand _clearNewPurchaseDetailCommand;
        private ICommand _deletePurchaseDetailCommand;
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
        public VMPurchaseDetail NewPurchaseDetail
        {
            get { return _newPurchaseDetail; }
            set
            {
                if (_newPurchaseDetail != value)
                {
                    _newPurchaseDetail = value;
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

        public ObservableCollection<VMProduct> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VMTypeofTVA> TypeofTVAs
        {
            get { return _typeofTVAs; }
            set
            {
                if (_typeofTVAs != value)
                {
                    _typeofTVAs = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddNewPurchaseDetailCommand
        {
            get { return _addNewPurchaseDetailCommand; }
            set
            {
                if (_addNewPurchaseDetailCommand != value)
                {
                    _addNewPurchaseDetailCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ClearNewPurchaseDetailCommand
        {
            get { return _clearNewPurchaseDetailCommand; }
            set
            {
                if (_clearNewPurchaseDetailCommand != value)
                {
                    _clearNewPurchaseDetailCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DeletePurchaseDetailCommand
        {
            get { return _deletePurchaseDetailCommand; }
            set
            {
                if (_deletePurchaseDetailCommand != value)
                {
                    _deletePurchaseDetailCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public PurchaseDetailController(ref VMPurchase purchase)
        {
            Init(purchase);
        }

        private async void Init(VMPurchase purchase)
        {
            await base.Init();

            Purchase = purchase;
            Purchase.PropertyChanged += Purchase_PropertyChanged;
        }

        protected override void InitCommands()
        {
            AddNewPurchaseDetailCommand = new RelayCommand<object>(o => AddNewPurchaseDetailCommandExecute());
            ClearNewPurchaseDetailCommand = new RelayCommand<object>(o => ClearNewPurchaseDetailCommandExecute());
            DeletePurchaseDetailCommand = new RelayCommand<object>(o => DeletePurchaseDetailCommandExecute((VMPurchaseDetail)o));
        }

        protected override void InitReferences()
        {
            NewPurchaseDetail = new VMPurchaseDetail();
        }

        protected override async Task Search()
        {
            var accountingAccounts = await KolbenServiceUnit.AccountingAccountService.GetAll();
            _localAccountingAccounts = accountingAccounts.Select(aa => new VMAccountingAccount(aa)).ToList();

            var products = await KolbenServiceUnit.ProductService.GetAll();
            _localProducts = products.Select(p => new VMProduct(p)).ToList();

            var typeofTVAs = await KolbenServiceUnit.TypeofTVAService.GetAll();
            _localTypeofTVAs = typeofTVAs.Select(p => new VMTypeofTVA(p)).ToList();
        }

        protected override void Display()
        {
            AccountingAccounts = new ObservableCollection<VMAccountingAccount>(_localAccountingAccounts.OrderBy(aa => aa.Name));
            Products = new ObservableCollection<VMProduct>(_localProducts.OrderBy(p => p.Name));
            TypeofTVAs = new ObservableCollection<VMTypeofTVA>(_localTypeofTVAs.OrderBy(totva => totva.Name));
        }

        private async void Purchase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentPurchase = (VMPurchase)sender;
            var purchase = new Purchase() { Id = currentPurchase.Id, Label = currentPurchase.Label, PurchaseDate = currentPurchase.PurchaseDate, IdProviderAccountingAccount = currentPurchase.ProviderAccountingAccount.Id };
            await KolbenServiceUnit.PurchaseService.Update(purchase);
        }

        #region Commands
        private async void AddNewPurchaseDetailCommandExecute()
        {
            try
            {
                if (NewPurchaseDetail == null || NewPurchaseDetail != null && (NewPurchaseDetail.Product == null || NewPurchaseDetail.TypeofTVA == null))
                    throw new Exception("Une erreur s'est produite !");

                var purchaseDetail = new PurchaseDetail()
                {
                    IdPurchase = Purchase.Id,
                    IdProduct = NewPurchaseDetail.Product.Id,
                    IdTypeofTVA = NewPurchaseDetail.TypeofTVA.Id,
                    KGQuantity = NewPurchaseDetail.KgQuantity.HasValue ? NewPurchaseDetail.KgQuantity.Value : 0,
                    UnitQuantity = NewPurchaseDetail.UnitQuantity.HasValue ? NewPurchaseDetail.UnitQuantity.Value : 0,
                    Price = NewPurchaseDetail.Price.HasValue ? NewPurchaseDetail.Price.Value : 0
                };

                NewPurchaseDetail.Id = await KolbenServiceUnit.PurchaseDetailService.Add(purchaseDetail);

                Purchase.PurchaseDetails.Add(NewPurchaseDetail);

                ClearNewPurchaseDetailCommandExecute();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.Commands.Add(new UICommand("Ok"));
                messageDialog.ShowAsync();
            }            
        }

        private void ClearNewPurchaseDetailCommandExecute()
        {
            NewPurchaseDetail = new VMPurchaseDetail();
        }

        private async void DeletePurchaseDetailCommandExecute(VMPurchaseDetail purchaseDetail)
        {
            try
            {
                if (purchaseDetail == null)
                {
                    throw new Exception("Une erreur s'est produite");
                }

                var messageDialog = new MessageDialog("Voulez-vous supprimer ce détail ?");

                messageDialog.Commands.Add(new UICommand(
           "Oui", async (o) =>
           {
               await KolbenServiceUnit.PurchaseDetailService.Delete(purchaseDetail.Id);
               var purchase = await KolbenServiceUnit.PurchaseService.GetSingle(Purchase.Id, p => p.PurchaseDetails, p => p.ProviderAccountingAccount);
               Purchase = new VMPurchase(purchase);
           }));

                messageDialog.Commands.Add(new UICommand(
                    "Non"));

                messageDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.Commands.Add(new UICommand("Ok"));
                messageDialog.ShowAsync();
            }
        }
    }
        #endregion
}
