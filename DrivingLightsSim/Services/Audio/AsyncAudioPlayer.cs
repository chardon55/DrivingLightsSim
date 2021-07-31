using DrivingLightsSim.Models;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrivingLightsSim.Services.Audio
{
    public sealed class AsyncAudioPlayer
    {

        public static readonly AsyncAudioPlayer Instance = new AsyncAudioPlayer();

        public const string INTERRUPT_SINGAL = "##";

        public static string Interrupt(uint timeMilli) => $"{INTERRUPT_SINGAL}{timeMilli}";

        private IGenericNativeSound sound;

        private List<ComboSource> sourceList = new List<ComboSource>();

        public struct ComboSource
        {
            public string Source { get; set; }
            public Action<string> Callback { get; set; }
        }

        private int cursor = 0;

        private bool playStarted = false;

        private bool playing = false;

        private bool skip = false;

        public bool IsPlaying => playing;

        private AsyncAudioPlayer()
        {
            sound = DependencyService.Get<IGenericNativeSound>();
        }

        public void LoadSource(string source, bool append = false, Action<string> callback = null)
        {
            LoadSource(new string[] { source }, append);

            if (callback != null)
            {
                var lastItem = sourceList[^1];
                lastItem.Callback = callback;
                sourceList[^1] = lastItem;
            }
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
                sourceList.Add(new ComboSource { Source = source, Callback = null });
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

            skip = false;
            playStarted = playing = true;

            for (; cursor < sourceList.Count; cursor++)
            {
                if (skip)
                {
                    break;
                }

                var source = sourceList[cursor];

                if (source.Source.IsInterrupt(out int delay))
                {
                    source.Callback?.Invoke(source.Source);
                    await Task.Delay(delay);

                    if (skip)
                    {
                        break;
                    }

                    continue;
                }
                
                sound.LoadFile(source.Source);
                source.Callback?.Invoke(source.Source);

                
                await StartPlayingAsync();

                if (skip)
                {
                    break;
                }
            }

            playStarted = playing = false;
        }

        public void Pause()
        {
            skip = true;
            sound.Stop();
            if ((sourceList?.Count ?? -1) > 0)
            {
                sound.LoadFile(sourceList[cursor].Source);
            }

            playing = false;
        }

        public void Stop()
        {
            skip = true;
            sound.Stop();
            cursor = 0;
            if ((sourceList?.Count ?? -1) > 0)
            {
                sound.LoadFile(sourceList[cursor].Source);
            }

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
