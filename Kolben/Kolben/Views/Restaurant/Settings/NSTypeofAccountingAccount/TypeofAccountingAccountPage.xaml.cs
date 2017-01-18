using Kolben.Controller.Restaurant.Settings.NSTypeofAccountingAccount;
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

namespace Kolben.Views.Restaurant.Settings.NSTypeofAccountingAccount
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TypeofAccountingAccountPage : Page
    {
        private TypeofAccountingAccountController _typeofAccountingAccountController;
        public TypeofAccountingAccountPage()
        {
            this.InitializeComponent();
            _typeofAccountingAccountController = new TypeofAccountingAccountController();
            DataContext = _typeofAccountingAccountController;
            _typeofAccountingAccountController.PropertyChanged += _typeofAccountingAccountController_PropertyChanged;
        }

        private void _typeofAccountingAccountController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "TypeofAccountingAccount")
            {
                var viewmodel = _typeofAccountingAccountController.TypeofAccountingAccount;
                TypeofAccountingAccountFrame.Navigate(typeof(TypeofAccountingAccountDetailPage), viewmodel);
            }
        }
    }
}
