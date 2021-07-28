using DrivingLightsSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLightsSim.Services
{
    public class LightCommandDataStore
    {
        public List<LightCommand> LightCommands { get; private set; }

        public static readonly LightCommandDataStore Instance = new LightCommandDataStore();

        protected LightCommandDataStore()
        {
            var commandsDict = new Dictionary<LightCommand.AnswerType, List<string>>
            {
                {
                    LightCommand.AnswerType.LOW,
                    new List<string>
                    {
                        "夜间与机动车会车",
                        "夜间同方向近距离跟车行驶",
                        "夜间通过有交通信号灯控制的路口",
                        "夜间在照明良好的道路上行驶",
                    }
                },
                {
                    LightCommand.AnswerType.LOW_HIGH,
                    new List<string>
                    {
                        "夜间超越前方车辆",
                        "夜间通过急弯",
                        "夜间通过坡路",
                        "夜间通过拱桥",
                        "夜间通过人行横道",
                        "夜间通过没有交通信号灯控制的路口",
                    }
                },
                {
                    LightCommand.AnswerType.HIGH,
                    new List<string>
                    {
                        "夜间在没有路灯，照明不良条件下行驶",
                    }
                },
                {
                    LightCommand.AnswerType.FLASHING,
                    new List<string>
                    {
                        "路边临时停车",
                    }
                }
            };

            LightCommands = new List<LightCommand>();

            var count = 1;

            foreach (var item in commandsDict)
            {
                foreach (var value in item.Value)
                {
                    LightCommands.Add(new LightCommand { Content = value, Answer = item.Key, AudioFile = $"{count++}.mp3" });
                }
            }
        }
    }
}