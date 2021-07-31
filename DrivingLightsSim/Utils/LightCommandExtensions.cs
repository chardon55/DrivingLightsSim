using DrivingLightsSim.Models;
using DrivingLightsSim.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLightsSim.Utils
{
    public static class LightCommandExtensions
    {
        public static async Task PlayAudioAsync(this LightCommand @this)
        {
            @this.LoadAudio(append: false);
            await AsyncAudioPlayer.Instance.PlayAsync();
        }

        public static void LoadAudio(this LightCommand @this, bool append = false, int interruptAfter = 0, Action<LightCommand> callback = null)
        {
            var player = AsyncAudioPlayer.Instance;

            player.LoadSource(@this.AudioFile, append: append, callback: s => callback?.Invoke(@this));
            player.LoadSource("dong.mp3", append: true);

            if (interruptAfter > 0)
            {
                player.AddInterrupt(interruptAfter);
            }
        }
    }
}
