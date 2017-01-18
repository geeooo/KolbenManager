using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views;
using KolbenService;
using KolbenService.Database.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using Kolben.Views.Restaurant.NSProducts;
using Windows.UI.Popups;
using Kolben.Views.Restaurant.NSPurchases;

namespace Kolben.Controller.Restaurant.NSProducts
{
    public class ProductsController : BaseController
    {
        #region Attributes
        private ObservableCollection<VMProduct> _products;
        private IQueryable<VMProduct> _localProducts;
        private VMProduct _currentProduct;

        private ActionData _addNewProductActonData;
        private ActionData _deleteProductActionData;
        #endregion

        #region Getters / Setters
        public ObservableCollection<VMProduct> Products
        {
            get { return _products; }
            set
            {
                if (value != _products)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }
        public VMProduct CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                if (value != _currentProduct)
                {
                    _currentProduct = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public ProductsController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();
        }

        protected override async Task Search()
        {
            var products = await KolbenServiceUnit.ProductService.GetAll(p => p.Stocks, p => p.TypeofProductCategory);
            _localProducts = products.Select(p => new VMProduct(p)).AsQueryable();
            _products = new ObservableCollection<VMProduct>(_localProducts);
        }

        protected override void InitReferences()
        {

        }

        public void FilterProductList(string searchText)
        {
            if (_localProducts == null || !_localProducts.Any())
                return;

            var whereClause = PredicateBuilder.True<VMProduct>();
            whereClause = whereClause.And(p => p.Name.ToLower().StartsWith(searchText.ToLower()));

            var products = _localProducts.Where(whereClause).ToList();

            if (products.Count == Products.Count)
                return;

            Products = new ObservableCollection<VMProduct>(products.OrderBy(p => p.Name));
        }

        protected override void InitCommands()
        {           
            _addNewProductActonData = new ActionData("Nouveau produit", Symbol.Add, new RelayCommand<object>(o => AddNewProductCommandExecute()));
            _deleteProductActionData = new ActionData("Supprimer produit", Symbol.Delete, new RelayCommand<object>(o => DeleteProductCommandExecute(), o => CurrentProductCanExecute()));

            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewProductActonData
                },
                new ObservableCollection<ActionData>()
                {
                    _deleteProductActionData
                }
            };
        }

        protected override void Display()
        {
            Products = new ObservableCollection<VMProduct>(_products.OrderBy(p => p.Name));
            CurrentProduct = _products.FirstOrDefault();
        }

        #region Commands
        private async void AddNewProductCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'un nouveau produit",
                Content = new AddProductPage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmProduct = ((AddProductController)((AddProductPage)dialog.Content).DataContext).CurrentProduct;
                var idProduct = await KolbenServiceUnit.ProductService.Add(new Product() { Name = vmProduct.Name, IdTypeofProductCategory = vmProduct.TypeofProductCategory.Id});
                var product = await KolbenServiceUnit.ProductService.GetSingle(idProduct, p => p.Stocks, p => p.TypeofProductCategory);

                var newVmProduct = new VMProduct(product);
                _localProducts.ToList().Add(newVmProduct);
                Products.Add(newVmProduct);
                Products.OrderBy(p => p.Name);
                CurrentProduct = newVmProduct;
            }
        }

        private bool CurrentProductCanExecute()
        {
            return CurrentProduct != null && CurrentProduct.Id != 0;
        }

        private async void DeleteProductCommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer le produit ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.ProductService.Delete(CurrentProduct.Id);
           await Search();           
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }


        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == "CurrentProduct")
            {
                _deleteProductActionData.Command.RaiseCanExecuteChanged();
            }
        }
    }
}
