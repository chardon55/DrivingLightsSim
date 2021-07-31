using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Models
{
    public class GitHubAPIReleaseInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TagName { get; set; }

        public string Body { get; set; }

        public string CreateAt { get; set; }

        public string PublishedAt { get; set; }

        public bool Prerelease { get; set; }

        public List<Asset> Assets { get; set; }
    }

    public class Asset
    {
        public string BrowserDownloadUrl { get; set; }

        public string Name { get; set; }
    }
}
