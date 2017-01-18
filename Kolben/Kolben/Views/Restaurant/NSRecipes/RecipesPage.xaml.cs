using Kolben.Controller.Restaurant.NSRecipes;
using Kolben.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSRecipes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipesPage : Page
    {
        private RecipesController _recipesController;

        public RecipesPage()
        {
            this.InitializeComponent();

            _recipesController = new RecipesController();
            _recipesController.PropertyChanged += _recipesController_PropertyChanged;

            DataContext = _recipesController;
        }

        private void _recipesController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentRecipe")
            {
                var viewmodel = _recipesController.CurrentRecipe;
                RecipeFrame.Navigate(typeof(RecipeDetailPage), viewmodel);
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //_recipesController.FilterProductList(sender.Text);
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var vmProduct = (VMRecipe)args.SelectedItem;
            sender.Text = vmProduct.Name;
        }
    }
}
