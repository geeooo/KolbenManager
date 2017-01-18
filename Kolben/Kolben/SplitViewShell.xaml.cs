using Kolben.Views;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplitViewShell : Page
    {
        public static SplitViewShell Current = null;
        public Frame AppFrame { get { return this.MainFrame; } }

        private List<NavigationMenuItem> _navigationMenuItem;

        public SplitViewShell()
        {
            this.InitializeComponent();

            Current = this;

            AppFrame.Navigated += AppFrame_Navigated;

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parametersArray = (object[])e.Parameter;

            Title.Text = parametersArray[0].ToString();
            _navigationMenuItem = (List<NavigationMenuItem>)parametersArray[1];
            NavMenuList.ItemsSource = _navigationMenuItem;
            NavMenuList.SelectedItem = NavMenuList.Items.FirstOrDefault();
        }

        private void AppFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            var menuItem = NavMenuList.Items.FirstOrDefault(i => ((NavigationMenuItem)i).DestPage == e.Content.GetType());
            if (menuItem != null)
            {
                NavMenuList.SelectedItem = menuItem;
            }
        }

        private void NavMenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var i in _navigationMenuItem)
            {
                i.IsSelected = false;
            }

            NavigationMenuItem menuItem = NavMenuList.SelectedItem as NavigationMenuItem;
            if (menuItem != null)
            {
                menuItem.IsSelected = true;
                AppFrame.Navigate(menuItem.DestPage);
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;

            if (AppFrame == null)
                return;


            // Check to see if this is the top-most page on the app back stack.
            if (AppFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                AppFrame.GoBack();
            }

            e.Handled = handled;
        }

        private void SettingsNavPaneButton_Click(object sender, RoutedEventArgs e)
        {
            NavMenuList.SelectedItem = null;
            AppFrame.Navigate(typeof(SettingsPage));
        }
    }
}
