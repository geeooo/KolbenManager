using Kolben.ViewModels;
using KolbenService;
using KolbenService.Database.Entities.Typeof;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofProductCategory
{
    public class TypeofProductCategoryDetailController : BaseController
    {
        #region Attributes
        private VMTypeofProductCategory _typeofProductCategory;
        #endregion

        #region Getters / Setters
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

        public TypeofProductCategoryDetailController(ref VMTypeofProductCategory typeofProductCategory)
        {     
            Init(typeofProductCategory);
        }

        private async void Init(VMTypeofProductCategory typeofProductCategory)
        {
            await base.Init();

            TypeofProductCategory = typeofProductCategory;
            TypeofProductCategory.PropertyChanged += TypeofProductCategory_PropertyChanged;
        }

        private async void TypeofProductCategory_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentTypeofProductCategory = (VMTypeofProductCategory)sender;
            var typeofProductCategory = new TypeofProductCategory() { Id = currentTypeofProductCategory.Id, Name = currentTypeofProductCategory.Name };
            await KolbenServiceUnit.TypeofProductCategoryService.Update(typeofProductCategory);
        }
    }
}
