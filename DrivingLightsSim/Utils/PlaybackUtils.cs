using DrivingLightsSim.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Utils
{
    public static class PlaybackUtils
    {
        public static void CascadePlayback(IList<ISound> sounds, ISound owner)
        {
            if (sounds.Count > 0)
            {
                var callbacks = new List<Action<ISound>>();

                for (int i = sounds.Count - 1; i >= 0; i--)
                {
                    if (i == sounds.Count - 1)
                    {
                        callbacks.Add(s =>
                        {
                            sounds[i].Play();
                        });
                    }
                    else
                    {
                        var index = callbacks.Count - 1;

                        callbacks.Add(s =>
                        {
                            sounds[i].Play(callback: callbacks[index]);
                        });
                    }
                }

                callbacks[callbacks.Count - 1].Invoke(owner);
            }
        }
    }
}
