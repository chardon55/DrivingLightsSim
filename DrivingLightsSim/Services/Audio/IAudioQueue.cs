using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrivingLightsSim.Services.Audio
{
    public interface IAudioQueue
    {
        bool AutoPlayNext { get; set; }

        void Play();

        void Pause();

        void Stop();

        void Enqueue(ISound sound, uint delay = 0);

        ISound Dequeue();
    }

    public static class AudioQueue
    {
        public static IAudioQueue Get()
        {
            return DependencyService.Get<IAudioQueue>();
        }
    }
}
