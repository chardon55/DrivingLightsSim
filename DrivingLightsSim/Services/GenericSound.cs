using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Services
{
    public class GenericSound : ISound
    {
        public string Url { get; protected set; }

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            throw new NotImplementedException();
        }

        public void Play(Action<ISound> callback = null)
        {
            throw new NotImplementedException();
        }

        public void Pause()
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

        public GenericSound(string url)
        {
            Url = url;
        }
    }
}
