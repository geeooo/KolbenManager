using Kolben.ViewModels;
using KolbenService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Kolben.Controller.Restaurant.NSProducts
{
    public class AddProductController : BaseController
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

        public AddProductController()
        {
            CurrentProduct = new VMProduct();
            Init();
        }

        protected override async Task Search()
        {
            var typeofProductCategories = await KolbenServiceUnit.TypeofProductCategoryService.GetAll();
            _localTypeofProductCategories = typeofProductCategories.Select(topc => new VMTypeofProductCategory(topc)).ToList();
        }

        #region Inits
        protected override void InitCommands()
        {
            
        }

        protected override void InitReferences()
        {

        }

        protected override void Display()
        {
            TypeofProductCategories = new ObservableCollection<VMTypeofProductCategory>(_localTypeofProductCategories.OrderBy(o => o.Name));
        }
        #endregion

    }
}
