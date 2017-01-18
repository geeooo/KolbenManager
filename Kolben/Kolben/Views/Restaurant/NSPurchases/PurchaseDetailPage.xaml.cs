using Kolben.Controller.Restaurant.NSPurchases;
using Kolben.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.NSPurchases
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseDetailPage : Page
    {
        private PurchaseDetailController _purchaseDetailController;
        public PurchaseDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var viewModel = (VMPurchase)e.Parameter;
                _purchaseDetailController = new PurchaseDetailController(ref viewModel);
            }
            DataContext = _purchaseDetailController;
        }

        private void ProductSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var product = args.SelectedItem as VMProduct;
            _purchaseDetailController.NewPurchaseDetail.Product = product;
        }

        private void ProductSuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var autoSuggestBox = (AutoSuggestBox)sender;
            var text = autoSuggestBox.Text.ToLower();

            var targetProduct = _purchaseDetailController.Products.FirstOrDefault(p => p.Name.ToLower().Equals(text));
            _purchaseDetailController.NewPurchaseDetail.Product = targetProduct;
        }
    }
}
