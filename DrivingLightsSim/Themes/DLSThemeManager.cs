using Chardon.XamarinThemeManager;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrivingLightsSim.Themes
{
    public sealed class DLSThemeManager : ThemeManager
    {
        public static DLSThemeManager Instance { get; } = new DLSThemeManager();

        private DLSThemeManager() : base(new LightTheme(), new DarkTheme())
        {
        }
    }

    public static class ThemeManagerExtensions
    {
        public static void AutoSetTheme(this ThemeManager @this)
        {
            @this.CurrentAppTheme = AppInfo.RequestedTheme;
        }

        public static ResourceDictionary GetResourceDictionary(this ThemeManager @this, AppTheme theme = AppTheme.Unspecified)
        {
            AppTheme t = theme;

            if (t == AppTheme.Unspecified)
            {
                t = @this.CurrentAppTheme;
            }

            return t switch
            {
                AppTheme.Light => @this.LightTheme,
                AppTheme.Dark => @this.DarkTheme,
                _ => null,
            };
        }
    }
}
