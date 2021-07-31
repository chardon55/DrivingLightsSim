using DrivingLightsSim.Models;
using DrivingLightsSim.Services;
using DrivingLightsSim.Services.Audio;
using DrivingLightsSim.Utils;
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
    public partial class BrowsePage : ContentPage
    {
        struct DisplayItem
        {
            public string Content { get; set; }
            public string Answer { get; set; }
        }

        private readonly List<DisplayItem> displayItemList = new List<DisplayItem>();  

        public BrowsePage()
        {
            InitializeComponent();

            foreach (LightCommand item in browseViewModel.CommandList)
            {
                displayItemList.Add(new DisplayItem { Content = item.Content, Answer = item.Answer.GetDescription() });
            }

            BrowseView.ItemsSource = displayItemList;
        }

        private async void BrowseView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await browseViewModel.CommandList[e.ItemIndex].PlayAudioAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AsyncAudioPlayer.Instance.Pause();
        }
    }
}