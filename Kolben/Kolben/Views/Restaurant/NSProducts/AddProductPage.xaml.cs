using Kolben.Controller.Restaurant.NSProducts;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddProductPage : Page
    {
        private AddProductController _addProductController;
        public AddProductPage()
        {
            this.InitializeComponent();

            _addProductController = new AddProductController();

            DataContext = _addProductController;
        }
    }
}
