using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrivingLightsSim.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimulationPage : ContentPage
    {
        public SimulationPage()
        {
            InitializeComponent();
        }

        private void playToggleButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void stopButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}