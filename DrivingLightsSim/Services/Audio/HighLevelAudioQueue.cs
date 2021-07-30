using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrivingLightsSim.Services.Audio.HighLevelAudioQueue))]
namespace DrivingLightsSim.Services.Audio
{
    public class HighLevelAudioQueue : IAudioQueue
    {
        private Queue<ISound> queue = new Queue<ISound>();

        protected internal class Delay : ISound
        {
            private int timeMilli;

            private List<ISound> sounds = new List<ISound>();

            public event Action<ISound> OnFinish;

            public Delay(int timeMilli)
            {
                this.timeMilli = timeMilli;
            }

            public ISound After(ISound sound)
            {
                throw new NotImplementedException();
            }

            public void Pause()
            {
                throw new NotImplementedException();
            }

            public void Play(Action<ISound> callback = null, Action<ISound> before = null)
            {
                before?.Invoke(this);
                if (timeMilli > 0)
                {
                    Thread.Sleep(timeMilli);
                }

                callback?.Invoke(this);
                OnFinish?.Invoke(this);
                PlaybackUtils.CascadePlayback(sounds, this);
            }

            public void Stop()
            {
                
            }

            public ISound Then(Action<ISound> action) => this;
        }

        public bool AutoPlayNext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public HighLevelAudioQueue()
        {

        }

        public ISound Dequeue()
        {
            throw new NotImplementedException();
        }

        public void Enqueue(ISound sound, uint delay = 0)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
