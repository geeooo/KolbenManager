using Kolben.Controller.Restaurant.Settings.NSTypeofTVA;
using Kolben.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.Settings.NSTypeofTVA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TypeofTVADetailPage : Page
    {
        private TypeofTVADetailController _typeofTVADetailController;
        public TypeofTVADetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var vmTypeofTVA = (VMTypeofTVA)e.Parameter;
                _typeofTVADetailController = new TypeofTVADetailController(ref vmTypeofTVA);
            }

            DataContext = _typeofTVADetailController;
        }
    }
}
