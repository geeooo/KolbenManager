﻿using Kolben.Controller.Restaurant.NSPurchases;
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
    public sealed partial class PurchasesPage : Page
    {
        private PurchasesController _purchaseController;
        public PurchasesPage()
        {
            this.InitializeComponent();
            _purchaseController = new PurchasesController();
            DataContext = _purchaseController;
            _purchaseController.PropertyChanged += _purchaseController_PropertyChanged;
        }

        private void _purchaseController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPurchase")
            {
                var viewmodel = _purchaseController.CurrentPurchase;
                PurchaseFrame.Navigate(typeof(PurchaseDetailPage), viewmodel);
            }
        }
    }
}