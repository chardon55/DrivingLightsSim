using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrivingLightsSim.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public bool NewVersionDetected => downloadUrl != null;

        private string downloadUrl = null;

        private async void CheckUpdateButton_Clicked(object sender, EventArgs e)
        {
            if (NewVersionDetected)
            {
                await Browser.OpenAsync(downloadUrl);
                return;
            }

            var info = await aboutViewModel.GetReleaseInfoAsync("GITEE");

            if (info == null)
            {
                await DisplayAlert("通知", "当前是最新版本", "确定");
                downloadUrl = null;
                return;
            }

            downloadUrl = info.Url;
            CheckUpdateButton.Text = "下载新版本";
            CheckUpdateButton.BackgroundColor = Color.FromHex("#C62828");
            await DisplayAlert("通知", "发现新版本", "确定");
        }
    }
}