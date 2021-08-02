using DrivingLightsSim.Models;
using DrivingLightsSim.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrivingLightsSim.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public DevicePlatform Platform = DeviceInfo.Platform;

        public AboutViewModel()
        {
            Title = "关于";
            OpenWebCommand = new Command(async () =>
            {
                await Browser.OpenAsync("https://github.com/chardon55/DrivingLightsSim/blob/main/LICENSE");
            });
            OpenReleaseCommand = new Command(async () =>
            {
                await Browser.OpenAsync("https://github.com/chardon55/DrivingLightsSim/releases");
            });
        }

        public ICommand OpenWebCommand { get; }

        public ICommand OpenReleaseCommand { get; }

        public ICommand CheckUpdates { get; }

        public async Task<NewVersionInfo> GetReleaseInfoAsync()
        {
            return await UpdateChecker.CheckCurrent();
        }
    }
}