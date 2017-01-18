using Kolben.Controller.Restaurant;
using Kolben.Controller.Restaurant.NSProducts;
using Kolben.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsPage : Page
    {
        private ProductsController _productsController;

        public ProductsPage()
        {
            this.InitializeComponent();

            _productsController = new ProductsController();
            _productsController.PropertyChanged += _productsController_PropertyChanged;
            DataContext = _productsController;   
        }

        private void _productsController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentProduct")
            {
                var viewmodel = _productsController.CurrentProduct;
                ProductFrame.Navigate(typeof(ProductDetailPage), viewmodel);
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            _productsController.FilterProductList(sender.Text);
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var vmProduct = (VMProduct)args.SelectedItem;
            sender.Text = vmProduct.Name;
        }
    }
}
