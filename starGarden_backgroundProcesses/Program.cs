using starGarden_backgroundProcesses.Models;
using System.Diagnostics;
using System.IO;
string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "StarGarden","bgProccesses");
ConfigFuncs config = new ConfigFuncs();
List<int> list = new List<int>();

if (!Directory.Exists(configFolderPath))
{
    DirectoryInfo directoryInfo = Directory.CreateDirectory(configFolderPath);
    directoryInfo.Attributes |= FileAttributes.Hidden;
    config.Create(list, configFolderPath);

}

var configList = config.OpenBGP_Config(Path.Combine(configFolderPath)).proccessIds;
    foreach (int Id in config.OpenBGP_Config(Path.Combine(configFolderPath)).proccessIds)
    {
        try
        {
            Process targetProcess = Process.GetProcessById(Id);
            if (!targetProcess.HasExited)
            {
                targetProcess.CloseMainWindow();

                targetProcess.Kill();
                configList.Remove(Id);
              

                Console.WriteLine($"Process with PID {Id} has been closed.");
            }
            else
            {
                Console.WriteLine($"Process with PID {Id} is already exited.");
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"No process with PID {Id} found.");
            configList.Remove(Id);
            continue;
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Process with PID {Id} cannot be accessed.");
            configList.Remove(Id);
            continue;
        }
    }
    if (configList.Count == 0)
    {
        Console.WriteLine($"There are no processes left");
    }
    Config finishedConfig = new Config();
    finishedConfig.proccessIds = configList;
    config.SaveBGP(finishedConfig,configFolderPath);
    
