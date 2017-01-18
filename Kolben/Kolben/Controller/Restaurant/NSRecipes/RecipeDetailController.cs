using GalaSoft.MvvmLight.Command;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.NSRecipes;
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
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.NSRecipes
{
    public class RecipeDetailController : BaseController
    {
        #region Attributes
        private VMRecipe _currentRecipe;
        private ICommand _editRecipeProductCommand;
        private ICommand _deleteRecipeProductCommand;
        private ICommand _saveNewRecipeProductCommand;
        private ICommand _clearNewRecipeProductCommand;
        private List<VMProduct> _localProducts;
        private ObservableCollection<VMProduct> _products;
        private VMRecipeProduct _newRecipeProduct;
        #endregion

        #region Getters / Setters
        public VMRecipe CurrentRecipe
        {
            get { return _currentRecipe; }
            set
            {
                if (_currentRecipe != value)
                {
                    _currentRecipe = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand EditRecipeProductCommand
        {
            get { return _editRecipeProductCommand; }
            set
            {
                if (value != _editRecipeProductCommand)
                {
                    _editRecipeProductCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DeleteRecipeProductCommand
        {
            get { return _deleteRecipeProductCommand; }
            set
            {
                if (value != _deleteRecipeProductCommand)
                {
                    _deleteRecipeProductCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveNewRecipeProductCommand
        {
            get { return _saveNewRecipeProductCommand; }
            set
            {
                if (value != _saveNewRecipeProductCommand)
                {
                    _saveNewRecipeProductCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ClearNewRecipeProductCommand
        {
            get { return _clearNewRecipeProductCommand; }
            set
            {
                if (value != _clearNewRecipeProductCommand)
                {
                    _clearNewRecipeProductCommand = value;
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

        public VMRecipeProduct NewRecipeProduct
        {
            get { return _newRecipeProduct; }
            set
            {
                if (_newRecipeProduct != value)
                {
                    _newRecipeProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public RecipeDetailController(ref VMRecipe recipe)
        {
            Init(recipe);
        }

        private async void Init(VMRecipe recipe)
        {
            await base.Init();

            CurrentRecipe = recipe;
        }

        protected override void InitReferences()
        {
            NewRecipeProduct = new VMRecipeProduct();
        }

        protected override void Display()
        {
            Products = new ObservableCollection<VMProduct>(_localProducts.OrderBy(p => p.Name));
        }

        protected override void InitCommands()
        {
            EditRecipeProductCommand = new RelayCommand<object>(o => EditRecipeProductCommandExecute(o));
            DeleteRecipeProductCommand = new RelayCommand<object>(o => DeleteRecipeProductCommandExecute(o));
            SaveNewRecipeProductCommand = new RelayCommand<object>(o => SaveNewRecipeProductCommandExecute());
            ClearNewRecipeProductCommand = new RelayCommand<object>(o => ClearNewRecipeProductCommandExecute());
        }

    protected override async Task Search()
        {
            var products = await KolbenServiceUnit.ProductService.GetAll(p => p.Stocks);
            _localProducts = new List<VMProduct>(products.Select(p => new VMProduct(p)));
        }

        private async void SaveNewRecipeProductCommandExecute()
        {
            try
            {
                if (NewRecipeProduct == null || (NewRecipeProduct != null && NewRecipeProduct.Product == null))
                    throw new Exception("Une erreur s'est produite !");

                if (NewRecipeProduct.Id > 0)
                {
                    var recipeProdut = new RecipeProduct()
                    {
                        Id = NewRecipeProduct.Id,
                        IdRecipe = CurrentRecipe.Id,
                        IdProduct = NewRecipeProduct.Product.Id,
                        KgQuantity = NewRecipeProduct.KgQuantity.HasValue ? NewRecipeProduct.KgQuantity.Value : 0,
                        UnitQuantity = NewRecipeProduct.UnitQuantity.HasValue ? NewRecipeProduct.UnitQuantity.Value : 0,
                    };

                    await KolbenServiceUnit.RecipeProductService.Update(recipeProdut);           
                }
                else
                {
                    var targetRecipeProduct = CurrentRecipe.RecipeProducts.FirstOrDefault(rp => rp.IdProduct == NewRecipeProduct.Product.Id);
                    if (targetRecipeProduct != null)
                    {
                        targetRecipeProduct.KgQuantity += NewRecipeProduct.KgQuantity;
                        targetRecipeProduct.UnitQuantity += NewRecipeProduct.UnitQuantity;

                        await KolbenServiceUnit.RecipeProductService.Update(new RecipeProduct() {
                            Id = targetRecipeProduct.Id,
                            IdRecipe = CurrentRecipe.Id,
                            KgQuantity = targetRecipeProduct.KgQuantity.HasValue ? targetRecipeProduct.KgQuantity.Value : 0,
                            UnitQuantity = targetRecipeProduct.UnitQuantity.HasValue ? targetRecipeProduct.UnitQuantity.Value : 0,
                            IdProduct = targetRecipeProduct.Product.Id });
                    }
                    else
                    {
                        var recipeProdut = new RecipeProduct()
                        {
                            Id = NewRecipeProduct.Id,
                            IdRecipe = CurrentRecipe.Id,
                            IdProduct = NewRecipeProduct.Product.Id,
                            KgQuantity = NewRecipeProduct.KgQuantity.HasValue ? NewRecipeProduct.KgQuantity.Value : 0,
                            UnitQuantity = NewRecipeProduct.UnitQuantity.HasValue ? NewRecipeProduct.UnitQuantity.Value : 0,
                        };

                        NewRecipeProduct.Id = await KolbenServiceUnit.RecipeProductService.Add(recipeProdut);
                        NewRecipeProduct.ComputePrice();

                        CurrentRecipe.RecipeProducts.Add(NewRecipeProduct);
                    }
                }
                NewRecipeProduct.ComputePrice();
                CurrentRecipe.ComputeStats();

                ClearNewRecipeProductCommandExecute();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.Commands.Add(new UICommand("Ok"));
                messageDialog.ShowAsync();
            }
        }

        private void ClearNewRecipeProductCommandExecute()
        {
            NewRecipeProduct = new VMRecipeProduct();
        }

        private async void EditRecipeProductCommandExecute(object param)
        {
            var targetRecipeProduct = (VMRecipeProduct)param;

            NewRecipeProduct = targetRecipeProduct;
        }

        private async void DeleteRecipeProductCommandExecute(object param)
        {
            var targetRecipeProduct = (VMRecipeProduct)param;

            var messageDialog = new MessageDialog("Voulez-vous supprimer le produit de la recette ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.RecipeProductService.Delete(targetRecipeProduct.Id);
           var recipe = await KolbenServiceUnit.RecipesService.GetSingle(CurrentRecipe.Id);
           CurrentRecipe = new VMRecipe(recipe);
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }
    }
}
