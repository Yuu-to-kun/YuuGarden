using StarGarden.Functions.FileWork;
using StarGarden.Models;
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
using StarGarden.Functions;
using StarGarden.Pages;
using DiscordRPC;
using Windows.Media.Protection.PlayReady;
using System.Diagnostics;
using System.Management;

namespace StarGarden
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void InitializeJumpList()
        {
            GlobalObjects.SG_Console.Show();

            GlobalObjects.DiscordRpcClient.Initialize();
            GlobalObjects.DiscordRpcClient.SetPresence(GlobalObjects.RichPresence);

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

            //Declaring variables
            string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            //Creating AppData Folder and config
            if (!Directory.Exists(LocalDataFolder))
            {
                Directory.CreateDirectory(LocalDataFolder);
                if (!File.Exists(Path.Combine(LocalDataFolder, "config.json")))
                {
                    configFunctions.CreateConfig();
                }
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            GlobalObjects.DiscordRpcClient.Dispose();

            foreach (var item in GlobalObjects.ProcessesList)
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessId={item.Id}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        UInt32 childProcessId = (UInt32)obj["ProcessId"];
                        Process childprocess = null;

                        try
                        {
                            childprocess = Process.GetProcessById((int)childProcessId);
                            childprocess.Kill();
                        }
                        catch (ArgumentException)
                        {

                            item.Kill();
                        }
                    }
                }
            }
        }


    }



}
