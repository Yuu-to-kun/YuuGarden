using StarGarden.Functions.FileWork;
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
                    gameDetection.GenerateEntries();

                    ConfigFunctions configFunctions = new ConfigFunctions();
                    var config = configFunctions.OpenConfig();

                    if (config.installPath.Equals("") || config.installPath.Equals(null))
                    {
                        GlobalObjects.isFreshInstall = true;
                    }
                    else
                    {
                        GlobalObjects.isFreshInstall = false;
                    }

                    for (int i = 0; i < GlobalObjects.GamesTemplate.Count; i++)
                    {
                        SG_Console.WriteLine($"[Recognised Game]: {GlobalObjects.GamesTemplate[i].Name}");
                    }

                    //Creating AppData Folder and config
                    if (!Directory.Exists(GlobalObjects.localDataPath))
                    {
                        Directory.CreateDirectory(GlobalObjects.localDataPath);

                    }
                    if (!File.Exists(Path.Combine(GlobalObjects.localDataPath, "config.json")))
                    {
                        try
                        {
                            configFunctions.CreateConfig();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Oopsie");
                        }
                        
                    }

                    // Initialize the Jumplist
                    InitializeJumpList();
                });
            });
            
        }

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
    }
}
