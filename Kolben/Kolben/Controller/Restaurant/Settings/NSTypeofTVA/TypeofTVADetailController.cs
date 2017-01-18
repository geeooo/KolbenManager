using Kolben.ViewModels;
using KolbenService;
using KolbenService.Database.Entities.Typeof;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofTVA
{
    public class TypeofTVADetailController : BaseController
    {
        #region Attributes
        private VMTypeofTVA _typeofTVA;
        #endregion

        #region Getters / Setters
        public VMTypeofTVA TypeofTVA
        {
            get { return _typeofTVA; }
            set
            {
                if (_typeofTVA != value)
                {
                    _typeofTVA = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public TypeofTVADetailController(ref VMTypeofTVA typeofTVA)
        {
            Init(typeofTVA);
        }

        private async void Init(VMTypeofTVA typeofTVA)
        {
            await base.Init();

            TypeofTVA = typeofTVA;
            TypeofTVA.PropertyChanged += TypeofTVA_PropertyChanged;            
        }

        private async void TypeofTVA_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentTypeofTVA = (VMTypeofTVA)sender;
            var typeofTVA = new TypeofTVA() { Id = currentTypeofTVA.Id, Name = currentTypeofTVA.Name, Value = currentTypeofTVA.Value };
            await KolbenServiceUnit.TypeofTVAService.Update(typeofTVA);
        }

        protected override void Display()
        {
            
        }

        protected override void InitCommands()
        {
            
        }

        protected override async Task Search()
        {
            
        }
    }
}
