using DrivingLightsSim.Services;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrivingLightsSim.UWP.Services.UWPNativeSound))]
namespace DrivingLightsSim.UWP.Services
{
    public class UWPNativeSound : IGenericNativeSound
    {
        private MediaPlayer player = null;

        private readonly List<ISound> sounds = new List<ISound>();

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            sounds.Add(sound);
            return this;
        }

        public void Dispose()
        {
            player?.Dispose();
        }

        public DeviceType GetDeviceType() => DeviceType.UWP;

        public void LoadFile(string path)
        {
            player?.Dispose();
            player = new MediaPlayer();
            player.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///{path}"));
        }

        public void Pause()
        {
            player?.Pause();
        }

        public void Play(Action<ISound> callback = null)
        {
            if (player == null)
            {
                return;
            }

            player.MediaEnded += (sender, e) =>
            {
                callback?.Invoke(this);
                OnFinish?.Invoke(this);

                PlaybackUtils.CascadePlayback(sounds, this);
            };

            Stop();
            player.Play();
        }

        public void Stop()
        {
            if (player == null)
            {
                return;
            }

            this.Pause();
            player.PlaybackSession.Position = TimeSpan.Zero;
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
