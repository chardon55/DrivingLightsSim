//using NAudio.Wave;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DrivingLightsSim.Services
//{
//    public class NAudioWrapperSound : ISound
//    {
//        public string Url { get; protected set; }

//        public event Action<ISound> OnFinish;

//        private Mp3FileReader reader = null;
//        private WaveOutEvent waveOut = new WaveOutEvent();

//        private List<ISound> sounds = new List<ISound>();

//        public ISound After(ISound sound)
//        {
//            sounds.Add(sound);
//            return this;
//        }

//        public void Play(Action<ISound> callback = null)
//        {
//            waveOut.PlaybackStopped += (s, e) =>
//            {
//                callback?.Invoke(this);
//                OnFinish?.Invoke(this);

//                if (sounds.Count > 0)
//                {
//                    var callbacks = new List<Action<ISound>>();

//                    for (int i = sounds.Count - 1; i >= 0; i--)
//                    {
//                        if (i == sounds.Count - 1)
//                        {
//                            callbacks.Add(ss =>
//                            {
//                                sounds[i].Play();
//                            });
//                        }
//                        else
//                        {
//                            var index = callbacks.Count - 1;

//                            callbacks.Add(ss =>
//                            {
//                                sounds[i].Play(callback: callbacks[index]);
//                            });
//                        }
//                    }

//                    callbacks[callbacks.Count - 1].Invoke(this);
//                }
//            };

//            waveOut.Play();
//        }

//        public void Pause()
//        {
//            waveOut.Pause();
//        }

//        public void Stop()
//        {
//            waveOut.Stop();
//            //waveOut.Dispose();
//            //Init();
//        }

//        public ISound Then(Action<ISound> action)
//        {
//            OnFinish += s => action.Invoke(s);
//            return this;
//        }

//        private void Init()
//        {
//            reader = new Mp3FileReader(Url);
//            waveOut.Init(reader);
//        }

//        public NAudioWrapperSound(string url)
//        {
//            Url = url;
//            Init();
//        }
//    }
//}
