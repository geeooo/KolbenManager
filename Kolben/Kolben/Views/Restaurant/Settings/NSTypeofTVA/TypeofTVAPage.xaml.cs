using Kolben.Controller.Restaurant.Settings.NSTypeofTVA;
using Kolben.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.Settings.NSTypeofTVA
{
    public sealed partial class TypeofTVAPage : Page
    {
        private TypeofTVAController _typeofTVAController;

        public TypeofTVAPage()
        {
            this.InitializeComponent();

            _typeofTVAController = new TypeofTVAController();
            DataContext = _typeofTVAController;
            _typeofTVAController.PropertyChanged += _typeofTVAController_PropertyChanged;
        }

        private void _typeofTVAController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "TypeofTVA")
            {
                var viewmodel = _typeofTVAController.TypeofTVA;
                TypeofTVAFrame.Navigate(typeof(TypeofTVADetailPage), viewmodel);
            }
        }
    }
}
