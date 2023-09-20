using StarGarden.Functions.FileWork;
using StarGarden.Functions.NetworkWork.Github;
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
            ConfigFunctions configFunctions = new ConfigFunctions();
            fpPS4_Download fpPS4_Download = new fpPS4_Download();
            var config = configFunctions.OpenConfig();
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
            ConfigFunctions configFunctions = new ConfigFunctions();
            fpPS4_Download fpPS4_Download = new fpPS4_Download();
            var result = await Task.Run(async () =>
            {
                var latestWorkFlow = await fpPS4_Download.getLatestWorkFlow();
                var config = configFunctions.OpenConfig();
                var result = false;
                if (config.fpVer == latestWorkFlow.head_sha.Substring(0, 7))
                {
                    result = true;
                }
                return result;
            });

            return result;
        }
    }
}
