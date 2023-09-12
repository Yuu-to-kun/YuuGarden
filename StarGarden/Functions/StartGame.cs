using StarGarden.Functions.FileWork;
using StarGarden.Models;
using StarGarden.Models.Launcher;
using StarGarden.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace StarGarden.Functions
{
    public class StartGame
    {
        public void Start(string elfLocation, string gameName, string logLoc,string iconLoc)
        {
            // Instances of Classes
            ConfigFunctions configFunctions = new ConfigFunctions();
            Presence presence = new Presence();
            Logging log = new Logging();
            var config = configFunctions.OpenConfig();

            // Checks allowed amount of games to run
            if (config.gamesAllowedToRun == GlobalObjects.runningGames.Count)
            {
                SG_Console.WriteLine("Maximum count of allowed games has already been reached");
                return;
            }
            else if (GlobalObjects.runningGames.Any(item=> item.Item5 == gameName))
            {
                SG_Console.WriteLine("Game is already running");
                return;
            }

            // Instances of objects
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
            
                FileName = "cmd.exe",
                Arguments = $"/c \"{config.fpPS4_ExePath} -e {elfLocation} & pause\"",
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,

            };
            Process p = new Process() { 
                 StartInfo = startInfo
            };
            ConsoleWindow currentGameConsole = new ConsoleWindow();
            currentGameConsole.Title = gameName;
            EventHandler<DataReceivedEventArgs> eventHandler = (sender, e) => GlobalObjects.OutputReceived(sender, e, currentGameConsole);
            EventHandler<DataReceivedEventArgs> eventHandlerError = (sender, e) => GlobalObjects.ErrorOutputReceived(sender, e, currentGameConsole);
            DataReceivedEventHandler dataReceivedHandler = new DataReceivedEventHandler(eventHandler);
            DataReceivedEventHandler dataReceivedHandlerError = new DataReceivedEventHandler(eventHandlerError);

            // Game console
            currentGameConsole.Show();
            currentGameConsole.WriteLine("\rGameLog\r");

            // Hooking logging events

            currentGameConsole.Dispatcher.Invoke(new Action(() =>
            {
                p.OutputDataReceived += dataReceivedHandler;

                p.ErrorDataReceived += dataReceivedHandlerError;
            }));
            




            // Running the whole process in a new thread
            _= Task.Run(() =>
            {
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                ConfigFunctions.BGPConfig bGPConfig = configFunctions.OpenBGP_Config();
                bGPConfig.proccessIds.Add(p.Id);
                configFunctions.SaveBGP(bGPConfig);

                GlobalObjects.runningGames.Add((p,currentGameConsole,dataReceivedHandler, dataReceivedHandlerError,gameName));
                
                // Set presence
                presence.Set($"{gameName}");

                while (!p.HasExited)
                {
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessId={p.Id}"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            UInt32 childProcessId = (UInt32)obj["ProcessId"];
                            Process childprocess = null;
                                
                            try
                            {
                                childprocess = Process.GetProcessById((int)childProcessId);
                            }
                            catch (ArgumentException)
                            {

                                p.Kill();
                            }
                        }
                    }
                }
                


                p.WaitForExit();

                // Removeing Process from Lists
                bGPConfig.proccessIds.Remove(p.Id);
                configFunctions.SaveBGP(bGPConfig);
                GlobalObjects.runningGames.RemoveAll(item => item.Item1 == p);


                // Set presence
                try
                {
                    presence.Set($"Idling");
                }
                catch (ObjectDisposedException)
                {

                    return;
                }

                // Save logs
                log.Save(logLoc);
                

                // Checking if the app has not been shutdown and if not it does what it does
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        currentGameConsole.Close();
                        SG_Console.WriteLine("Game has been stopped");
                    });
                }
                    
                
            });
            
            




        }

    }
}
