using DrivingLightsSim.DrivingLightsSim.Services.Audio;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DrivingLightsSim.Services
{
    public sealed class TTSSound : ISound
    {
        public string Content { get; set; } = "";

        private CancellationTokenSource cts = null;

        public bool IsPlaying => !(cts?.IsCancellationRequested ?? true);

        private List<ISound> sounds = new List<ISound>();

        private Queue<Action<ISound>> actions = new Queue<Action<ISound>>();

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            if (sound != null)
            {
                sounds.Add(sound);
            }

            return this;
        }

        public void Pause()
        {

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Play(Action<ISound> callback = null)
        {
            Stop();

            cts = new CancellationTokenSource();
            TextToSpeech.SpeakAsync(Content, cancelToken: cts.Token).ContinueWith(t =>
            {
                callback?.Invoke(this);
                OnFinish?.Invoke(this);

                foreach (var action in actions)
                {
                    action.Invoke(this);
                }

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

                    callbacks[callbacks.Count - 1].Invoke(this);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            if (IsPlaying)
            {
                cts?.Cancel();
                cts?.Dispose();
                cts = null;
            }
        }

        public ISound Then(Action<ISound> action)
        {
            if (action != null)
            {
                actions.Enqueue(action);
            }

            return this;
        }

        public void ClearThen() => actions.Clear();

        public void ClearAfter() => sounds.Clear();
    }
}
