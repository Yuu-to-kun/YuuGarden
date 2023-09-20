using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Models.Launcher
{
    public class ConfigFile
    {
        public string appVer;
        public string fpVer;
        public string lang;
        public string installPath;
        public List<string> gamesDirPaths;
        public string fpPS4_ExePath;
        public int gamesAllowedToRun;
    }

    public class BGPConfig
    {
        public List<int> proccessIds;
    }
}
