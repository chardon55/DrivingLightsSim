using DrivingLightsSim.Models;
using DrivingLightsSim.Services.Audio;
using DrivingLightsSim.Themes;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrivingLightsSim.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        public List<string> displayTexts = new List<string>();

        public HashSet<LightCommand> lightCommands = new HashSet<LightCommand>();

        public DisplayInfo DisplayHeader { get; private set; }

        public DisplayInfo DisplayFooter { get; private set; }

        public List<DisplayInfo> InfoList { get; } = new List<DisplayInfo>();

        public List<LightCommand> CurrentCommands { get; private set; } = new List<LightCommand>();

        public bool IsPlaying { get; private set; } = false;

        public enum Status
        {
            IDLE, ACTIVE, SELECTED, OTHER,
        }

        public struct DisplayInfo
        {
            public string Title { get; set; }
            public string DisplayTitle { get; set; }

            public string Answer { get; set; }
            public string DisplayAnswer { get; set; }

            public Status Status { get; set; }

            private ResourceDictionary GetResourceDictionary()
            {
                return DLSThemeManager.Instance.GetResourceDictionary();
            }

            private T Get<T>(string key)
            {
                return (T)GetResourceDictionary()[key];
            }

            public Style StyleS => Status == Status.ACTIVE ? Get<Style>("SelectedS") : Get<Style>("UnselectedS");

            public Style StyleL => Status == Status.ACTIVE ? Get<Style>("SelectedL") : Get<Style>("UnselectedL");
        }

        private Dictionary<int, int> cursorMap = new Dictionary<int, int>();

        private void ResetStatus()
        {
            for (int i = 0; i < InfoList.Count; i++)
            {
                var item = InfoList[i];
                item.Status = Status.IDLE;
                InfoList[i] = item;
            }
            OnPropertyChanged("InfoList");
        }

        public async Task PlayAsync()
        {
            var player = AsyncAudioPlayer.Instance;

            cursorMap.Clear();

            player.LoadSource(StartCommandAudioFile);
            player.AddInterrupt(3000);

            int cursor = 2;

            for (int i = 0; i < CurrentCommands.Count; i++)
            {
                LightCommand item = CurrentCommands[i];
                item.LoadAudio(append: true, interruptAfter: 5000, callback: lc =>
                {
                    ResetStatus();

                    var index = InfoList.FindIndex(di => di.Title.Equals(lc.Content));

                    var item = InfoList[index];
                    item.Status = Status.ACTIVE;
                    InfoList[index] = item;
                    OnPropertyChanged("InfoList");
                });

                cursorMap.Add(cursor, i);
                cursor += 2;
            }

            player.LoadSource(EndCommandAudioFile, append: true, callback: s =>
            {
                ResetStatus();
            });

            IsPlaying = true;
            OnPropertyChanged("IsPlaying");
            await player.PlayAsync();
            IsPlaying = false;
            OnPropertyChanged("IsPlaying");
        }

        public void Play()
        {
            _ = PlayAsync();
        }

        public void Pause()
        {
            AsyncAudioPlayer.Instance.Pause();
            IsPlaying = false;
            OnPropertyChanged("IsPlaying");
        }

        public void PlayToggle()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        public void Stop()
        {
            AsyncAudioPlayer.Instance.Stop();
            ResetStatus();
            IsPlaying = false;
            OnPropertyChanged("IsPlaying");
        }

        private bool answerStatus = false;

        public void ViewAnswer()
        {

            answerStatus = true;
        }

        public void HideAnswer()
        {

            answerStatus = false;
        }

        public void ToggleAnswer(out bool status)
        {
            if (answerStatus)
            {
                HideAnswer();
            }
            else
            {
                ViewAnswer();
            }

            status = answerStatus;
        }

        private Random random = new Random();

        private int[] RandomIndices(int length, ICollection<int> finalOnly = null)
        {
            var indexSet = new HashSet<int>();

            while (indexSet.Count < length)
            {
                var value = random.Next(0, CommandList.Count);

                if (indexSet.Count < length - 1 && (finalOnly?.Contains(value) ?? false))
                {
                    continue;
                }

                _ = indexSet.Add(value);
            }

            return indexSet.ToArray();
        }

        public void Reset()
        {
            Stop();

            DisplayHeader = new DisplayInfo
            {
                Title = StartCommand,
                Answer = "",
                Status = Status.IDLE,
            };

            DisplayFooter = new DisplayInfo
            {
                Title = EndCommand,
                Answer = "",
                Status = Status.IDLE,
            };

            InfoList.Clear();
            CurrentCommands.Clear();

            var indices = RandomIndices(5, new List<int>(new int[] { CommandList.Count - 1 }));

            foreach (var index in indices)
            {
                var item = CommandList[index];
                InfoList.Add(new DisplayInfo
                {
                    Title = item.Content,
                    DisplayTitle = "",
                    Answer = item.Answer.GetDescription(),
                    Status = Status.IDLE,
                    DisplayAnswer = Resources.Strings.ShowAnswer,
                });

                CurrentCommands.Add(item);
            }

            OnPropertyChanged("InfoList");
            OnPropertyChanged("CurrentCommands");
        }

        public SimulationViewModel()
        {
            Title = "模拟";
            Reset();
        }
    }
}
