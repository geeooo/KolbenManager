using Kolben.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofProductCategory
{
    public class TypeofProductCategoryAddController : BaseController
    {
        #region Attributes
        private List<VMTypeofProductCategory> _localTypeofProductCategory;
        private ObservableCollection<VMTypeofProductCategory> _typeofProductCategories;
        private VMTypeofProductCategory _typeofProductCategory;
        #endregion

        #region Getters / Setters
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
        public VMTypeofProductCategory TypeofProductCategory
        {
            get { return _typeofProductCategory; }
            set
            {
                if (_typeofProductCategory != value)
                {
                    _typeofProductCategory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public TypeofProductCategoryAddController()
        {
            TypeofProductCategory = new VMTypeofProductCategory();

            Init();
        }
    }
}
