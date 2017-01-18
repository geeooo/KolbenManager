using Kolben.ViewModels;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofTVA
{
    public class TypeofTVAAddController : BaseController
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

        public TypeofTVAAddController()
        {
            TypeofTVA = new VMTypeofTVA();
        }


        protected override void Display()
        {
            return;
        }

        protected override void InitCommands()
        {
            return;
        }

        protected override async Task Search()
        {
            return;
        }
    }
}
