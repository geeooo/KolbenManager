using Kolben.Controller.Restaurant.NSRecipes;
using Kolben.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSRecipes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeDetailPage : Page
    {
        private RecipeDetailController _recipeDetailController;
        public RecipeDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var vmRecipe = (VMRecipe)e.Parameter;
                _recipeDetailController = new RecipeDetailController(ref vmRecipe);
            }

            DataContext = _recipeDetailController;
        }

        private void ProductSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var product = args.SelectedItem as VMProduct;
            _recipeDetailController.NewRecipeProduct.Product = product;
        }

        private void ProductSuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var autoSuggestBox = (AutoSuggestBox)sender;
            var text = autoSuggestBox.Text.ToLower();

            var targetProduct = _recipeDetailController.Products.FirstOrDefault(p => p.Name.ToLower().Equals(text));
            _recipeDetailController.NewRecipeProduct.Product = targetProduct;
        }
    }
}
