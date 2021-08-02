using DrivingLightsSim.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.IO;

namespace DrivingLightsSim.Utils
{
    public static class UpdateChecker
    {
        public static string CurrentSource { get; set; } = "GITEE";

        public static DevicePlatform Platform = DeviceInfo.Platform;

        private static async Task<List<GitHubAPIReleaseInfo>> DownloadUpdateInfoAsync()
        {
            using var client = new WebClient();

            string data = await client.DownloadStringTaskAsync(sources[CurrentSource]);

            return JsonConvert.DeserializeObject<List<GitHubAPIReleaseInfo>>(data, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            });
        }

        private static readonly Dictionary<string, string> sources = new Dictionary<string, string>
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

        public static async Task<NewVersionInfo> CheckCurrent()
        {
            return await Checkers[CurrentSource]();
        }

        private static Dictionary<string, Func<Task<NewVersionInfo>>> Checkers { get; } = new Dictionary<string, Func<Task<NewVersionInfo>>>
        {
            {
                "GITHUB",
                async () =>
                {
                    var infoList = await DownloadUpdateInfoAsync();

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
            },
            {
                "GITEE",
                async () =>
                {
                    var infoList = await DownloadUpdateInfoAsync();

                    infoList.Reverse();

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
        };
    }
}
