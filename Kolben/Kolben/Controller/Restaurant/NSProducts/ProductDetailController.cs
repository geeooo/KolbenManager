using GalaSoft.MvvmLight.Command;
using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views;
using KolbenService;
using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.NSProducts
{
    public class ProductDetailController : BaseController
    {
        #region Attributes  
        private VMProduct _currentProduct;

        private List<VMTypeofProductCategory> _localTypeofProductCategories;
        private ObservableCollection<VMTypeofProductCategory> _typeofProductCategories;
        #endregion

        #region Getters / Setters
        public VMProduct CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                if (value != _currentProduct)
                {
                    _currentProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VMTypeofProductCategory> TypeofProductCategories
        {
            get { return _typeofProductCategories; }
            set
            {
                if (_typeofProductCategories != value)
                {
                    _typeofProductCategories = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public ProductDetailController(ref VMProduct vmProduct)
        {
            Init(vmProduct);
        }

        private async void Init(VMProduct vmProduct)
        {
            await base.Init();

            CurrentProduct = vmProduct;
            CurrentProduct.PropertyChanged += CurrentProduct_PropertyChanged;
        }

        private async void CurrentProduct_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentProduct = (VMProduct)sender;
            var product = new Product() { Id = currentProduct.Id, Name = currentProduct.Name, IdTypeofProductCategory = currentProduct.TypeofProductCategory.Id };
            await KolbenServiceUnit.ProductService.Update(product);
        }

        protected override void Display()
        {
            TypeofProductCategories = new ObservableCollection<VMTypeofProductCategory>(_localTypeofProductCategories.OrderBy(o => o.Name));
        }

        protected override void InitCommands()
        {

        }

        protected override void InitReferences()
        {
            
        }

        protected async override Task Search()
        {
            var typeofProductCategories = await KolbenServiceUnit.TypeofProductCategoryService.GetAll();
            _localTypeofProductCategories = typeofProductCategories.Select(topc => new VMTypeofProductCategory(topc)).ToList();
        }
    }
}
