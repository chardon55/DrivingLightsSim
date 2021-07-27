using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Models
{
    public class LightCommand
    {
        public string Content { get; set; }

        public AnswerType Answer { get; set; }

        public enum AnswerType
        {
            LOW, HIGH, LOW_HIGH, FLASHING,
        }
    }
}
