using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DrivingLightsSim.Services.Audio;
using DrivingLightsSim.Services;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrivingLightsSim.Droid.Services.Audio.AndroidNativeSound))]
namespace DrivingLightsSim.Droid.Services.Audio
{
    public class AndroidNativeSound : IGenericNativeSound
    {
        private MediaPlayer player = null;

        private readonly List<ISound> sounds = new List<ISound>();

        private bool paused = false;

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            sounds.Add(sound);
            return this;
        }

        public void Dispose()
        {
            player.Release();
        }

        public DeviceType GetDeviceType() => DeviceType.ANDROID;

        public void LoadFile(string path)
        {
            player?.Release();
            player = new MediaPlayer();

            var _path = path.Split(".")[0];
            var prop = typeof(Resource.Raw).GetField(_path);
            var descriptor = MainActivity.Current.ApplicationContext.Resources.OpenRawResourceFd((int)prop.GetValue(null));
            player.SetDataSource(descriptor);
            player.Prepare();
        }

        public void Pause()
        {
            player?.Pause();
            paused = true;
        }

        public void Play(Action<ISound> callback = null, Action<ISound> before = null)
        {
            if (player == null)
            {
                return;
            }

            player.Completion += (sender, e) =>
            {
                callback?.Invoke(this);
                OnFinish?.Invoke(this);

                PlaybackUtils.CascadePlayback(sounds, this);
            };

            before?.Invoke(this);
            player.Start();
            paused = false;
        }

        public void Stop()
        {
            player?.Stop();
        }

        public ISound Then(Action<ISound> action)
        {
            if (action != null)
            {
                OnFinish += s => action.Invoke(s);
            }
            return this;
        }
    }
}