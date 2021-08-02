using DrivingLightsSim.Services;
using DrivingLightsSim.Services.Audio;
using DrivingLightsSim.Themes;
using DrivingLightsSim.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrivingLightsSim
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<LightCommandDataStore>();
            MainPage = new AppShell();

            DLSThemeManager.Instance.AutoSetTheme();
        }

        protected override void OnStart()
        {
            DLSThemeManager.Instance.AutoSetTheme();
        }

        protected override void OnSleep()
        {
            AsyncAudioPlayer.Instance.Pause();
        }

        protected override void OnResume()
        {
            DLSThemeManager.Instance.AutoSetTheme();
        }
    }
}
