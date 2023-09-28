using StarGarden.Functions.FileWork;
using StarGarden.Functions.NetworkWork.Github;
using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    public class UpdateCheck
    {
        public async Task Update()
        {
            fpPS4_Download fpPS4_Download = new fpPS4_Download();
            await Task.Run(async () =>
            {
                var check = await isLatest();
                if (check == false)
                {
                    await fpPS4_Download.Download();
                }
            });
        }

        public async Task<bool> isLatest()
        {
            fpPS4_Download fpPS4_Download = new fpPS4_Download();
            var result = await Task.Run(async () =>
            {
                var latestWorkFlow = await fpPS4_Download.getLatestWorkFlow();
                var result = false;
                if (GlobalObjects.ConfigFile.fpVer == latestWorkFlow.head_sha.Substring(0, 7))
                {
                    result = true;
                }
                return result;
            });

            return result;
        }
    }
}
