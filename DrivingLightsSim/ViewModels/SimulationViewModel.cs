using DrivingLightsSim.Models;
using DrivingLightsSim.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        public List<string> displayTexts = new List<string>();

        public HashSet<LightCommand> lightCommands = new HashSet<LightCommand>();

        public DisplayInfo DisplayHeader { get; private set; }

        public DisplayInfo DisplayFooter { get; private set; }

        public List<DisplayInfo> InfoList { get; private set; } = new List<DisplayInfo>();

        public List<string> AudioFiles { get; private set; } = new List<string>();

        public enum Status
        {
            IDLE, ACTIVE, SELECTED, OTHER,
        }

        public struct DisplayInfo
        {
            public string Title { get; set; }
            public string Answer { get; set; }
            public Status Status { get; set; }
        }

        public void Play()
        {

        }

        public void Pause()
        {

        }

        public void Stop()
        {

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

        private static int[] RandomIndices(int length, ICollection<int> finalOnly = null)
        {
            var random = new Random();
            var indexSet = new HashSet<int>();

            while (indexSet.Count < length)
            {
                var value = random.Next(0, length - 1);

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
            AudioFiles.Clear();

            var indices = RandomIndices(5, new List<int>(new int[] { 11 }));

            foreach (var index in indices)
            {
                var item = CommandList[index];
                InfoList.Add(new DisplayInfo
                {
                    Title = item.Content,
                    Answer = item.Answer.GetDescription(),
                    Status = Status.IDLE,
                });

                AudioFiles.Add(item.AudioFile);
            }
        }

        public SimulationViewModel()
        {
            Title = "模拟";
            //Reset();
        }
    }
}
