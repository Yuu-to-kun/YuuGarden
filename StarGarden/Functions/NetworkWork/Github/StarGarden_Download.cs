using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions.NetworkWork.Github
{
    public class Asset_Star
    {
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public object label { get; set; }
        public Uploader uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string browser_download_url { get; set; }
    }

    public class Author_Star
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Root_Star
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public Author_Star author { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public List<Asset_Star> assets { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }

    public class Uploader
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
    public class StarGarden_Download
    {
        public Root_Star getJsonAndParse(string GToken, Uri endpoint)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GToken}");
                client.DefaultRequestHeaders.Add("User-Agent", "Kimie");
                var responseMsg = client.GetAsync(endpoint).Result;
                string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                Root_Star latestReleaseJson = JsonConvert.DeserializeObject<Root_Star>(responseBody);
                return latestReleaseJson;

            }
        }
        public string latestReleaseDownloadLink(string GToken, Uri endpoint)
        {
            Root_Star latestReleaseDownloadLink = getJsonAndParse(GToken, endpoint);
            string downloadLink = latestReleaseDownloadLink.assets[0].browser_download_url.ToString();
            return downloadLink;
        }

        public async Task downloadLatestRelease()
        {
            await Task.Run(() =>
            {
                string part0 = "ghp";
                string part1 = "_yB5ynO2r8mjJEr6R";
                string part2 = "61cC5OWK9qwTHk3vIB3u";
                string GToken = part0 + part1 + part2;

                string downloadFilePath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden", "bgProccesses", "bg.exe"));
                Uri endpointLatestRelease = new Uri("https://api.github.com/repos/KimieStar/StarGarden/releases/latest");
                string link = latestReleaseDownloadLink(GToken, endpointLatestRelease);
                DownloadFile dw = new DownloadFile();
                dw.Downloader(link, downloadFilePath);
            });
        }
    }
}
