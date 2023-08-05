using FpPS4_Garden.Functions.FileWork;
using FpPS4_Garden.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace FpPS4_Garden
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FP_Garden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            if (!Directory.Exists(LocalDataFolder))
            {
                Directory.CreateDirectory(LocalDataFolder);
                configFunctions.CreateConfig();
            }
            else if (Directory.Exists(LocalDataFolder) || !File.Exists(Path.Combine(LocalDataFolder,"config.json")))
            {
                configFunctions.CreateConfig();
            }
        }
    }
}
