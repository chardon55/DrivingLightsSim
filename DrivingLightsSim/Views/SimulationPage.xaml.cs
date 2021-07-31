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
        private void UpdateIconSource()
        {
            PlayToggleButton.Source = simulationViewModel.IsPlaying ? DrivingLightsSim.Resources.Strings.PauseSource : (ImageSource)DrivingLightsSim.Resources.Strings.PlaySource;
        }

        public SimulationPage()
        {
            InitializeComponent();

            simulationViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsPlaying"))
                {
                    UpdateIconSource();
                }
            };
        }

        private void PlayToggleButton_Clicked(object sender, EventArgs e)
        {
            simulationViewModel.PlayToggle();
            UpdateIconSource();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            simulationViewModel.Stop();
            UpdateIconSource();
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            simulationViewModel.Reset();
            UpdateIconSource();

            displayList.ItemsSource = null;
            displayList.ItemsSource = simulationViewModel.InfoList;
        }
    }
}