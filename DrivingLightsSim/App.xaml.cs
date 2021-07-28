﻿using DrivingLightsSim.Services;
using DrivingLightsSim.Views;
using System;
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
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}