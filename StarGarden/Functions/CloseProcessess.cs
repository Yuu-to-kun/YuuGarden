using StarGarden.Functions.FileWork;
using StarGarden.Models;
using StarGarden.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StarGarden.Functions
{
    public class CloseProcessess
    {
        public async Task Close()
        {
            await Task.Run(async() =>
            {
                
                if (GlobalObjects.runningGames.Count != 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SG_Console.WriteLine("Shutting down processes please wait");
                    });
                   

                    List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler,string)> procConsList
                        = new List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler, string)>(GlobalObjects.runningGames);
                    ConfigFunctions configFunctions = new ConfigFunctions();
                    foreach ((Process process, ConsoleWindow consoleWindow, DataReceivedEventHandler eventHandler, DataReceivedEventHandler eventHandlerError,string gameName) in procConsList)
                    {
                        process.OutputDataReceived -= eventHandler;
                        process.ErrorDataReceived -= eventHandlerError;
                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessId={process.Id}"))
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

                                    process.Kill();
                                    //GlobalObjects.ProcessesList.Remove(items);
                                    GlobalObjects.runningGames.RemoveAll(item => item.Item1 == process);
                                }
                            }
                        }
                        await process.WaitForExitAsync();
                    }
                    

            }
            });
        }
    }
}
