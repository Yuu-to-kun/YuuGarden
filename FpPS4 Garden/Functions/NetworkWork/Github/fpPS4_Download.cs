using FpPS4_Garden.Functions.FileWork;
using FpPS4_Garden.Models;
using FpPS4_Garden.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static FpPS4_Garden.Models.fpPS4_Artifact_Json;

namespace FpPS4_Garden.Functions.NetworkWork.Github
{
    class fpPS4_Download
    {
        public async Task Download(Button PrevButton)
        {

            string filePath = Path.Combine(Misc.downloadPath, "fpPS4.zip");
            string fpPS4_Folder = Path.Combine(Misc.downloadPath, "fpPS4");

            ConfigFunctions configFunctions = new ConfigFunctions();
            
            if (!File.Exists(Path.Combine(Misc.downloadPath, "fpPS4.zip")))
            {
                await Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PrevButton.IsEnabled = false;
                        
                    });
                    if (!Directory.Exists(Misc.downloadPath))
                    {
                        Directory.CreateDirectory(Misc.downloadPath);
                    }
                    Download_Latest_Trunk(Path.Combine(Misc.downloadPath, "fpPS4.zip"));
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PrevButton.IsEnabled = true;
                    });

                    Extract_FpPS4(filePath,fpPS4_Folder);

                    File.Delete(Path.Combine(Misc.downloadPath, "fpPS4.zip"));

                    string appPath = Process.GetCurrentProcess().MainModule.FileName;
                    string startMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Microsoft","Windows","Start Menu","Programs");
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string startMenuFolder = Path.Combine(startMenuPath, "Fp_Garden");

                    if (!Directory.Exists(Path.Combine(startMenuPath,"Fp_Garden")))
                    {
                        Directory.CreateDirectory(startMenuFolder);


                    }
                    if (!File.Exists(Path.Combine(startMenuFolder, "FP Garden.lnk")))
                    {
                        CreateShortcut shortcut = new CreateShortcut();

                        shortcut.startMenuShortcut(Path.Combine(startMenuFolder,"FP Garden.lnk"),appPath);
                    }
                    if (!File.Exists(Path.Combine(desktopPath, "FP Garden.lnk")))
                    {
                        CreateShortcut shortcut = new CreateShortcut();

                        shortcut.startMenuShortcut(Path.Combine(desktopPath, "FP Garden.lnk"), appPath);
                    }

                });
            }
            else
            {
                Console.WriteLine("File Exists");
                if (!Directory.Exists(Path.Combine(Misc.downloadPath, "fpPS4")))
                {
                    
                    await Task.Run(() =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            PrevButton.IsEnabled = false;
                        });

                        Thread.Sleep(2500);
                        Extract_FpPS4(filePath,fpPS4_Folder);
                        File.Delete(Path.Combine(Misc.downloadPath, "fpPS4.zip"));
                        //Thread.Sleep(5000);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            PrevButton.IsEnabled = true;
                        });
                    });
                    
                    
                }
                else
                {
                    Console.WriteLine("Already Extracted");
                }
            }

            var config = configFunctions.OpenConfig();
            config.installPath = Misc.downloadPath;

            configFunctions.SaveConfig(config);
        }

        public void Extract_FpPS4(string filePath, string fpPS4_Folder)
        {
            using (ZipArchive zar = ZipFile.OpenRead(filePath))
            {
                foreach (ZipArchiveEntry entry in zar.Entries)
                {
                    string destinationPath = Path.Combine(fpPS4_Folder, entry.Name);

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    if (!entry.Name.EndsWith("/")) // Skip directory entries
                    {
                        entry.ExtractToFile(destinationPath, overwrite: true);
                    }
                }
            }
        }
        public void Download_Latest_Trunk(string downloadFilePath)
        {
            string part0 = "ghp";
            string part1 = "_yB5ynO2r8mjJEr6R";
            string part2 = "61cC5OWK9qwTHk3vIB3u";
            string GToken = part0 + part1 + part2;

            int latestTrunkArtifactId = getLatestTrunkArtifactID(GToken);
            Uri endpointLatestAction = new Uri("https://api.github.com/repos/red-prig/fpPS4/actions/artifacts/" + $"{latestTrunkArtifactId}" + "/zip");
            string link = getFpPS4_ActionLink(endpointLatestAction, GToken);
            DownloadFile dw = new DownloadFile();
            dw.Downloader(link, downloadFilePath);
        }

        public string getFpPS4_ActionLink(Uri endpoint, string GToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GToken}");
                client.DefaultRequestHeaders.Add("User-Agent", "Kimie");
                var responseMsg = client.GetAsync(endpoint).Result;
                string link = responseMsg.RequestMessage.RequestUri.ToString();
                return link;
            }
        }

        public int getLatestTrunkArtifactID(string GToken)
        {
            Root artifactsJson = getJsonAndParse(GToken);
            int i = 0;
            while (true)
            {
                if (artifactsJson.artifacts[i].workflow_run.head_branch == "trunk")
                {

                    break;
                }
                i++;
            }

            int latestArtifactID = artifactsJson.artifacts[i].id;
            return latestArtifactID;
        }

        public Root getJsonAndParse(string GToken)
        {
            using (var client = new HttpClient())
            {
                Uri endpointArtifactJson = new Uri("https://api.github.com/repos/red-prig/fpPS4/actions/artifacts");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GToken}");
                client.DefaultRequestHeaders.Add("User-Agent", "Kimie");
                var responseMsg = client.GetAsync(endpointArtifactJson).Result;
                string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                Root artifactsJson = JsonConvert.DeserializeObject<Root>(responseBody);
                return artifactsJson;

            }
        }
    }
}
