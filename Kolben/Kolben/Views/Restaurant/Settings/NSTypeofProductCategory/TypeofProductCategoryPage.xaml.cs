using Kolben.Controller.Restaurant.Settings.NSTypeofProductCategory;
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
    public sealed partial class TypeofProductCategoryPage : Page
    {
        private TypeofProductCategoryController _typeofProductCategoryController;
        public TypeofProductCategoryPage()
        {
            this.InitializeComponent();
            _typeofProductCategoryController = new TypeofProductCategoryController();
            DataContext = _typeofProductCategoryController;
            _typeofProductCategoryController.PropertyChanged += _typeofProductCategoryController_PropertyChanged;
        }

        private void _typeofProductCategoryController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "TypeofProductCategory")
            {
                var viewModel = _typeofProductCategoryController.TypeofProductCategory;
                TypeofProductCategoryFrame.Navigate(typeof(TypeofProductCategoryDetailPage), viewModel);
            }
        }       
    }
}
