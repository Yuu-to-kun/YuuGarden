﻿using FpPS4_Garden.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FpPS4_Garden.Functions.FileWork
{
    public class ConfigFunctions
    {
        string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FP_Garden");


        public void CreateConfig()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.appVer = appVersion;
            configFile.lang = "En-Us";

            var conf = JsonConvert.SerializeObject(configFile, Formatting.Indented);

            File.WriteAllText(Path.Combine(LocalDataFolder, "config.json"), conf);
        }
    }
}