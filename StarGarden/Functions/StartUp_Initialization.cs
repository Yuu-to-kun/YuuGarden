﻿using StarGarden.Functions.FileWork;
using StarGarden.Functions.NetworkWork;
using StarGarden.Functions.NetworkWork.Github;
using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;

namespace StarGarden.Functions
{
    public class StartUp_Initialization
    {

        public async Task Initialize()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Generate data for Games
                    GameDetection gameDetection = new GameDetection();
                    ConfigFunctions configFunctions = new ConfigFunctions();

                    //Creating AppData Folder and config
                    if (!Directory.Exists(GlobalObjects.localDataPath))
                    {
                        Directory.CreateDirectory(GlobalObjects.localDataPath);

                    }
                    if (!File.Exists(Path.Combine(GlobalObjects.localDataPath, "config.json")))
                    {
                        configFunctions.CreateConfig();
                    }

                    var config = configFunctions.OpenConfig();

                    if (config.installPath.Equals("") || config.installPath.Equals(null))
                    {
                        GlobalObjects.isFreshInstall = true;
                    }
                    else
                    {
                        GlobalObjects.ConfigFile = config;
                        GlobalObjects.isFreshInstall = false;
                        gameDetection.GenerateEntries();
                        for (int i = 0; i < GlobalObjects.GamesTemplate.Count; i++)
                        {
                            SG_Console.WriteLine($"[Recognised Game]: {GlobalObjects.GamesTemplate[i].Name}");
                        }


                        GlobalObjects.DiscordRpcClient.Initialize();
                        GlobalObjects.DiscordRpcClient.SetPresence(GlobalObjects.RichPresence);

                    }
                    fpPS4_Download fpPS4_Download = new fpPS4_Download();
                    var workflow = fpPS4_Download.getLatestWorkFlow();
                    // Initialize the Jumplist
                    InitializeJumpList();


                });
                
            });
        }

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
    }
}
