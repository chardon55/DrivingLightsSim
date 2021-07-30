using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Services.Audio
{
    public interface IGenericNativeSound : ISound, IDisposable
    {
        DeviceType GetDeviceType();
        void LoadFile(string url);
    }

    public enum DeviceType
    {
        ANDROID,
        IOS,
        UWP,
    }
}
