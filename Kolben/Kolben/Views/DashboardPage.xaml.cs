using Kolben.Views.Restaurant.NSPurchases;
using Kolben.Views.Restaurant.NSProducts;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Kolben.Views.Restaurant.NSAccountingAccount;
using Kolben.Views.Restaurant.NSRecipes;
using Kolben.Views.Restaurant.NSCashRegister;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            this.InitializeComponent();
        }

        private void FarmButton_Click(object sender, RoutedEventArgs e)
        {
            var navList = new List<NavigationMenuItem>(
            new[]
            {
               new NavigationMenuItem()
                {
                    Symbol = Symbol.Home,
                    Label = "Accueil",
                    DestPage = typeof(DashboardPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Shop,
                    Label = "Achats",
                    DestPage = typeof(PurchasesPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Calculator,
                    Label = "Produits",
                    DestPage = typeof(ProductsPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Page,
                    Label = "Ventes",
                    DestPage = typeof(RecipesPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Manage,
                    Label = "Comptes",
                    DestPage = typeof(AccountingAccountPage)
                }
    });

            AppShell.Current.AppFrame.Navigate(typeof(SplitViewShell), new object[] { "Ferme", navList });
        }

        private void RestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            var navList = new List<NavigationMenuItem>(
    new[]
    {
               new NavigationMenuItem()
                {
                    Symbol = Symbol.Home,
                    Label = "Accueil",
                    DestPage = typeof(DashboardPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Shop,
                    Label = "Achats",
                    DestPage = typeof(PurchasesPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Calculator,
                    Label = "Produits",
                    DestPage = typeof(ProductsPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Page,
                    Label = "Recettes",
                    DestPage = typeof(RecipesPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Calculator,
                    Label = "Caisse",
                    DestPage = typeof(AddConsomationPage)
                },
                new NavigationMenuItem()
                {
                    Symbol = Symbol.Manage,
                    Label = "Comptes",
                    DestPage = typeof(AccountingAccountPage)
                }
    });

            AppShell.Current.AppFrame.Navigate(typeof(SplitViewShell), new object[] { "Crapahûte", navList });
        }
    }
}
