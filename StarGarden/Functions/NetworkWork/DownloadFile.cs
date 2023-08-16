using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions.NetworkWork
{
    class DownloadFile
    {
        public void Downloader(string link, string downloadFilePath)
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(link, downloadFilePath);
            }
        }
    }
}
