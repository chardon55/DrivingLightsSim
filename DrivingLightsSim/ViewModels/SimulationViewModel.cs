using DrivingLightsSim.Models;
using DrivingLightsSim.Services.Audio;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLightsSim.ViewModels
{
    public class SimulationViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected new virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<string> displayTexts = new List<string>();

        public HashSet<LightCommand> lightCommands = new HashSet<LightCommand>();

        public DisplayInfo DisplayHeader { get; private set; }

        public DisplayInfo DisplayFooter { get; private set; }

        public List<DisplayInfo> InfoList { get; } = new List<DisplayInfo>();

        public List<LightCommand> ActiveCommands { get; private set; } = new List<LightCommand>();

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
        }

        public async Task PlayAsync()
        {
            var player = AsyncAudioPlayer.Instance;

            player.LoadSource(StartCommandAudioFile);
            player.AddInterrupt(3000);

            foreach (var item in ActiveCommands)
            {
                item.LoadAudio(append: true, interruptAfter: 5000);
            }

            player.LoadSource(EndCommandAudioFile, append: true);

            IsPlaying = true;
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
            IsPlaying = false;
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
            ActiveCommands.Clear();

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

                ActiveCommands.Add(item);
            }

            OnPropertyChanged("InfoList");
        }

        public SimulationViewModel()
        {
            Title = "模拟";
            Reset();
        }
    }
}
