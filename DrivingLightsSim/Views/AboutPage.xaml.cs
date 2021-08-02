using DrivingLightsSim.Services.Audio;
using System;
using System.ComponentModel;
using System.Net;
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

            Models.NewVersionInfo info;

            try
            {
                info = await aboutViewModel.GetReleaseInfoAsync();
            }
            catch (WebException)
            {
                await DisplayAlert("错误", "网络异常，请检查网络", "确定");
                return;
            }
            catch (Exception)
            {
                await DisplayAlert("错误", "未知异常", "确定");
                return;
            }

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