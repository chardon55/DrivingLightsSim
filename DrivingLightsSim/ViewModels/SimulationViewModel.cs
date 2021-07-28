using DrivingLightsSim.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        public List<string> displayTexts = new List<string>();

        public HashSet<LightCommand> lightCommands = new HashSet<LightCommand>();

        public List<DisplayInfo> InfoList { get; private set; } = new List<DisplayInfo>();

        public struct DisplayInfo
        {
            public string Header {  get; set; }
            public string Answer { get; set; }
            public string BackgroundColor { get; set; }
            public string TextColor { get; set; }
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

        public void ViewAnswer()
        {

        }

        public void HideAnswer()
        {

        }

        public void Regenerate()
        {

        }

        public SimulationViewModel()
        {
            Title = "模拟";
            Regenerate();
        }
    }
}
