using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Services
{
    public interface ISound
    {
        event Action<ISound> OnFinish;

        void Play(Action<ISound> callback = null);

        void Pause();

        void Stop();

        ISound After(ISound sound);

        ISound Then(Action<ISound> action);
    }
}
