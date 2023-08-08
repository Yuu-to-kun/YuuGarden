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
using System.Windows.Shell;
using Windows.Storage;

namespace FpPS4_Garden
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void InitializeJumpList()
        {
            JumpList jumpList = new JumpList();

            JumpTask jumpTask1 = new JumpTask
            {
                Title = "Game Compatibility List",
                Arguments = "https://fpps4.net/compatibility/",
                CustomCategory = "Links",
                IconResourcePath = "",
                ApplicationPath = "https://fpps4.net/compatibility/",
                Description = "Opens the game compatibility list."
            };

            jumpList.JumpItems.Add(jumpTask1);

            // Apply changes to the JumpList
            jumpList.Apply();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeJumpList();

            string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FP_Garden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            if (!Directory.Exists(LocalDataFolder))
            {
                Directory.CreateDirectory(LocalDataFolder);
                configFunctions.CreateConfig();
            }
            else if (Directory.Exists(LocalDataFolder) && !File.Exists(Path.Combine(LocalDataFolder,"config.json")))
            {
                configFunctions.CreateConfig();
            }
        }
    }



}
