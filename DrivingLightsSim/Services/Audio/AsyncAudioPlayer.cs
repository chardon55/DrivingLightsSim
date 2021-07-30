using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLightsSim.Services.Audio
{
    public class AsyncAudioPlayer
    {
        private ISound sound;

        public AsyncAudioPlayer(ISound sound)
        {
            this.sound = sound;
        }

        public async Task PlayAsync()
        {
            await new Task(() =>
            {
                
            });
        }
    }
}
