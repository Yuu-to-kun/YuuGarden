using StarGarden.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static StarGarden.Models.fpPS4_Artifact_Json;

namespace StarGarden.Functions.FileWork
{
    public class ConfigFunctions
    {
        string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden");


        public void CreateConfig()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.appVer = appVersion;
            configFile.lang = "En-Us";
            configFile.installPath = "";
            configFile.gamesDirPath = "";
            configFile.fpPS4_ExePath = "";

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
    }
}
