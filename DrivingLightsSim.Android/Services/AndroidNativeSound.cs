using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DrivingLightsSim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrivingLightsSim.Droid.Services
{
    public class AndroidNativeSound : IGenericNativeSound
    {
        private MediaPlayer player = null;

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            throw new NotImplementedException();
        }

        public DeviceType GetDeviceType()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(Action<ISound> callback = null)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public ISound Then(Action<ISound> action)
        {
            throw new NotImplementedException();
        }
    }
}