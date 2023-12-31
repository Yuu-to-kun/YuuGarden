﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StarGarden.Models.Launcher;
using StarGarden.Models;

namespace StarGarden.Functions.FileWork
{
    public class ConfigFunctions
    {
        string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden");
        string configFolderPathBGP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "StarGarden", "bgProccesses");

        public void CreateConfig()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.appVer = appVersion;
            configFile.fpVer = "";
            configFile.lang = "En-Us";
            configFile.installPath = "";
            configFile.gamesDirPaths = new List<string> {$"{Path.Combine(GlobalObjects.downloadPath, "Games")}"};
            configFile.fpPS4_ExePath = "";
            configFile.gamesAllowedToRun = 1;

            var conf = JsonConvert.SerializeObject(configFile, Formatting.Indented);

            File.WriteAllText(Path.Combine(LocalDataFolder, "config.json"), conf);
        }

        public ConfigFile OpenConfig()
        {
            ConfigFile config = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(Path.Combine(LocalDataFolder, "config.json")));

            return config;
        }

        public void SaveConfig(ConfigFile config)
        {
            var conf = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Path.Combine(LocalDataFolder, "config.json"), conf);
        }

        public BGPConfig OpenBGP_Config()
        {
            var config = JsonConvert.DeserializeObject<BGPConfig>(File.ReadAllText(Path.Combine(configFolderPathBGP,"config.json")));
            return config;
        }
        public void SaveBGP(BGPConfig config)
        {
            var conf = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Path.Combine(configFolderPathBGP, "config.json"), conf);
        }
    }
}
