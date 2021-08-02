using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrivingLightsSim.Themes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:标识符应具有正确的后缀", Justification = "<挂起>")]
    public partial class DarkTheme : ResourceDictionary
    {
        public DarkTheme()
        {
            InitializeComponent();
        }
    }
}