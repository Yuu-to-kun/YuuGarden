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
using System.Windows.Threading;

namespace StarGarden.Functions
{
    public class StartGame
    {
        public void Start(string elfLocation, string gameName, string logLoc)
        {
            // Instances of Classes
            ConfigFunctions configFunctions = new ConfigFunctions();
            Presence presence = new Presence();
            Logging log = new Logging();
            var config = configFunctions.OpenConfig();

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



            // Loggining
            SG_Console.WriteLine("\rGameLog\r");
            p.OutputDataReceived += GlobalObjects.OutputReceived;

            p.ErrorDataReceived += GlobalObjects.ErrorOutputReceived;




            // Running async the whole process
            _= Task.Run(() =>
            {
               p.Start();
               p.BeginOutputReadLine();
               p.BeginErrorReadLine();

                GlobalObjects.ProcessesList.Add(p);
                // Set presence
                presence.Set($"{gameName}");
                

                //Checking for game window close
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
                GlobalObjects.ProcessesList.Remove(p);
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

                SG_Console.WriteLine("Game has been stopped");
            });
            
            




        }

    }
}
