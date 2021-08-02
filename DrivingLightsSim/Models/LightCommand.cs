using System;
using System.Collections.Generic;
using System.Text;
using static DrivingLightsSim.Models.LightCommand;

namespace DrivingLightsSim.Models
{
    public class LightCommand
    {
        public string Content { get; set; }

        public AnswerType Answer { get; set; }

        public string AudioFile { get; set; }

        public bool IsFinal {  get; set; }

        public enum AnswerType
        {
            LOW, HIGH, LOW_HIGH, FLASHING,
        }
    }

    public static class AnswerTypeExtensions
    {
        public static string GetDescription(this AnswerType @this)
        {
            return @this switch
            {
                AnswerType.LOW => "近光灯",
                AnswerType.HIGH => "远光灯（等下一条播报时关闭远光灯）",
                AnswerType.LOW_HIGH => "远光灯交替两次",
                AnswerType.FLASHING => "关闭大灯，开示廓灯，打开双闪",
                _ => "",
            };
        }
    }
}
