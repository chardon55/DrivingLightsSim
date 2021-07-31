using DrivingLightsSim.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Utils
{
    public static class AsyncAudioPlayerExtensions
    {
        public static void AddInterrupt(this AsyncAudioPlayer @this, int milliseconds)
        {
            if (milliseconds <= 0)
            {
                return;
            }

            @this.LoadSource(AsyncAudioPlayer.Interrupt((uint)milliseconds), append: true);
        }
    }
}
