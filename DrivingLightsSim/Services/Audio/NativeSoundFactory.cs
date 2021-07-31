using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrivingLightsSim.Services.Audio
{
    public class NativeSoundFactory
    {
        public IGenericNativeSound GetNativeSound(string url)
        {
            var o = DependencyService.Get<IGenericNativeSound>();
            o.LoadFile(url);
            return o;
        }
    }
}
