using Kolben.Controller.Restaurant;
using Kolben.Controller.Restaurant.NSProducts;
using Kolben.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductDetailPage : Page
    {
        private ProductDetailController _productController;

        public ProductDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var vmProduct = (VMProduct)e.Parameter;
                _productController = new ProductDetailController(ref vmProduct);
            }

            DataContext = _productController;
        }        
    }
}
