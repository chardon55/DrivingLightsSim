using DrivingLightsSim.Models;
using DrivingLightsSim.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DrivingLightsSim.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<LightCommand> DataStore => DependencyService.Get<IDataStore<LightCommand>>();

        public IReadOnlyList<LightCommand> CommandList = LightCommandDataStore.Instance.LightCommands;

        public string StartCommand => "请开始考试，下面将开始模拟夜间灯光考试，请在语音播报“叮”一声结束后5秒内完成操作，请开启前照灯。";

        public string StartCommandAudioFile => "header.mp3";

        public string EndCommand => "模拟夜间考试完成，请关闭所有灯光，请起步继续完成考试。";

        public string EndCommandAudioFile => "footer.mp3";

        public string AlertAudioFile => "dong.mp3";

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
