using Kolben.Controller.Restaurant.NSAccountingAccount;
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

namespace Kolben.Views.Restaurant.NSAccountingAccount
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountingAccountPage : Page
    {
        private AccountingAccountController _accountingAccountController;
        public AccountingAccountPage()
        {
            this.InitializeComponent();
            _accountingAccountController = new AccountingAccountController();
            DataContext = _accountingAccountController;
            _accountingAccountController.PropertyChanged += _accountingAccountController_PropertyChanged;
        }

        private void _accountingAccountController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "AccountingAccount")
            {
                var viewmodel = _accountingAccountController.AccountingAccount;
                AccountingAccountFrame.Navigate(typeof(AccountingAccountDetailPage), viewmodel);
            }
        }
    }
}
