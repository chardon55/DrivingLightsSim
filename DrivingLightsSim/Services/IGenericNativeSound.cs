using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Services
{
    public interface IGenericNativeSound : ISound, IDisposable
    {
        DeviceType GetDeviceType();
    }

    public enum DeviceType
    {
        ANDROID,
        IOS,
        UWP,
    }
}
