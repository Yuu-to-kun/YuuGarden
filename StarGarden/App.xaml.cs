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
using System.Threading;
using System.Runtime.InteropServices;

namespace StarGarden
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_SHOWMINIMIZED = 2;


        Mutex myMutex;
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

            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOWMINIMIZED);
            ShowWindow(handle, SW_HIDE);
        }
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool aIsNewInstance = false;
            myMutex = new Mutex(true, "StarGarden", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("There is an instance is already running...");
                App.Current.Shutdown();
            }


            InitializeJumpList();

            //Declaring variables
            string LocalDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            //Creating AppData Folder and config
            if (!Directory.Exists(LocalDataFolder))
            {
                Directory.CreateDirectory(LocalDataFolder);
               
            }
            if (!File.Exists(Path.Combine(LocalDataFolder, "config.json")))
            {
                configFunctions.CreateConfig();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            StartCleaning start = new StartCleaning();
            start.Clean();
            GlobalObjects.DiscordRpcClient.Dispose();
        }


    }



}
