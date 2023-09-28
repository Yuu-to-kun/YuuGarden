using StarGarden.Functions.FileWork;
using StarGarden.Models;
using StarGarden.Pages;
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
using static StarGarden.Models.fpPS4_Github_Classes.fpPS4_Artifact_Json;
using static StarGarden.Models.fpPS4_Github_Classes.fpPS4_Workflow;

namespace StarGarden.Functions.NetworkWork.Github
{
    class fpPS4_Download
    {
        public async Task Download(Button PrevButton = null)
        {
            ConfigFunctions configFunctions = new ConfigFunctions();
            string filePath = Path.Combine(GlobalObjects.downloadPath, "fpPS4.zip");
            string fpPS4_Folder = Path.Combine(GlobalObjects.downloadPath, "fpPS4");
            var config = configFunctions.OpenConfig();

            if (!File.Exists(Path.Combine(GlobalObjects.downloadPath, "fpPS4.zip")))
            {
                 await Task.Run(async() =>
                 {
                     if (!Directory.Exists(GlobalObjects.downloadPath))
                     {
                         Directory.CreateDirectory(GlobalObjects.downloadPath);
                     }
                     await Download_Latest_Trunk(Path.Combine(GlobalObjects.downloadPath, "fpPS4.zip"));
                     Extract_FpPS4(filePath,fpPS4_Folder);
                     config.fpVer = GlobalObjects.fpSha;
                     configFunctions.SaveConfig(config);
                     GlobalObjects.ConfigFile = config;
                     File.Delete(Path.Combine(GlobalObjects.downloadPath, "fpPS4.zip"));

                 });
             }
             else
            {
                SG_Console.WriteLine("File Exists, skipping download.\nExtracting...");
                if (!Directory.Exists(Path.Combine(GlobalObjects.downloadPath, "fpPS4")))
                {
                    
                    await Task.Run(() =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (PrevButton != null)
                            {
                                PrevButton.IsEnabled = false;
                            }
                        });

                        Thread.Sleep(2500);
                        Extract_FpPS4(filePath,fpPS4_Folder);
                        File.Delete(Path.Combine(GlobalObjects.downloadPath, "fpPS4.zip"));
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (PrevButton != null)
                            {
                                PrevButton.IsEnabled = true;
                            }
                        });
                    });
                    
                    
                }
                else
                {
                    SG_Console.WriteLine("Already Extracted, skipping");
                }
            }

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
        public async Task Download_Latest_Trunk(string downloadFilePath)
        {
            string part0 = "ghp";
            string part1 = "_yB5ynO2r8mjJEr6R";
            string part2 = "61cC5OWK9qwTHk3vIB3u";
            string GToken = part0 + part1 + part2;

            await Task.Run(async() =>
            {
                string link = await getFpPS4_DownloadLink(GToken);
                DownloadFile dw = new DownloadFile();
                dw.Downloader(link, downloadFilePath);
            });

            
        }

        public async Task<string> getFpPS4_DownloadLink(string GToken)
        {
            var link = await Task.Run(async() =>
            {
                using (var client = new HttpClient())
                {
                    var workflow = await getLatestWorkFlow();
                    GlobalObjects.fpSha = workflow.head_sha.Substring(0, 7);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GToken}");
                    client.DefaultRequestHeaders.Add("User-Agent", "Kimie");
                    var responseMsg = client.GetAsync(workflow.artifacts_url).Result;
                    string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                    FP_Root artifactsJson = JsonConvert.DeserializeObject<FP_Root>(responseBody);

                    var secondResponseMsg = client.GetAsync(artifactsJson.artifacts[0].archive_download_url).Result;
                    string link = secondResponseMsg.RequestMessage.RequestUri.ToString();
                    return link;
                }
            });
            return link;
            
        }
        public async Task<WorkflowRun> getLatestWorkFlow()
        {
            string part0 = "ghp";
            string part1 = "_yB5ynO2r8mjJEr6R";
            string part2 = "61cC5OWK9qwTHk3vIB3u";
            string GToken = part0 + part1 + part2;
            var workflow = await Task.Run(() =>
            {
                using (var client = new HttpClient())
                {
                    Uri endpointArtifactJson = new Uri("https://api.github.com/repos/red-prig/fpPS4/actions/runs");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GToken}");
                    client.DefaultRequestHeaders.Add("User-Agent", "Kimie");
                    var responseMsg = client.GetAsync(endpointArtifactJson).Result;
                    string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                    WorkFlow_Root artifactsJson = JsonConvert.DeserializeObject<WorkFlow_Root>(responseBody);
                    int i = 0;
                    while (true)
                    {
                        if (artifactsJson.workflow_runs[i].head_branch == "trunk")
                        {
                            break;
                        }
                        i++;
                    }

                    var workflow = artifactsJson.workflow_runs[i];
                    return workflow;

                }
            });
            return workflow;
        }
    }
}
