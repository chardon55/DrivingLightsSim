using DrivingLightsSim.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrivingLightsSim.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public DevicePlatform Platform = DeviceInfo.Platform;

        public AboutViewModel()
        {
            Title = "关于";
            OpenWebCommand = new Command(async () =>
            {
                await Browser.OpenAsync("https://github.com/chardon55/DrivingLightsSim/blob/main/LICENSE");
            });
            OpenReleaseCommand = new Command(async () =>
            {
                await Browser.OpenAsync("https://github.com/chardon55/DrivingLightsSim/releases");
            });
        }

        public ICommand OpenWebCommand { get; }

        public ICommand OpenReleaseCommand { get; }

        public ICommand CheckUpdates { get; }

        private Dictionary<string, string> sources = new Dictionary<string, string>
        {
            {
                "GITHUB",
                "https://api.github.com/repos/chardon55/DrivingLightsSim/releases"
            },
            {
                "GITEE",
                "https://gitee.com/api/v5/repos/dy55/driving-lights-sim/releases"
            }
        };

        public async Task<NewVersionInfo> GetReleaseInfoAsync(string source)
        {
            using var client = new WebClient();

            string data = await client.DownloadStringTaskAsync(new Uri(sources[source]));

            var infoList = JsonConvert.DeserializeObject<List<GitHubAPIReleaseInfo>>(data, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            });

            foreach (var item in infoList)
            {
                var info = item;

                if (info.Prerelease)
                {
                    continue;
                }

                if (new Version(info.TagName.Split("-")[0].Replace("v", "")) <= AppInfo.Version)
                {
                    continue;
                }

                string url = null;

                foreach (var asset in info.Assets)
                {
                    string _url = asset.Name != null ? Path.Combine(asset.BrowserDownloadUrl, asset.Name) : asset.BrowserDownloadUrl;

                    if (Platform == DevicePlatform.Android)
                    {
                        if (!_url.EndsWith("apk"))
                        {
                            continue;
                        }
                    }
                    else if (Platform == DevicePlatform.iOS)
                    {
                        if (!_url.EndsWith("ipa"))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    url = _url;
                    break;
                }

                if (url == null)
                {
                    continue;
                }

                var newVersion = new NewVersionInfo
                {
                    Version = info.TagName,
                    Name = info.Id,
                    Title = info.Name,
                    Description = info.Body,
                    Url = url,
                };

                return newVersion;
            }

            return null;
        }
    }
}