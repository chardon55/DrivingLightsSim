using DrivingLightsSim.Services.Audio;
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
                else if (e.PropertyName.Equals("InfoList"))
                {
                    UpdateDisplayList();
                }
            };

            displayList.RefreshCommand = new Command(() =>
            {
                simulationViewModel.Reset();
                UpdateDisplayList();
                displayList.EndRefresh();
            });
        }

        private void UpdateDisplayList()
        {
            displayList.ItemsSource = null;
            displayList.ItemsSource = simulationViewModel.InfoList;
        }

        private void PlayToggleButton_Clicked(object sender, EventArgs e)
        {
            simulationViewModel.PlayToggle();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            simulationViewModel.Stop();
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            displayList.BeginRefresh();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (simulationViewModel.IsPlaying)
            {
                simulationViewModel.Pause();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateIconSource();
        }
    }
}