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
    }
}
