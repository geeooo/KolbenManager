using Kolben.Controller.Restaurant.Settings.NSTypeofTVA;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kolben.Views.Restaurant.Settings.NSTypeofTVA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TypeofTVAAddPage : Page
    {
        private TypeofTVAAddController _typeofTVAAddController;
        public TypeofTVAAddPage()
        {
            this.InitializeComponent();
            _typeofTVAAddController = new TypeofTVAAddController();
            DataContext = _typeofTVAAddController;
        }
    }
}
