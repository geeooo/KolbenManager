﻿using Kolben.Controller.Restaurant.Settings.NSTypeofProductCategory;
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

namespace Kolben.Views.Restaurant.Settings.NSTypeofProductCategory
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TypeofProductCategoryDetailPage : Page
    {
        private TypeofProductCategoryDetailController _typeofProductCategoryDetailController;
        public TypeofProductCategoryDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var vmTypeofProductCategory = (VMTypeofProductCategory)e.Parameter;
                _typeofProductCategoryDetailController = new TypeofProductCategoryDetailController(ref vmTypeofProductCategory);
            }

            DataContext = _typeofProductCategoryDetailController;
        }
    }
}
