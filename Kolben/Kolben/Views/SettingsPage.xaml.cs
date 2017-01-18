using Kolben.ViewModels;
using Kolben.Views.Restaurant.Settings.NSTypeofAccountingAccount;
using Kolben.Views.Restaurant.Settings.NSTypeofProductCategory;
using Kolben.Views.Restaurant.Settings.NSTypeofTVA;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            SettingList.ItemsSource = new List<VMSettingMenuItem>()
            {
                new VMSettingMenuItem()
                {
                    Name = "TVA",
                    TargetPage = typeof(TypeofTVAPage)
                },
                new VMSettingMenuItem()
                {
                    Name = "Catégorie de produit",
                    TargetPage = typeof(TypeofProductCategoryPage)
                },
                new VMSettingMenuItem()
                {
                    Name = "Type de compte",
                    TargetPage = typeof(TypeofAccountingAccountPage)
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AppShell.Current.AppFrame.BackStack.Clear();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void SettingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vmSettingMenuItem = SettingList.SelectedItem as VMSettingMenuItem;
            if (vmSettingMenuItem != null)
            {
                SettingFrame.Navigate(vmSettingMenuItem.TargetPage);
            }
        }
    }
}
