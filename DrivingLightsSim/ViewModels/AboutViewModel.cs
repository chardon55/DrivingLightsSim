using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrivingLightsSim.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "关于";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/chardon55/DrivingLightsSim/blob/main/LICENSE"));
        }

        public ICommand OpenWebCommand { get; }
    }
}