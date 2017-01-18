using Kolben.Utils;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kolben
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static AppShell Current = null;
        public Loader Loader { get; set; }
        public Frame AppFrame { get { return this.MainFrame; } }
   
        public AppShell()
        {
            this.InitializeComponent();
            Current = this;
            Loader = Loader.Instance;  
        }
    }
}
