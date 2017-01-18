using Kolben.Controller.Restaurant.NSRecipes;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSRecipes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRecipePage : Page
    {
        private AddRecipeController _addRecipeController;
        public AddRecipePage()
        {
            this.InitializeComponent();

            _addRecipeController = new AddRecipeController();
            DataContext = _addRecipeController;
        }
    }
}
