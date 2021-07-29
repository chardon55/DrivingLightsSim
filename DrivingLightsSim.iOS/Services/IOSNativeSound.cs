﻿using AVFoundation;
using DrivingLightsSim.Services;
using DrivingLightsSim.Utils;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrivingLightsSim.iOS.Services.IOSNativeSound))]
namespace DrivingLightsSim.iOS.Services
{
    public class IOSNativeSound : IGenericNativeSound
    {
        private AVAudioPlayer audioPlayer = null;

        private List<ISound> sounds = new List<ISound>();

        public event Action<ISound> OnFinish;

        public ISound After(ISound sound)
        {
            sounds.Add(sound);
            return this;
        }

        public void Dispose()
        {
            audioPlayer?.Dispose();
        }

        public DeviceType GetDeviceType() => DeviceType.IOS;

        public void LoadFile(string path)
        {
            audioPlayer?.Dispose();
            audioPlayer = new AVAudioPlayer(new NSUrl(path), "mp3", out var error);
            audioPlayer.PrepareToPlay();
        }

        public void Pause()
        {
            audioPlayer?.Pause();
        }

        public void Play(Action<ISound> callback = null)
        {
            if (audioPlayer == null)
            {
                return;
            }

            audioPlayer.FinishedPlaying += (sender, e) =>
            {
                callback?.Invoke(this);
                OnFinish?.Invoke(this);

                PlaybackUtils.CascadePlayback(sounds, this);
            };

            audioPlayer.Play();
        }

        public void Stop()
        {
            audioPlayer?.Stop();
        }

        public ISound Then(Action<ISound> action)
        {
            if (action != null)
            {
                OnFinish += s => action.Invoke(s);
            }

            return this;
        }
    }
}