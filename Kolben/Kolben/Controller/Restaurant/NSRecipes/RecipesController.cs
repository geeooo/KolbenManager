using GalaSoft.MvvmLight.Command;
using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.NSRecipes;
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

namespace Kolben.Controller.Restaurant.NSRecipes
{
    public class RecipesController : BaseController
    {
        #region Attributes
        private List<VMRecipe> _localRecipes;
        private ObservableCollection<VMRecipe> _recipes;
        private VMRecipe _currentRecipe;

        private ActionData _addNewRecipeActionData;
        private ActionData _deleteRecipeActionData;
        #endregion

        #region Getters / Setters
        public ObservableCollection<VMRecipe> Recipes
        {
            get { return _recipes; }
            set
            {
                if (_recipes != value)
                {
                    _recipes = value;
                    OnPropertyChanged();
                }
            }
        }
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
        #endregion

        public RecipesController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();
        }

        protected override void Display()
        {
            Recipes = new ObservableCollection<VMRecipe>(_localRecipes);
            if (_recipes.Any())
            {
                CurrentRecipe = _recipes.FirstOrDefault();
            }
        }

        protected override void InitCommands()
        {
            _addNewRecipeActionData = new ActionData("Nouvelle recette", Symbol.Add, new RelayCommand<object>(o => AddNewRecipeCommandExecute()));
            _deleteRecipeActionData = new ActionData("Supprimer la recette", Symbol.Delete, new RelayCommand<object>(o => DeleteRecipeCommandExecute(), o => CurrentRecipeCanExecute()));

            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewRecipeActionData
                },
                new ObservableCollection<ActionData>()
                {
                    _deleteRecipeActionData
                }
            };
        }

        protected async override Task Search()
        {
            var recipes = await KolbenServiceUnit.RecipesService.GetAll(r => r.RecipeProducts);
            _localRecipes = recipes.Select(r => new VMRecipe(r)).ToList();       
        }

        #region Commands
        private async void AddNewRecipeCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'une nouvelle recette",
                Content = new AddRecipePage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmRecipe = ((AddRecipeController)((AddRecipePage)dialog.Content).DataContext).CurrentRecipe;
                var idRecipe = await KolbenServiceUnit.RecipesService.Add(new Recipe() { Name = vmRecipe.Name, RecipeCategory = vmRecipe.RecipeCategory });
                var recipe = await KolbenServiceUnit.RecipesService.GetSingle(idRecipe);

                var newVmRecipe = new VMRecipe(recipe);
                _localRecipes.ToList().Add(newVmRecipe);
                Recipes.Add(newVmRecipe);
                Recipes = new ObservableCollection<VMRecipe>(Recipes.OrderBy(p => p.Name));
                CurrentRecipe = newVmRecipe;
            }
        }

        private void DeleteRecipeCommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer la recette ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.RecipesService.Delete(CurrentRecipe.Id);
           await Search();
           Display();
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }

        private bool CurrentRecipeCanExecute()
        {
            return CurrentRecipe != null && CurrentRecipe.Id != 0;
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "CurrentRecipe")
            {
                _deleteRecipeActionData.Command.RaiseCanExecuteChanged();
            }
        }
    }
}
