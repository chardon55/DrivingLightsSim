using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using static DrivingLightsSim.Services.Audio.HighLevelAudioQueue;

namespace DrivingLightsSim.Services.Audio
{
    public sealed class AsyncAudioPlayer
    {
        public static readonly AsyncAudioPlayer Instance = new AsyncAudioPlayer();

        public const string INTERRUPT_SINGAL = "##";

        public static string Interrupt(uint timeMilli) => $"{INTERRUPT_SINGAL}{timeMilli}";

        private IGenericNativeSound sound;

        private List<string> sourceList = new List<string>();

        private int cursor = 0;

        private bool playStarted = false;

        private bool playing = false;

        public bool IsPlaying => playing;

        private AsyncAudioPlayer()
        {
            sound = DependencyService.Get<IGenericNativeSound>();
        }

        public void LoadSource(string source, bool append = false)
        {
            LoadSource(new string[] { source }, append);
        }

        public void LoadSource(ICollection<string> sources, bool append = false)
        {
            LoadSource(sources.ToArray(), append);
        }

        public void LoadSource(string[] sources, bool append = false)
        {
            if (!append)
            {
                sourceList.Clear();
            }

            foreach (string source in sources)
            {
                sourceList.Add(source);
            }
        }

        private async Task StartPlayingAsync()
        {
            using var semaphore = new Semaphore(0, 1);

            await Task.Run(() =>
            {
                sound.Play(s =>
                {
                    _ = semaphore.Release();
                });

                _ = semaphore.WaitOne();
            });
        }

        public async Task PlayAsync()
        {
            if (playStarted && playing)
            {
                Stop();
            }

            if (!playStarted)
            {
                cursor = 0;
            }

            playStarted = playing = true;

            for (; cursor < sourceList.Count; cursor++)
{
                string source = sourceList[cursor];

                bool success = source.IsInterrupt(out int delay);

                if (success)
                {
                    await Task.Delay(delay);
                    continue;
                }

                sound.LoadFile(source);
                await StartPlayingAsync();
            }

            playStarted = playing = false;
        }

        public void Pause()
        {
            sound.Pause();
            playing = false;
        }

        public void Stop()
        {
            sound.Stop();
            playStarted = playing = false;
        }
    }

    internal static class AsyncAudioPlayerExtensions
    {
        public static bool IsInterrupt(this string source, out int interruptMilli)
        {
            bool isInter = source.StartsWith(AsyncAudioPlayer.INTERRUPT_SINGAL);

            if (isInter)
            {
                interruptMilli = int.Parse(source.Replace(AsyncAudioPlayer.INTERRUPT_SINGAL, ""));
            }
            else
            {
                interruptMilli = -1;
            }

            return isInter;
        }
    }
}
